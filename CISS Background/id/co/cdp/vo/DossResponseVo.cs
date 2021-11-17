using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISS.id.co.cdp.vo
{
    class DossResponseVo
    {
        public string container_no;
        public string rfid;
        public string tLine;

        public DossResponseVo(string container_no, string rfid, string tline)
        {
            this.container_no = container_no;
            this.rfid = rfid;
            this.tLine = tline;
        }
    }
}
