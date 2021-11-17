using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISS.id.co.cdp.exception
{
    class SecurosDbFailedConnectionException : Exception
    {
        public SecurosDbFailedConnectionException() { }

        public SecurosDbFailedConnectionException(string name)
            : base(String.Format("Db Connection Failed for : {0}", name))
        {

        }
    }
}
