using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CISS_Background.id.co.cdp.vo;
using CISS.id.co.cdp.vo;
using CISS_Background.id.co.cdp.model;

namespace CISS_Background.id.co.cdp.dao
{
    interface IDefaultDao
    {
        RegisteredTransaction checkRegisteredTransaction(Event param);

        void saveInHistory(Event currentEvent);

        long? registerNewTransaction(Event currentEvent);

        TransactionHeaders registerNewHeaderTransaction(Event currentEvent);

        TransactionHeaders getPrevHeader(int gate, long? headerId);

        long? startLog(Event param, string message);

        void logActivity(long? logId, string message);

        void logActivity(long? logId, int status, string message);

        void setStatus(long? headerId, int? phaseStatus, int? validationStatus, int? errMsgId);

        void setSecurosResult(long? headerId, Int64? securos_id, string no_container);

        long? setMaster(Event currentEvent);

        long? getLastHeaderId(Event currentEvent);

        long? getLogId(Event currentEvent);

        void addTappingAmount(long? nullable);

        void saveSchedule(TruckScheduleVo schedule);

        void updateHeader(TransactionHeaders h);
    }
}
