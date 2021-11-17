using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISS.id.co.cdp.exception
{
    class FailedWriteViewException : Exception
    {
        public FailedWriteViewException() { }

        public FailedWriteViewException(string name)
            : base(String.Format("Failed for : {0}", name))
        {

        }
    }
}
