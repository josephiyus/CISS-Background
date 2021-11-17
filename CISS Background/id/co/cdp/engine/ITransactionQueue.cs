using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CISS_Background.id.co.cdp.bo;

namespace CISS_Background.id.co.cdp.engine
{
    interface ITransactionQueue
    {
        void setGateIndex(int gateIndex);

        void startWorking();

        void stopWorking();

        int getGateIndex();

        void addQueue(IEventProcessor transaction);
    }
}
