using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CISS.id.co.cdp.vo;

namespace CISS_Background.id.co.cdp.vo
{
    public class DossInfoVo
    {
        public bool validationResult { get; set; }
        public string message { get; set; }
        public int message_error_id { get; set; }
        public TruckScheduleVo schedule { get; set; }
    }
}
