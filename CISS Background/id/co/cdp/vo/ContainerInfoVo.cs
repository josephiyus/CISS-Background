using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISS_Background.id.co.cdp.vo
{
    public class ContainerInfoVo
    {
        public string container_no { get; set; }
        public Int64? securos_id { get; set; }
        public bool? is_container { get; set; }
        public bool? con_db_status { get; set; }
    }
}
