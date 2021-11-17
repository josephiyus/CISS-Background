using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CISS_Background.id.co.cdp.vo;
using System.Net;
using System.IO;
using System.Threading;
using CISS_Background.id.co.cdp.constant;
using CISS.id.co.cdp.vo;
using Newtonsoft.Json;
using CISS.id.co.cdp.util;

namespace CISS_Background.id.co.cdp.util
{
    public static class RestApiUtil
    {
        public static RestApiResult pushNotifToWeb(long? headerId, int monitoringMsgID, 
            int ekioskMsgID, int status, int inputManualStatus, int tapingSecond, MonitoringFieldVo visual)
        {
            RestApiResult result = new RestApiResult();
            result.status = "0";
            try
            {
                string endPoint =
                    ConfigurationUtil.getConfigFromFile<APIConfigVo>(AppConstant.API_CONFIG).frontend_api_basic
                    + "headerId=" + headerId.ToString()
                    + "&messageId=" + monitoringMsgID.ToString()
                    + "&ekioskMessageId=" + ekioskMsgID.ToString()
                    + "&isSuccess=" + status.ToString()
                    + "&isInputManual=" + inputManualStatus.ToString()
                    + "&tapingSecond=" + tapingSecond.ToString();
                TextViewUtil.appendText(visual.txt_csv, "--- start hitting api ");
                TextViewUtil.appendText(visual.txt_csv, "--- endpoint :  " + endPoint);
                HttpWebRequest http = (HttpWebRequest)HttpWebRequest.Create(endPoint);

                HttpWebResponse response = (HttpWebResponse)http.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    string responseJson = sr.ReadToEnd();
                    TextViewUtil.appendText(visual.txt_csv, "--- result : " + responseJson);
                }
                result.status = "1";
                TextViewUtil.appendText(visual.txt_csv, "--- end hitting api with success");
            }
            catch(Exception e)
            {
                TextViewUtil.appendText(visual.txt_csv, "--- end hitting api with failed cause : " + e.Message);
            }
            return result;
        }

        public static string hitCdpApi(string endPoint, string jsonParam, MonitoringFieldVo visual, string title)
        {
            string resultStr = "";
            var request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = "POST";
            request.Accept = "application/json";
            request.ContentType = "application/json";

            TextViewUtil.appendText(visual.txt_csv, string.Format("--- start hitting {0} Api ", title));
            TextViewUtil.appendText(visual.txt_csv, "--- endpoint :  " + endPoint);

            TextViewUtil.appendText(visual.txt_csv, string.Format("--- {0} Api Start set JSON Param", title));

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(jsonParam);
                TextViewUtil.appendText(visual.txt_csv, string.Format("--- JSON {0} Api Param : {1}", title, jsonParam));
            }

            TextViewUtil.appendText(visual.txt_csv, string.Format("--- {0} Api End  set JSON Param", title));

            var httpResponse = (HttpWebResponse)request.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                resultStr = streamReader.ReadToEnd();
            }

            TextViewUtil.appendText(visual.txt_csv, string.Format("--- {0} Api Result : {1}", title, resultStr));

            return resultStr;
        }       

        public static RestApiResult printToEkiosk(Event currentEvent, MonitoringFieldVo visual)
        {
            RestApiResult result = new RestApiResult();
            result.status = "0";
            try
            {
                string endPoint =
                    (currentEvent.lineType.Equals(AppConstant.LINE_IN) ? 
                    ConfigurationUtil.getConfigFromFile<APIConfigVo>(AppConstant.API_CONFIG).ekiosk_in : 
                    ConfigurationUtil.getConfigFromFile<APIConfigVo>(AppConstant.API_CONFIG).ekiosk_out);
                var request = (HttpWebRequest)WebRequest.Create(endPoint);
                request.Method = "POST";
                request.Accept = "application/json";
                request.ContentType = "application/json";

                TextViewUtil.appendText(visual.txt_csv, "--- start hitting ekiosk api ");
                TextViewUtil.appendText(visual.txt_csv, "--- Ekiosk endpoint :  " + endPoint);
                HttpWebRequest http = (HttpWebRequest)HttpWebRequest.Create(endPoint);

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = JsonUtil.getJsonFormat<EkioskParamVo>(
                        new EkioskParamVo(currentEvent.CARDNO));
                    streamWriter.Write(json);
                    TextViewUtil.appendText(visual.txt_csv, "--- Ekiosk JSON Param : " + json);
                }

                HttpWebResponse response = (HttpWebResponse)http.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    string responseJson = sr.ReadToEnd();
                    TextViewUtil.appendText(visual.txt_csv, "--- Ekiosk result : " + responseJson);
                }
                result.status = "1";
                TextViewUtil.appendText(visual.txt_csv, "--- end hitting ekiosk api with success");
            }
            catch (Exception e)
            {
                TextViewUtil.appendText(visual.txt_csv, "--- end hitting ekiosk api with failed cause : " + e.Message);
            }
            return result;
        }
    }
}
