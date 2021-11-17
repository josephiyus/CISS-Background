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
    [Table(tableName = "tb_h_log_headers", schemaName = "cdp")]
    public class LogHeadersVo : CissBase
    {
        [Field(name = "id", tableId = true, type = typeof(Int64), autoIncrement=true)]
        public Int64? id;

        [Field(name = "status", type = typeof(int), updatable=true)]
        public int? status;

        [Field(name = "gate_id", type = typeof(int))]
        public int? gateId;

        [Field(name = "start_date", dateSystem = true, type = typeof(DateTime))]
        public DateTime? startDate;

        [Field(name = "end_date", dateSystem = true, type = typeof(DateTime), updatable=true)]
        public DateTime? endDate;

        public LogHeadersVo() { }

        public LogHeadersVo(int gateId, int status)
        {
            this.gateId = gateId;
            this.status = status;
        }

        public LogHeadersVo(Int64 id, int status) 
        {
            this.id = id;
            this.status = status; 
        }
    }
}
