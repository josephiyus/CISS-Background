using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISS_Background.id.co.cdp.common.attribute
{
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class FieldAttribute : System.Attribute
    {
        public string name;

        public bool autoIncrement;

        public bool tableId;

        public bool nullable;

        public bool updatable;

        public bool dateSystem;

        public Type type;
    }
}
