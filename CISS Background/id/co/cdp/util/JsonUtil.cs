using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISS_Background.id.co.cdp.util
{
    public static class JsonUtil
    {
        public static string getJsonFormat<Z>(Z input) 
        {
            string result = null;
            foreach (var memberName in AttributesUtil.getAllMembersName<Z>())
            {
                string memberValue = AttributesUtil.getMemberValue<Z>(input, memberName).ToString();
                
                string jsonValue = @"""{0}"": ""{1}""";
                if (result == null) result = string.Format(jsonValue, memberName, memberValue);
                else result += @"," + string.Format(jsonValue, memberName, memberValue);
            }
            return "{" + result + "}";
        }
    }
}
