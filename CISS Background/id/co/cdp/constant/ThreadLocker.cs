using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISS_Background.id.co.cdp.constant
{
    public static class ThreadLocker
    {
        public static object _LOG_LOCKER = new object();

        public static object _INSERT_SYNCH_LINE = new object();

        public static object _VIEW_STATE_SYNCH_LINE = new object();
    }
}
