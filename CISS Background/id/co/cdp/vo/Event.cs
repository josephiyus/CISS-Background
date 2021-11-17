using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISS_Background.id.co.cdp.vo
{
    public class Event
    {
        public string ETYPE { get; set; }
        public string TRDATE { get; set; }
        public string TRTIME { get; set; }
        public string TRCODE { get; set; }
        public string TRDESC { get; set; }
        public string CTRLIP { get; set; }
        public string CTRLNAME { get; set; }
        public string DEVNAME { get; set; }
        public string CARDNO { get; set; }
        public string STAFFNO { get; set; }
        public string STAFFNAME { get; set; }
        public string DEPTNAME { get; set; }
        public string JOBNAME { get; set; }
        public string SHIFTNAME { get; set; }
        public string ZONENAME { get; set; }
        public string POINTNAME { get; set; }
        public int gateID { get; set; }
        public int weight { get; set; }
        public long? logID { get; set; }
        public string xml { get; set; }
        public string containerNo { get; set; }
        public string line { get; set; }
        public string lineType { get; set; }
    }
}
