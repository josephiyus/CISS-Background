using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CISS_Background.id.co.cdp.vo;
using CISS_Background.id.co.cdp.util;
using CISS_Background.id.co.cdp.dao;
using CISS_Background.id.co.cdp.dao.impl;
using CISS_Background.id.co.cdp.constant;
using System.Windows.Forms;
using System.Threading;
using CISS.id.co.cdp.util;
using CISS_Background.id.co.cdp.common.persistence;
using CISS.id.co.cdp.exception;
using System.Diagnostics;
using CISS_Background.id.co.cdp.model;
using CISS.id.co.cdp.vo;
using CISS.id.co.cdp.model;

namespace CISS_Background.id.co.cdp.bo.impl
{
    class EventProcessorBo : IEventProcessor
    {
        private Event currentEvent;
        private object _LOCKER;
        private IRecognitionInfoDao securosDao;
        private IDefaultDao cissDao;
        private MonitoringFieldVo visual;
        private long? headerId;
        private int status = 0;
        private long? logId;

        public EventProcessorBo(ICommonDatabase cissDb, ICommonDatabase securosDb) 
        {
            this.cissDao = DefaultDao.getInstance(cissDb);
            this.securosDao = RecognitionInfoDao.getInstance(securosDb);
        }

        public EventProcessorBo(MonitoringFieldVo visual
            , ICommonDatabase cissDb
            , ICommonDatabase securosDb)
        {
            // TODO: Complete member initialization
            this.visual = visual;
            this.cissDao = DefaultDao.getInstance(cissDb);
            this.securosDao = RecognitionInfoDao.getInstance(securosDb);
        }

        public void setProcessedEvent(Event currentEvent)
        {
            this.currentEvent = currentEvent;
        }

        public void process()
        {
            try
            {
                Monitor.Enter(_LOCKER);
                logId = cissDao.startLog(currentEvent, "start transaction [VEHICLE : " + currentEvent.STAFFNAME + "] in Gate " + currentEvent.gateID.ToString());
                TextViewUtil.appendText(visual.txt_csv, "--- start transaction [VEHICLE : " + currentEvent.STAFFNAME + "] in Gate " + currentEvent.gateID.ToString());
                if (logId != null)
                {
                    cissDao.logActivity(logId, "Checking Truck");
                    TextViewUtil.appendText(visual.txt_csv, "--- Truck Validation");
                    if (currentEvent.ETYPE == "0" && TransactionUtil.isValidTruckVehicle(currentEvent.STAFFNAME))
                    {
                        cissDao.logActivity(logId, "Valid Checking Truck");
                        TextViewUtil.appendText(visual.txt_csv, "--- Truck Valid");
                        TransactionClassification cls = TransactionUtil.getGateInfo(currentEvent);
                        currentEvent.gateID = cls.gateIndex;
                        currentEvent.line = cls.line;
                        currentEvent.lineType = cls.lineType;

                        try
                        {
                            currentEvent.logID = logId;

                            cissDao.logActivity(logId, "start save history");

                            cissDao.saveInHistory(currentEvent);

                            cissDao.logActivity(logId, "end save history");

                            cissDao.logActivity(logId, "check available transaction");

                            //teruskan process utama bila belum terdaftar di db
                            TextViewUtil.appendText(visual.txt_csv, "--- Check Existing previous same truck");
                            RegisteredTransaction tr = cissDao.checkRegisteredTransaction(currentEvent);
                            if (!tr.existingStatus)
                            {
                                //create csv file
                                TableViewUtil.setCellValue(
                                    visual.tabel,
                                    currentEvent.line,
                                    currentEvent.lineType,
                                    7, "Processing..." //ImageUtil.getLoadingImage()
                                );

                                TableViewUtil.setCellValue(
                                    visual.tabel,
                                    currentEvent.line,
                                    currentEvent.lineType,
                                    9,
                                "1");

                                TableViewUtil.setCellValue(
                                    visual.tabel,
                                    currentEvent.line,
                                    currentEvent.lineType,
                                    3,
                                    currentEvent.CARDNO);

                                TableViewUtil.setCellValue(
                                    visual.tabel,
                                    currentEvent.line,
                                    currentEvent.lineType,
                                    4,
                                    currentEvent.STAFFNAME);

                                ContainerInfoVo containerInfo = null;
                                int? amountTapping = 1;
                                TransactionHeaders existingHeader = null;
                                if (tr.existingTransaction != null)
                                {                                    
                                    //MessageBox.Show("exist header id " + tr.existingTransaction.id); 
                                    existingHeader = tr.existingTransaction;
                                    headerId = existingHeader.id;
                                    logId = existingHeader.logId;

                                    if (currentEvent.gateID != 5 && !currentEvent.lineType.Equals(AppConstant.LINE_OUT))
                                    {
                                        containerInfo = new ContainerInfoVo();
                                        containerInfo.container_no = existingHeader.containerOcr;
                                        containerInfo.securos_id = existingHeader.securos_id;
                                    }
                                    else 
                                        containerInfo = getContainerInfoFromSecuros(currentEvent, logId, visual.dummy_decision.Checked);   

                                    if (currentEvent.gateID == 5 && currentEvent.lineType.Equals(AppConstant.LINE_OUT))
                                    {
                                        if (visual.dummy_decision.Checked)
                                        {
                                            DummyUtil.getWeightDummy().ForEach(i =>
                                            {
                                                if (i.truck_no.Equals(currentEvent.STAFFNAME))
                                                    currentEvent.weight = i.weight;
                                            });
                                        }
                                        else
                                            currentEvent.weight = 0;//to be continued
                                    }
                                    existingHeader.amountTapping = tr.existingTransaction.amountTapping + 1;
                                    amountTapping = existingHeader.amountTapping;
                                    cissDao.addTappingAmount(tr.existingTransaction.id);
                                    TextViewUtil.appendText(visual.txt_csv, string.Format("--- Truck {0} Tapping at {1}x", currentEvent.STAFFNAME, amountTapping));
                                }
                                else 
                                {
                                    cissDao.logActivity(logId, "start register event");

                                    if (currentEvent.gateID == 5 && currentEvent.lineType.Equals(AppConstant.LINE_IN)) 
                                    {
                                        if (visual.dummy_decision.Checked) 
                                        {
                                            DummyUtil.getWeightDummy().ForEach(i => { 
                                                if(i.truck_no.Equals(currentEvent.STAFFNAME))
                                                    currentEvent.weight = i.weight;
                                            });
                                        }
                                        else
                                            currentEvent.weight = 0;//to be continued
                                    }

                                    existingHeader = cissDao.registerNewHeaderTransaction(currentEvent);
                                    headerId = existingHeader.id;
                                    cissDao.logActivity(logId, "end register event");

                                    if (!(currentEvent.line + currentEvent.lineType).Equals(AppConstant.LINE_5 + AppConstant.LINE_OUT)) 
                                        containerInfo = getContainerInfoFromSecuros(currentEvent, logId, visual.dummy_decision.Checked);
                                    TextViewUtil.appendText(visual.txt_csv, string.Format("--- Truck {0} First Tapping", currentEvent.STAFFNAME));
                                }

                                TableViewUtil.setCellValue(
                                    visual.tabel,
                                    currentEvent.line,
                                    currentEvent.lineType,
                                    9,
                                    amountTapping.ToString());                                

                                if (currentEvent.gateID == 5
                                    && currentEvent.lineType.Equals(AppConstant.LINE_OUT)
                                    && existingHeader.amountTapping == 1)
                                {
                                    RestApiUtil.pushNotifToWeb(headerId, 21, 21, 1, 0, 0, visual);
                                    TableViewUtil.setCellValue(
                                        visual.tabel,
                                        currentEvent.line,
                                        currentEvent.lineType,
                                        7, "Gate Opened"
                                    );
                                    status = 1;
                                }
                                else
                                {
                                    cissDao.setStatus(headerId, 0, null, null); //set status untuk fase process validasi

                                    currentEvent.containerNo = containerInfo.container_no;

                                    TableViewUtil.setCellValue(
                                        visual.tabel,
                                        currentEvent.line,
                                        currentEvent.lineType,
                                        5,
                                        containerInfo.container_no);

                                    TableViewUtil.setCellValue(
                                        visual.tabel,
                                        currentEvent.line,
                                        currentEvent.lineType,
                                        6,
                                        containerInfo.securos_id.ToString());

                                    cissDao.setSecurosResult(headerId, containerInfo.securos_id, containerInfo.container_no);

                                    cissDao.setStatus(headerId, 1, null, null);//set status untuk fase process validasi

                                    RestApiUtil.pushNotifToWeb(headerId, 12, 2, 1, 0, 0, visual);

                                    TextViewUtil.appendText(visual.txt_csv, "--- start DOSS Validation");
                                    DossInfoVo dossInfo = AppUtil.validateDoss(currentEvent, visual.dummy_decision.Checked, visual);

                                    bool valid = dossInfo.validationResult;
                                    string resultMessage = dossInfo.message;

                                    cissDao.logActivity(logId, resultMessage);
                                    TextViewUtil.appendText(visual.txt_csv, "--- end DOSS Validation");
                                    if (valid)
                                    {
                                        TextViewUtil.appendText(visual.txt_csv, "--- Success Transaction Result");
                                        //success
                                        cissDao.logActivity(logId, "Success Validation");
                                        //Thread subWorker = new Thread(this.successProcessing);
                                        //subWorker.Start();
                                        successProcessing();
                                    }
                                    else
                                    {
                                        TextViewUtil.appendText(visual.txt_csv, "--- Failed Transaction Resulr");
                                        //MessageBox.Show("failed validation header id " + headerId + " of " + amountTapping.ToString());
                                        //failed
                                        cissDao.setStatus(headerId, 1, 0, dossInfo.message_error_id);//set status untuk fase process validasi
                                        if (dossInfo.message_error_id == 11)
                                        {
                                            RestApiUtil.pushNotifToWeb(headerId, dossInfo.message_error_id, 13, 1, 0, 0, visual);
                                            TableViewUtil.setCellValue(
                                                visual.tabel,
                                                currentEvent.line,
                                                currentEvent.lineType,
                                                7, "No Assigned"
                                            );
                                        }
                                        else
                                        {
                                            if (visual.dummy_decision.Checked)
                                            {
                                                cissDao.saveSchedule(dossInfo.schedule);
                                            }

                                            RestApiUtil.pushNotifToWeb(headerId, dossInfo.message_error_id, 13, 0, 1, 0, visual);
                                            TableViewUtil.setCellValue(
                                                visual.tabel,
                                                currentEvent.line,
                                                currentEvent.lineType,
                                                7, "Failed"
                                            );
                                        }
                                    }

                                    TableViewUtil.setCellValue(
                                        visual.tabel,
                                        currentEvent.line,
                                        currentEvent.lineType,
                                        8, "-"
                                    );
                                }                               
                            }
                            else
                            {
                                if (tr.existingTransaction != null && tr.existingTransaction.validationStatus == 1)
                                {
                                    status = 1;
                                    if (tr.existingTransaction.printingStatus == 0 || tr.existingTransaction.printingStatus == null)
                                    {
                                        currentEvent.containerNo = tr.existingTransaction.containerOcr;

                                        TransactionHeaders h = new TransactionHeaders();
                                        h.id = tr.existingTransaction.id;
                                        h.printingStatus = 1;
                                        cissDao.updateHeader(h);

                                        TextViewUtil.appendText(visual.txt_csv, "--- Second Tapping Occured");
                                        TextViewUtil.appendText(visual.txt_csv, string.Format("--- Card no {0} is valid", currentEvent.CARDNO));
                                        RestApiUtil.pushNotifToWeb(tr.existingTransaction.id, 1, 1, 1, 0, 1, visual);

                                        if (!visual.dummy_decision.Checked)
                                        {
                                            Thread t = new Thread(executeEkisokPrinting);
                                            t.Start();
                                        }
                                    }
                                }
                                else 
                                {
                                    cissDao.logActivity(logId, "transaction cant be processed");
                                    TextViewUtil.appendText(visual.txt_csv, "--- Invalid Tapping Trial");
                                }
                                
                                TableViewUtil.setCellValue(
                                    visual.tabel,
                                    currentEvent.line,
                                    currentEvent.lineType,
                                    9,
                                    (tr.existingTransaction.amountTapping + 1).ToString());
                                cissDao.addTappingAmount(tr.existingTransaction.id);
                            }
                        }
                        catch (CISSDbFailedConnectionException)
                        {
                            LabelViewUtil.setOfflineMode(visual.ciss_db_status, "NO");
                        }
                        catch (Exception e)
                        {
                            TextViewUtil.appendText(visual.txt_csv, "--- unhandled error : " + e.Message);
                            cissDao.logActivity(logId, "unhandled error : " + e.Message);
                            long? headerId = cissDao.getLastHeaderId(currentEvent);
                            RestApiUtil.pushNotifToWeb(headerId, 13, 13, 1, 0, 0, visual);
                        }
                        finally
                        {
                            if (status == 0)
                            {
                                cissDao.logActivity(logId, status, "transaction is done with not successfully");
                            }
                            else
                            {
                                cissDao.logActivity(logId, status, "transaction is done with successfully");
                            }
                            TextViewUtil.appendText(visual.txt_csv, "--- end transaction [no pol : " + currentEvent.STAFFNAME + "] in Gate " + currentEvent.gateID.ToString());
                            cissDao.logActivity(logId, "end transaction [no pol : " + currentEvent.STAFFNAME + "] in Gate " + currentEvent.gateID.ToString());
                        }
                    }
                    else
                    {
                        cissDao.logActivity(logId, "Invalid Checking Truck");
                        if (currentEvent.STAFFNAME.ToUpper().Contains("MASTER")
                            || currentEvent.STAFFNAME.ToUpper().Contains("TEST"))
                        {
                            long? headerId = cissDao.setMaster(currentEvent);
                            cissDao.setStatus(headerId, 1, null, null);

                            TableViewUtil.setCellValue(
                                visual.tabel,
                                currentEvent.line,
                                currentEvent.lineType,
                                8, "MASTER"
                            );
                        }
                    }
                }                
            }
            catch (CISSDbFailedConnectionException)
            {
                LabelViewUtil.setOfflineMode(visual.ciss_db_status, "NO");
            }
            catch (FailedWriteViewException)
            {
                MessageBox.Show("Failed Write View");
            }
            catch (Exception e)
            {
                MessageBox.Show("error trans desc : " + e.ToString());
                cissDao.logActivity(logId, "error trans desc : " + e.ToString());
            }
            finally 
            {
                Monitor.Exit(_LOCKER);
            }                 
        }

        private void successProcessing()
        {
            RestApiUtil.pushNotifToWeb(headerId, 14, 14, 1, 0, 0, visual);

            TableViewUtil.setCellValue(
                visual.tabel,
                currentEvent.line,
                currentEvent.lineType,
                7, "Verified"
            );

            cissDao.setStatus(headerId, 1, 1, null);//set status untuk fase process validasi
            status = 1;

            cissDao.logActivity(logId, "start pembentukan file P1");
            string p1FilePath = TransactionUtil.generateP1File(currentEvent);
            cissDao.logActivity(logId, "file : " + p1FilePath + " berhasil terbentuk");
            cissDao.logActivity(logId, "end pembentukan file P1");
            if (!(currentEvent.line.Equals(AppConstant.LINE_5) && currentEvent.lineType.Equals(AppConstant.LINE_OUT)))
            {
                Thread.Sleep(10000);
                RestApiUtil.pushNotifToWeb(headerId, 14, 4, 1, 0, 0, visual);
            }
        }

        private ContainerInfoVo getContainerInfoFromSecuros(Event currentEvent, long? logId, bool dummy)
        {
            ContainerInfoVo result = new ContainerInfoVo();
            cissDao.logActivity(logId, "start rconnect to securos to get container no");
            try
            {
                if (dummy)
                {
                    List<SecurosDummyVo> dummies = DummyUtil.getCurrentSecurosDummy();
                    dummies.ForEach(i=>{
                        if (i.gate.Equals(currentEvent.gateID.ToString())
                            && i.truck_no.Equals(currentEvent.STAFFNAME)) 
                        {
                            result.container_no = i.container_no;
                            result.securos_id = 0;
                        }
                    });

                    if (result.container_no != null) 
                    {
                        TransactionHeaders prevHeader = cissDao.getPrevHeader(currentEvent.gateID, headerId);
                        if (prevHeader.containerOcr == result.container_no) 
                        {
                            result.container_no = null;
                        }
                    }
                }
                else 
                {
                    //RecognitionInfoVo securosRecog = securosDao.getRecognitionInfo(currentEvent);
                    TLogVo tlog = securosDao.getLastTransactionByGateId(currentEvent.gateID.ToString());
                    TransactionHeaders prevHeader = cissDao.getPrevHeader(currentEvent.gateID, headerId);

                    if (prevHeader.containerOcr == tlog.plate_recognized)
                    {
                        tlog.plate_recognized = null;
                    }

                    result.con_db_status = true;
                    TransactionHeaders h = new TransactionHeaders();
                    result.container_no = tlog.plate_recognized;
                    result.securos_id = tlog.tid;
                }

                if (result.container_no == null || result.container_no.Trim().Equals("")) 
                    result.container_no = AppConstant.EMPTY_CONTAINER;
                int gateIndex = currentEvent.gateID;
                /*
                 if (gateIndex == 3 || gateIndex == 4 || gateIndex == 5)
                {
                    if (result.container_no != null)
                    {
                        if (currentEvent.TRCODE == AppConstant.CA)
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
                        if (currentEvent.TRCODE == AppConstant.CB)
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
                 */
            }
            catch (SecurosDbFailedConnectionException)
            {
                result.container_no = AppConstant.EMPTY_CONTAINER;
                result.con_db_status = false;
                LabelViewUtil.setOfflineMode(visual.securos_db_status, "NO");
            }
            catch (Exception)
            {
                result.container_no = AppConstant.EMPTY_CONTAINER;
            }
            cissDao.logActivity(logId, "end rconnect to securos to get container no");
            return result;
        }

        public void setLockerLine(object locker)
        {
            this._LOCKER = locker;
        }

        public void executeEkisokPrinting() 
        {
            lock(visual)
            {
                AppUtil.dossUpdate(currentEvent, visual);
                RestApiResult result = AppUtil.getEkioskInfoStructure(currentEvent, visual);
                if(result.status.Equals("1"))
                    AppUtil.printJob(currentEvent, visual, result.message);
            }
        }
    }
}
