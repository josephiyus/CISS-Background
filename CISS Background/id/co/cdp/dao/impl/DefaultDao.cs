using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CISS_Background.id.co.cdp.vo;
using CISS_Background.id.co.cdp.util;
using CISS_Background.id.co.cdp.common.persistence;
using CISS_Background.id.co.cdp.common.persistence.abstracts;
using CISS_Background.id.co.cdp.constant;
using System.Globalization;
using System.Windows.Forms;
using CISS_Background.id.co.cdp.model;
using CISS.id.co.cdp.vo;

namespace CISS_Background.id.co.cdp.dao.impl
{
    class DefaultDao : IDefaultDao
    {
        ICommonDatabase repo;

        public static DefaultDao getInstance(ICommonDatabase conDb) 
        {
            return new DefaultDao(conDb);
        }

        public DefaultDao() 
        {
            this.repo = MySqlCommon.getInstance();
        }

        public DefaultDao(ICommonDatabase conDb)
        {
            // TODO: Complete member initialization
            this.repo = conDb;
        }

        public RegisteredTransaction checkRegisteredTransaction(Event param)
        {
            RegisteredTransaction result = new RegisteredTransaction();
            TransactionHeaders h = new TransactionHeaders();
            h.gateId = param.gateID;

            result.existingTransaction = repo.getOneOrDefault(h, "id", AppConstant.DESC, null);

            if (result.existingTransaction.id != null)
                h.id = result.existingTransaction.id;

            result.existingTransaction = repo.getOneOrDefault(h, "id", AppConstant.DESC, new string[] { "(TIMESTAMPDIFF(SECOND, end_date, NOW())) < 30" });
            result.existingStatus = result.existingTransaction.id != null;

            if (result.existingStatus == false)
            {
                result.existingTransaction = repo.getOneOrDefault(h, "id", AppConstant.DESC, new string[] { "(TIMESTAMPDIFF(SECOND, end_date, NOW())) > 30" });
                result.existingStatus = result.existingTransaction.platNo == param.STAFFNAME;
                if (result.existingStatus)
                {
                    if (result.existingTransaction.amountTapping == 1
                        && param.line.Equals(AppConstant.LINE_5)
                        && param.lineType.Equals(AppConstant.LINE_OUT))
                        result.existingStatus = false;
                    else 
                    {
                        result.existingStatus = result.existingTransaction.validationStatus != null 
                            && result.existingTransaction.validationStatus == 1;
                        if (result.existingStatus == false) 
                        {
                            if (param.line.Equals(AppConstant.LINE_5)
                                && param.lineType.Equals(AppConstant.LINE_OUT))
                                result.existingStatus = false;
                            else
                                result.existingStatus = result.existingTransaction.errMsgId != 11;
                        }
                            
                    }
                }
                else //if (param.line.Equals(AppConstant.LINE_5) && param.lineType.Equals(AppConstant.LINE_OUT))
                    result.existingTransaction = null;
            }
            //else if (param.line.Equals(AppConstant.LINE_5) && param.lineType.Equals(AppConstant.LINE_OUT))
            //    result.existingTransaction = null;
            return result;
        }


        public void saveInHistory(Event currentEvent)
        {
            //insert into transactions
        }

        public long? startLog(Event param, string message)
        {
            LogHeadersVo header = new LogHeadersVo(param.gateID, 0);

            repo.insert(header);
            header = repo.getOneOrDefault(header, "id", AppConstant.DESC, null);

            LogDetailsVo detail = new LogDetailsVo();
            if (header.id != null)
            {
                detail.id = header.id;
                detail.seq = 1;
                detail.activityNote = message;
                repo.insert(detail);
            }
            long? created_id = header.id;

            return header.id;
        }

        public void logActivity(long? logId, string message)
        {
            LogDetailsVo detail = new LogDetailsVo();
            detail.id = logId;
            LogDetailsVo dEXIST = repo.getOneOrDefault(detail, "seq", AppConstant.DESC, null);
            detail.seq = dEXIST.seq + 1;
            detail.activityNote = message;

            repo.insert(detail);

            LogHeadersVo header = new LogHeadersVo();
            header.id = logId;
            repo.update(header);           
        }

        public void logActivity(long? logId, int status, string message)
        {
            LogDetailsVo detail = new LogDetailsVo();
            detail.id = logId;
            detail.seq = repo.getOneOrDefault(detail, "seq", AppConstant.DESC, null).seq + 1;
            detail.activityNote = message;
            repo.insert(detail);

            LogHeadersVo header = new LogHeadersVo();
            header.id = logId;
            header.status = status;
            repo.update(header);            
        }

        public void setStatus(long? headerId, int? phaseStatus, int? validationStatus, int? errorMsgId)
        {
            TransactionHeaders h = new TransactionHeaders();
            h.id = headerId;
            h.phaseStatus = phaseStatus;
            h.validationStatus = validationStatus;
            h.errMsgId = errorMsgId;

            repo.update(h);

            TransactionDetails d = new TransactionDetails();
            d.id = headerId;

            TransactionDetails lastDetail = repo.getOneOrDefault(d, "seq", AppConstant.DESC, null);

            if (lastDetail.seq == null)
                d.seq = 1;
            else
            {
                repo.update(lastDetail);
                d.seq = lastDetail.seq + 1;
            }

            d.status = phaseStatus;
            repo.insert(d);            
        }

        public void updateStatus(long? headerId, int? phaseStatus, int? validationStatus)
        {
            TransactionHeaders h = new TransactionHeaders();
            h.id = headerId;
            h.phaseStatus = phaseStatus;
            h.validationStatus = validationStatus;

            repo.update(h);

            TransactionDetails d = new TransactionDetails();
            d.id = headerId;

            d.seq = repo.getOneOrDefault(d, "seq desc", null, null).seq;
            d.status = phaseStatus;
            var x = repo.update(d);
        }


        public long? registerNewTransaction(Event currentEvent)
        {
            TransactionHeaders h = new TransactionHeaders();           

            h.gateId = currentEvent.gateID;
            h.platNo = currentEvent.STAFFNAME;
            h.phaseStatus = 0;
            h.rfid = currentEvent.CARDNO;
            h.tappingDate = DateTimeUtil.parseDateTime(DateTimeUtil.getFormattedDateTime(currentEvent.TRDATE, currentEvent.TRTIME));
            h.xml = currentEvent.xml;
            h.logId = currentEvent.logID;
            h.weight = currentEvent.weight;
            h.amountTapping = 1;

            repo.insert(h);
            h = new TransactionHeaders(h.gateId, currentEvent.STAFFNAME, currentEvent.CARDNO);
            h = repo.getOneOrDefault(h, "id", AppConstant.DESC, null);
            return h.id;
        }

        public TransactionHeaders registerNewHeaderTransaction(Event currentEvent)
        {
            TransactionHeaders h = new TransactionHeaders();

            h.gateId = currentEvent.gateID;
            h.lineType = currentEvent.lineType;
            h.platNo = currentEvent.STAFFNAME;
            h.phaseStatus = 0;
            h.rfid = currentEvent.CARDNO;
            h.tappingDate = DateTimeUtil.parseDateTime(DateTimeUtil.getFormattedDateTime(currentEvent.TRDATE, currentEvent.TRTIME));
            h.xml = currentEvent.xml;
            h.logId = currentEvent.logID;
            h.weight = currentEvent.weight;
            h.amountTapping = 1;

            repo.insert(h);
            h = new TransactionHeaders(h.gateId, currentEvent.STAFFNAME, currentEvent.CARDNO);
            h = repo.getOneOrDefault(h, "id", AppConstant.DESC, null);
            return h;
        }


        public void setSecurosResult(long? headerId, long? securos_id, string no_container)
        {
            TransactionHeaders h = new TransactionHeaders();
            h.id = headerId;
            h.securos_id = securos_id;
            h.containerOcr = no_container;
            h.containerValid = no_container;
            repo.update(h);
        }


        public long? setMaster(Event currentEvent)
        {
            TransactionHeaders h = new TransactionHeaders();
            h.gateId = currentEvent.gateID;
            h.id = repo.getOneOrDefault(h, "id", AppConstant.DESC, null).id;
            h.masterStatus = 1;
            repo.update(h);
            return h.id;
        }


        public long? getLastHeaderId(Event currentEvent)
        {
            TransactionHeaders h = new TransactionHeaders();
            h.gateId = currentEvent.gateID;
            h = repo.getOneOrDefault(h, "id", AppConstant.DESC, null);

            return h.id;
        }


        public void addTappingAmount(long? headerId)
        {
            TransactionHeaders h = new TransactionHeaders();
            h.id = headerId;
            h.amountTapping = repo.getOneOrDefault(h, null, null, null).amountTapping + 1;
            repo.update(h);
        }


        public long? getLogId(Event currentEvent)
        {
            TransactionHeaders h = new TransactionHeaders();
            h.id = getLastHeaderId(currentEvent);
            h = repo.getOneOrDefault(h, null, null, null);
            return h.logId;
        }


        public void saveSchedule(TruckScheduleVo schedule)
        {
            repo.insert(schedule);
        }


        public TransactionHeaders getPrevHeader(int gate, long? headerId)
        {
            TransactionHeaders param = new TransactionHeaders();
            param.gateId = gate;
            return repo.getOneOrDefault(param, "id", AppConstant.DESC, 
                new string[] { 
                    "id<>" + headerId, "container_ocr<>'" + AppConstant.EMPTY_CONTAINER + "'",
                    "day(tapping_date) = day(CURDATE())",
                    "month(tapping_date) = month(CURDATE())",
                    "year(tapping_date) = year(CURDATE())"
                });
        }


        public void updateHeader(TransactionHeaders h)
        {
            repo.update(h);
        }
    }
}
