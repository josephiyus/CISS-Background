using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CISS_Background.id.co.cdp.vo;
using System.Threading;
using CISS_Background.id.co.cdp.dao;
using CISS_Background.id.co.cdp.dao.impl;
using CISS_Background.id.co.cdp.util;
using CISS_Background.id.co.cdp.constant;
using System.Windows.Forms;
using CISS_Background.id.co.cdp.model;

namespace CISS_Background.id.co.cdp.bo.impl
{
    class EventProcessorBoDummy : IEventProcessor
    {
        private IDefaultDao appRepo;
        private IRecognitionInfoDao recogDao;
        private Event currentEvent;
        private DataGridView tbl_transaction_monitoring;

        public EventProcessorBoDummy() 
        {
            this.appRepo = DefaultDao.getInstance(null);
            this.recogDao = RecognitionInfoDao.getInstance(null);
        }

        public EventProcessorBoDummy(DataGridView tbl_transaction_monitoring)
        {
            // TODO: Complete member initialization
            this.tbl_transaction_monitoring = tbl_transaction_monitoring;
            this.appRepo = DefaultDao.getInstance(null);
            this.recogDao = RecognitionInfoDao.getInstance(null);
        }

        public void setProcessedEvent(Event ev)
        {
            this.currentEvent = ev;
        }

        public void process()
        {
            /*
             Console.WriteLine("process " + ev.STAFFNAME + " ke-1");
            Thread.Sleep(500);
            Console.WriteLine("process " + ev.STAFFNAME + " ke-2");
            Thread.Sleep(500);
            Console.WriteLine("process " + ev.STAFFNAME + " ke-3");
            Thread.Sleep(500);
            Console.WriteLine("process " + ev.STAFFNAME + " ke-4");
             */
            try
            {
                MessageBox.Show("START PROCESS");
                if (currentEvent.ETYPE == "0")
                {
                    MessageBox.Show("START LOG");
                    long? logId = appRepo.startLog(currentEvent, "start transaction [no pol : " + currentEvent.STAFFNAME + "] in Gate " + currentEvent.gateID.ToString());
                    MessageBox.Show("END LOG");
                    ContainerInfoVo containerInfo = getContainerInfoFromSecuros(currentEvent, logId);

                    TableViewUtil.setCellValue(
                                    tbl_transaction_monitoring,
                                    currentEvent.line,
                                    currentEvent.lineType,
                                    9,
                                "1");

                    TableViewUtil.setCellValue(
                        tbl_transaction_monitoring,
                        currentEvent.line,
                        currentEvent.lineType,
                        3,
                        currentEvent.CARDNO);

                    TableViewUtil.setCellValue(
                        tbl_transaction_monitoring,
                        currentEvent.line,
                        currentEvent.lineType,
                        4,
                        currentEvent.STAFFNAME);
                    /*ev.gateID = TransactionUtil.getGateInfo(ev).gateIndex;
                   long? headerId = appRepo.registerNewTransaction(ev);
                   appRepo.setStatus(headerId, 1, null);
                   appRepo.setStatus(headerId, 2, null);
                   appRepo.setStatus(headerId, 3, null);
                   appRepo.setStatus(headerId, 4, 1);
                   appRepo.setStatus(headerId, 4, 0);
                
                    long? logId = appRepo.startLog("start transaction [no pol : " + ev.STAFFNAME + "] in Gate " + ev.gateID.ToString());
                
                   appRepo.logActivity(logId, "start save history");

                   appRepo.saveInHistory(ev);

                   appRepo.logActivity(logId, "end save history");

                   appRepo.logActivity(logId, "check available transaction");

                   appRepo.logActivity(logId, "start register event");
                   long? headerId = appRepo.registerNewTransaction(ev);
                   appRepo.logActivity(logId, "end register event");

                   appRepo.setStatus(headerId, 1, null);//set status untuk fase process validasi

                   appRepo.logActivity(logId, "start transaction validation");
                   //Dictionary<string, object> dossInfo = dossRepo.validateDryPortAutogate(containerNo, platLisenceNo);
                   appRepo.logActivity(logId, "end transaction validation");
                   appRepo.setStatus(headerId, 1, 1);
                   appRepo.logActivity(logId, "end transaction [no pol : " + ev.STAFFNAME + "] in Gate " + ev.gateID.ToString());
                    */
                }
            }
            catch (Exception e) 
            {
                MessageBox.Show(e.Message);
            }
        }

        private ContainerInfoVo getContainerInfoFromSecuros(Event ev, long? logId)
        {
            ContainerInfoVo result = new ContainerInfoVo();
            RecognitionInfoVo securosRecog = recogDao.getRecognitionInfo(ev);
            appRepo.logActivity(logId, "start rconnect to securos to get container no");
            int gateIndex = ev.gateID;

            result.container_no = securosRecog.plate_no;

            if (gateIndex == 3 || gateIndex == 4 || gateIndex == 5)
            {
                if (result.container_no != null)
                {
                    if (ev.TRCODE == AppConstant.CA)
                    {
                        result.container_no = AppConstant.EMPTY_CONTAINER;
                        result.is_container = false;
                    }
                    else
                    {
                        result.is_container = true;
                    }
                }
                else
                {
                    result.is_container = true;
                }
            }
            else if (gateIndex == 1 || gateIndex == 2)
            {
                if (result.container_no != null)
                {
                    if (ev.TRCODE == AppConstant.CB)
                    {
                        result.container_no = AppConstant.EMPTY_CONTAINER;
                        result.is_container = false;
                    }
                    else
                    {
                        result.is_container = true;
                    }
                }
                else
                {
                    result.is_container = true;
                }
            }

            appRepo.logActivity(logId, "end rconnect to securos to get container no");
            return result;
        }

        public void setLockerLine(object locker)
        {
            throw new NotImplementedException();
        }
    }
}
