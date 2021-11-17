using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISS_Background.id.co.cdp.vo
{
    class DossParamVo
    {
        public string container_no;
        public string rfid;
        public string tline;

        public DossParamVo(string container_no, string rfid, string tline)
        {
            this.container_no = container_no;
            this.rfid = rfid;
            this.tline = tline;
        }
    }
}
