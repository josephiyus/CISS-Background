using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CISS_Background.id.co.cdp.common.attribute;
using CISS_Background.id.co.cdp.constant;
using CISS_Background.id.co.cdp.util;
using CISS.id.co.cdp.model.based;

namespace CISS_Background.id.co.cdp.model
{
    [Table(tableName = "tb_r_transaction_headers", schemaName = "cdp")]
    class TransactionHeaders : CissBase
    {
        public TransactionHeaders()
        {
            // TODO: Complete member initialization
        }

        public TransactionHeaders(int? gateId, string platNo, string rfid)
        {
            this.gateId = gateId;
            this.platNo = platNo;
            this.rfid = rfid;
        }

        [Field(name = "id", tableId = true, type = typeof(Int64), autoIncrement = true)]
        public Int64? id;

        [Field(name = "securos_id", type = typeof(Int64), updatable=true)]
        public Int64? securos_id;

        [Field(name = "gate_id", type = typeof(int))]
        public int? gateId;

        [Field(name = "error_id", type = typeof(int), updatable = true)]
        public int? errMsgId;

        [Field(name = "container_weight", type = typeof(int), updatable = true)]
        public int? weight;

        [Field(name = "log_id", type = typeof(int))]
        public long? logId;        

        [Field(name = "plate_no", type = typeof(string))]
        public string platNo;

        [Field(name = "rfid", type = typeof(string))]
        public string rfid;

        [Field(name = "tapping_date", type = typeof(DateTime))]
        public DateTime? tappingDate;

        [Field(name = "container_ocr", type = typeof(string), updatable=true)]
        public string containerOcr;

        [Field(name = "container_valid", type = typeof(string), updatable = true)]
        public string containerValid;

        [Field(name = "phase_status", type = typeof(int), updatable = true)]
        public int? phaseStatus;

        [Field(name = "manual_status", type = typeof(int), updatable = true)]
        public int? manualStatus;

        [Field(name = "master_status", type = typeof(int), updatable = true)]
        public int? masterStatus;

        [Field(name = "printing_status", type = typeof(int), updatable = true)]
        public int? printingStatus;

        [Field(name = "amount_tapping", type = typeof(int), updatable = true)]
        public int? amountTapping;

        [Field(name = "validation_status", type = typeof(int), updatable = true)]
        public int? validationStatus;

        [Field(name = "xml", type = typeof(string))]
        public string xml;

        [Field(name = "line_type", type = typeof(string))]
        public string lineType;

        [Field(name = "start_date", dateSystem = true, type = typeof(DateTime))]
        public DateTime? startDate;

        [Field(name = "end_date", dateSystem = true, type = typeof(DateTime), updatable = true)]
        public DateTime? endDate;    
    }
}
