using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CISS_Background.id.co.cdp.common.persistence;
using CISS_Background.id.co.cdp.vo;
using CISS_Background.id.co.cdp.constant;
using System.Windows.Forms;
using CISS.id.co.cdp.util;
using CISS.id.co.cdp.vo;

namespace CISS_Background.id.co.cdp.util
{
    public static class AppUtil
    {
        public static bool checkExpirationApp()
        {
            int amountDays = 0;
            try
            {
                amountDays = (int)Math.Round((DateTime.Now - DateTime.Parse("08/29/2021")).TotalDays);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return amountDays < 30;
        }

        public static DossInfoVo validateDoss(Event currentEvent, bool dummy, MonitoringFieldVo visual)
        {
            DossInfoVo result = new DossInfoVo();
            if (dummy)
            {
                List<TruckScheduleVo> schedules = DummyUtil.getCurrentTruckScheduleDummy();
                schedules.ForEach(schedule =>
                {
                    if (schedule.gate.ToString().Equals(currentEvent.gateID.ToString())
                        && schedule.rfid.Equals(currentEvent.CARDNO)
                        && schedule.truck_no.Equals(currentEvent.STAFFNAME))
                    {
                        result.schedule = schedule;
                        if (schedule.container_no.Equals(currentEvent.containerNo))
                            result.validationResult = true;
                        else if (currentEvent.containerNo.Equals(AppConstant.EMPTY_CONTAINER))
                            result.message_error_id = 5;
                        else
                            result.message_error_id = 6;
                    }
                });

                if (result.validationResult == false && result.message_error_id == 0)
                    result.message_error_id = 11;
            }
            else
            {
                try
                {
                    string endPoint = ConfigurationUtil.getConfigFromFile<APIConfigVo>(AppConstant.API_CONFIG).doss_api_basic;
                    string jsonParam = JsonUtil.getJsonFormat<DossParamVo>(
                            new DossParamVo(currentEvent.containerNo, currentEvent.CARDNO, currentEvent.lineType));

                    string resultStr = RestApiUtil.hitCdpApi(endPoint, jsonParam, visual, "DOSS");
                    
                    if (resultStr.ToLower().Contains("null")) result.message_error_id = 11;
                    else
                    {
                        //ResultVo r = JsonConvert.DeserializeObject<ResultVo>(resultStr);
                        if (resultStr.ToLower().Contains(AppConstant.SUCCESS))
                            result.validationResult = true;
                        else if (currentEvent.containerNo.Equals(AppConstant.EMPTY_CONTAINER))
                            result.message_error_id = 5;
                        else
                            result.message_error_id = 6;
                    }
                    result.message = resultStr;
                }
                catch (Exception e)
                {
                    TextViewUtil.appendText(visual.txt_csv, "--- Error Execute API : " + e.Message);
                    result.message_error_id = 22;
                    result.message = e.ToString().Replace("'", "");
                }
            }
            return result;
        }

        public static RestApiResult getEkioskInfoStructure(Event currentEvent, MonitoringFieldVo visual)
        {
            RestApiResult result = new RestApiResult();
            result.status = "0";

            try
            {
                var endPoint = (currentEvent.lineType.Equals(AppConstant.LINE_IN) ?
                   ConfigurationUtil.getConfigFromFile<APIConfigVo>(AppConstant.API_CONFIG).ekiosk_in :
                   ConfigurationUtil.getConfigFromFile<APIConfigVo>(AppConstant.API_CONFIG).ekiosk_out);

                string jsonParam = JsonUtil.getJsonFormat<EkioskParamVo>(new EkioskParamVo(currentEvent.CARDNO));
                string resultStr = RestApiUtil.hitCdpApi(endPoint, jsonParam, visual, "Ekisok Get Printing Info");
                result.status = "1";
                result.message = resultStr;
                TextViewUtil.appendText(visual.txt_csv, "--- end hitting ekiosk api with success");
            }
            catch (Exception e)
            {
                TextViewUtil.appendText(visual.txt_csv, "--- end hitting ekiosk api with failed cause : " + e.Message);
            }

            return result;
        }

        public static void dossUpdate(Event currentEvent, MonitoringFieldVo visual)
        {
            try
            {
                string endPoint = ConfigurationUtil.getConfigFromFile<APIConfigVo>(AppConstant.API_CONFIG).put_response;
                string jsonParam = JsonUtil.getJsonFormat<DossResponseVo>(
                        new DossResponseVo(currentEvent.containerNo, currentEvent.CARDNO, currentEvent.lineType));

                string resultStr = RestApiUtil.hitCdpApi(endPoint, jsonParam, visual, "Response Putting");
            }
            catch (Exception e) 
            {
                TextViewUtil.appendText(visual.txt_csv, "--- end hitting Response Putting api with failed cause : " + e.Message);
            }
        }

        public static void printJob(Event currentEvent, MonitoringFieldVo visual, string jsonParam)
        {
            RestApiResult result = new RestApiResult();
            result.status = "0";

            try
            {
                var endPoint = "";
                if(currentEvent.gateID == 1)
                    endPoint = ConfigurationUtil.getConfigFromFile<EkioskJobPrintVo>(AppConstant.EKIOSK_JOB_PRINT).gate1;
                else if (currentEvent.gateID == 2)
                    endPoint = ConfigurationUtil.getConfigFromFile<EkioskJobPrintVo>(AppConstant.EKIOSK_JOB_PRINT).gate2;
                else if (currentEvent.gateID == 3)
                    endPoint = ConfigurationUtil.getConfigFromFile<EkioskJobPrintVo>(AppConstant.EKIOSK_JOB_PRINT).gate3;
                else if (currentEvent.gateID == 4)
                    endPoint = ConfigurationUtil.getConfigFromFile<EkioskJobPrintVo>(AppConstant.EKIOSK_JOB_PRINT).gate4;
                else if (currentEvent.gateID == 5 && currentEvent.line == AppConstant.LINE_IN)
                    endPoint = ConfigurationUtil.getConfigFromFile<EkioskJobPrintVo>(AppConstant.EKIOSK_JOB_PRINT).gate5In;
                else if (currentEvent.gateID == 5 && currentEvent.line == AppConstant.LINE_OUT)
                    endPoint = ConfigurationUtil.getConfigFromFile<EkioskJobPrintVo>(AppConstant.EKIOSK_JOB_PRINT).gate5Out;

                string resultStr = RestApiUtil.hitCdpApi(endPoint, jsonParam, visual, "Ekisok Printing");
                result.status = "1";
                result.message = resultStr;
                TextViewUtil.appendText(visual.txt_csv, "--- end hitting ekiosk api with success");
            }
            catch (Exception e)
            {
                TextViewUtil.appendText(visual.txt_csv, "--- end hitting ekiosk api with failed cause : " + e.Message);
            }

        }
    }
}
