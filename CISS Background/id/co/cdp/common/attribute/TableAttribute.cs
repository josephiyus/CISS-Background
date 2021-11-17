using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISS_Background.id.co.cdp.common.attribute
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class TableAttribute : System.Attribute
    {
        public string tableName;

        public string schemaName;
    }
}
