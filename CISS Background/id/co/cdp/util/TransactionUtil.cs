using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CISS_Background.id.co.cdp.vo;
using CISS_Background.id.co.cdp.constant;
using System.Windows.Forms;

namespace CISS_Background.id.co.cdp.util
{
    public static class TransactionUtil
    {
        public static TransactionClassification getGateInfo(Event param) 
        {
            TransactionClassification result = new TransactionClassification();
            if (param.CTRLIP == AppConstant.IP_GATE_4)
            {
                result.lineType = "IN";
                result.line = "L1";
                result.gateIndex = 4;
                result.accessGroup = "\"ACS4IN\"";
            }
            else if (param.CTRLIP == AppConstant.IP_GATE_3)
            {
                result.lineType = "IN";
                result.line = "L2";
                result.gateIndex = 3;
                result.accessGroup = "\"ACS4IN\"";
            }
            else if (param.CTRLIP == AppConstant.IP_GATE_1)
            {
                result.lineType = "OUT";
                result.line = "L1";
                result.gateIndex = 1;
                result.accessGroup = "\"ACS4OUT\"";
            }
            else if (param.CTRLIP == AppConstant.IP_GATE_2)
            {
                result.lineType = "OUT";
                result.line = "L2";
                result.gateIndex = 2;
                result.accessGroup = "\"ACS4OUT\"";
            }
            else if (param.DEVNAME == AppConstant.DOOR_NAME_GATE_5_IN)
            {
                result.lineType = "IN";
                result.line = "L5";
                result.gateIndex = 5;
                result.accessGroup = "\"TIMBANGAN\"";
            }
            else if (param.DEVNAME == AppConstant.DOOR_NAME_GATE_5_OUT_2)
            {
                result.lineType = "OUT";
                result.line = "L5";
                result.gateIndex = 5;
                result.accessGroup = "\"TIMBANGAN\"";
            }
            else if (param.DEVNAME == AppConstant.DOOR_NAME_GATE_5_OUT)
            {
                result.lineType = "OUT";
                result.line = "L5";
                result.gateIndex = 5;
                result.accessGroup = "\"TIMBANGAN\"";
            }
            return result;
        }

        public static bool isValidTruckVehicle(string plateNo) 
        {
            return StringUtil.classifiedNumbers(plateNo).Equals("CNC");
        }

        public static string generateP1File(Event ev) 
        {
            string fileName = "P1Imp_"
                + ev.containerNo 
                + "_" + ev.STAFFNO 
                + "_" + DateTime.Now.ToString("yyMMddHHmmssffffff") 
                + "_CISS_BACKGROUND.txt";
            string fileNameLog = fileName;

            TransactionClassification tr = getGateInfo(ev);
            string value = tr.lineType
                + "," + ev.STAFFNAME
                + "," + ev.STAFFNO
                + "," + DateTime.Now.AddDays(1).ToString("dd/MM/yyyy")
                + "," + tr.accessGroup
                + ",1,1"
                + "," + tr.line;
            IOConfigVo conf = ConfigurationUtil.getConfigFromFile<IOConfigVo>(AppConstant.P1_FILE_CONFIG);
            if (conf != null && conf.txtPath != null) 
            {
                string path = conf.txtPath;
                if (path.ElementAt(path.Length - 1) != '\\')
                    path += "\\";
                if (FileUtil.isFolder(path + "\\LOG\\"))
                    fileNameLog = path + "\\LOG\\" + fileName;
                if(FileUtil.isFolder(path))
                    fileName = path + fileName;               
            }
            FileUtil.generateFile(fileName, value);
            FileUtil.generateFile(fileNameLog, value);
            return fileName;
        }

        public static string generateCSVFile(Event ev)
        {
            string fileName = string.Format("{0}{1}_{2}_{3}_{4}_{5}_.{9}",
                DateTime.Now.ToString("yyyyMMddHHmmssffffffff"),
                "CSV",
                ev.line,
                ev.lineType,
                ev.containerNo,
                DateTime.Now.ToString("yyyy_MM_dd_HH_mm"),
                "CSV"
                );
            TransactionClassification tr = getGateInfo(ev);
            string value = tr.lineType
                + "," + ev.STAFFNAME
                + "," + ev.STAFFNO
                + "," + DateTime.Now.AddDays(10).ToString("dd/MM/yyyy")
                + "," + tr.accessGroup
                + ",1,1"
                + "," + tr.line;
            IOConfigVo conf = ConfigurationUtil.getConfigFromFile<IOConfigVo>(AppConstant.P1_FILE_CONFIG);
            if (conf != null && conf.txtPath != null)
            {
                string path = conf.txtPath;
                if (path.ElementAt(path.Length - 1) != '\\')
                    path += "\\";
                if (FileUtil.isFolder(path))
                    fileName = path + fileName;
            }
            FileUtil.appendTextFile(fileName, "Type, line, truckno, size, time_best, rfid_card, tr_code, rfid_tap_time, devname");
            FileUtil.appendTextFile(fileName, value);
            return fileName;
        }
    }
}
