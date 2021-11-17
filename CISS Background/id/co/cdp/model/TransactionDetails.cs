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
    [Table(tableName = "tb_r_transaction_details", schemaName = "cdp")]
    class TransactionDetails : CissBase
    {
        [Field(name = "id", tableId = true, type = typeof(Int64))]
        public Int64? id;

        [Field(name = "seq", tableId = true, type = typeof(int))]
        public int? seq;

        [Field(name = "status", type = typeof(int))]
        public int? status;

        [Field(name = "trans_date_start", dateSystem = true, type = typeof(DateTime))]
        public DateTime? transDateStart;

        [Field(name = "trans_date_end", dateSystem = true, type = typeof(DateTime), updatable = true)]
        public DateTime? transDateEnd;
    }
}
