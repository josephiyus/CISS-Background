using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISS.id.co.cdp.exception
{
    class CISSDbFailedConnectionException : Exception
    {
        public CISSDbFailedConnectionException() { }

        public CISSDbFailedConnectionException(string name)
            : base(String.Format("Db Connection Failed for : {0}", name))
        {

        }
    }
}
