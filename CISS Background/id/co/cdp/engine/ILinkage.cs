using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace CISS_Background.id.co.cdp.engine
{
    interface ILinkage
    {       
        void start();

        void stop();
    }
}
