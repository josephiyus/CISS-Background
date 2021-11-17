using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CISS_Background.id.co.cdp.vo;

namespace CISS_Background.id.co.cdp.bo
{
    interface IEventProcessor
    {
        void setLockerLine(object locker);

        void setProcessedEvent(Event ev);

        void process();
    }
}
