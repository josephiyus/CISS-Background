using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CISS_Background.id.co.cdp.model;

namespace CISS_Background.id.co.cdp.vo
{
    class RegisteredTransaction
    {
        public bool existingStatus { get; set; }
        public TransactionHeaders existingTransaction { get; set; }
    }
}
