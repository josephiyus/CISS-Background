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
    [Table(tableName = "vw_transaction_gate_info", schemaName = "tobackup")]
    public class RecognitionInfoVo : SecurosBase
    {
        [Field(name = "securos_id", tableId = true, type = typeof(Int64))]
        public Int64? sec_id;

        [Field(name = "plate_no_recognized", type = typeof(string))]
        public string plate_no;

        [Field(name = "trans_date", type = typeof(DateTime))]
        public DateTime? transaction_date;

        [Field(name = "trans_code", type = typeof(string))]
        public string transaction_code;

        [Field(name = "line_type", type = typeof(string))]
        public string line_type;

        [Field(name = "line_name", type = typeof(string))]
        public string line;

        [Field(name = "lpr_id", type = typeof(int))]
        public int? lpr_id;        
    }
}
