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
    [Table(tableName = "tb_h_log_details", schemaName = "cdp")]
    class LogDetailsVo : CissBase
    {
        [Field(name = "id", tableId = true, type = typeof(Int64))]
        public Int64? id;

        [Field(name = "seq", type = typeof(Int64))]
        public Int64? seq;

        [Field(name = "activity_note", type = typeof(string))]
        public string @activityNote;

        [Field(name = "activity_date", dateSystem = true, type = typeof(DateTime))]
        public DateTime? activityDate;
    }
}
