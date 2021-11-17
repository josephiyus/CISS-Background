using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISS_Background.id.co.cdp.constant
{
    public static class AppConstant
    {
        public const string IP_GATE_1 = "10.0.3.12";
        public const string IP_GATE_2 = "10.0.3.13";
        public const string IP_GATE_3 = "10.0.3.11";
        public const string IP_GATE_4 = "10.0.3.10";
        public const string DOOR_NAME_GATE_5_IN = "TIMBANGAN IN";
        public const string DOOR_NAME_GATE_5_OUT = "TIMBANGAN OUT1";
        public const string DOOR_NAME_GATE_5_OUT_2 = "TIMBANGAN_OUT2";
        public const string LINE_TYPE = "lineType";
        public const string LINE = "line";
        public const string GATE_INDEX = "gateIdx";
        public const string CONTAINER_NO = "CONTAINER NO";
        public const string IS_COUNTED_CONTAINER = "IS_COUNTED_CONTAINER";
        public const string CA = "CA";
        public const string CB = "CB";
        public const string EMPTY_CONTAINER = "00000000000";
        public const string AUTOGATE_VALIDATION = "AUTOGATE_VALIDATION";
        public const string DOSS_VALIDATION_MESSAGE = "DOSS_VALIDATION_MESSAGE";
        public const string SYSTEM_DATE = "NOW()";
        public static DateTime CURRENT_REFRESH_TIME = new DateTime();
        public const string APP_SCHEMA = "new_dp";
        public const string SECUROS_SCHEMA = "tobackup";

        public const string ASC = "asc";
        public const string DESC = "desc";

        public const string SOCKET_CONFIG = "socket_config.txt";
        public const string SECUROS_DB_CONFIG = "securos_db_config.txt";
        public const string P1_MONITORING_DB_CONFIG = "p1_monitoring_db_config.txt";
        public const string API_CONFIG = "api_config.txt";
        public const string EKIOSK_JOB_PRINT = "ekiosk_job_print.txt";
        public const string P1_FILE_CONFIG = "p1_file_config.txt";
        public const string DUMMY_CONFIG = "dummy_decision_config.txt";

        public const string ENABLE_DATETIME_MYSQL_C = "convert zero datetime=True";
        public const string SUCCESS = "success";

        public const string LINE_IN = "IN";
        public const string LINE_OUT = "OUT";
        public const string LINE_5 = "L5";
    }
}
