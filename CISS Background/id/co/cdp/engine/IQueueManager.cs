using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CISS_Background.id.co.cdp.vo;

namespace CISS_Background.id.co.cdp.engine
{
    interface IQueueManager
    {
        void registerTransaction(Event gateEvent);

        void stopAllWorking();

        void startAllWorking();
    }
}
