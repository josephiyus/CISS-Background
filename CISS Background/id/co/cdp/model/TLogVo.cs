using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CISS.id.co.cdp.model.based;
using CISS_Background.id.co.cdp.common.attribute;

namespace CISS.id.co.cdp.model
{
    [Table(tableName = "t_log", schemaName = "tobackup")]
    public class TLogVo : SecurosBase
    {
        [Field(name = "tid", tableId = true, type = typeof(Int64))]
        public Int64? tid;

        [Field(name = "plate_recognized", type = typeof(string))]
        public string plate_recognized;

        [Field(name = "lpr_id", type = typeof(string))]
        public string lpr_id;        
    }
}
