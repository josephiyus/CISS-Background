using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CISS_Background.id.co.cdp.common.attribute;
using CISS.id.co.cdp.model.based;

namespace CISS.id.co.cdp.vo
{
    [Table(tableName = "schedule_dummy", schemaName = "cdp")]
    public class TruckScheduleVo : CissBase
    {
        [Field(name = "truck_no", type = typeof(string))]
        public string truck_no;

        [Field(name = "container_no", type = typeof(string))]
        public string container_no;

        [Field(name = "rfid", type = typeof(string))]
        public string rfid;

        [Field(name = "gate_id", type = typeof(int))]
        public int gate;

        [Field(name = "weight", type = typeof(int))]
        public int weight;

        [Field(name = "created_at", dateSystem = true, type = typeof(DateTime))]
        public DateTime? created_at;

        [Field(name = "updated_at", dateSystem = true, type = typeof(DateTime))]
        public DateTime? updated_at;
    }
}
