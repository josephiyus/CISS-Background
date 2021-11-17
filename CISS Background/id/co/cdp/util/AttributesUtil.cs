using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using CISS_Background.id.co.cdp.common.attribute;

namespace CISS_Background.id.co.cdp.util
{
    public static class AttributesUtil
    {
        public static List<string> getTableFieldNames<T>() 
        {
            return (from info in typeof(T).GetFields()
                        .Where(i => getFieldAttribute<T>(i.Name).autoIncrement == false)
                    select getFieldAttribute<T>(info.Name).name).ToList();
        }

        public static List<string> getAllTableFieldNames<T>()
        {
            return (from info in typeof(T).GetFields()
                    select getFieldAttribute<T>(info.Name).name).ToList();
        }

        public static List<string> getMembersName<T>()
        {
            return (from info in typeof(T).GetFields()
                        .Where(i => getFieldAttribute<T>(i.Name).autoIncrement == false)
                    select info.Name).ToList();
        }

        public static List<string> getAllMembersName<T>()
        {
            return (from info in typeof(T).GetFields()
                    select info.Name).ToList();
        }

        public static object getMemberValue<T>(T objRefference, string memberName)
        {
            Type type = typeof(T);
            return type.GetFields().Where(fieldInfo => fieldInfo.Name == memberName).FirstOrDefault().GetValue(objRefference);
        }

        public static void setMemberValue<T>(T objRefference, string memberName, object memberValue)
        {
            try
            {
                Type type = typeof(T);
                type.GetFields().Where(fieldInfo => fieldInfo.Name == memberName).FirstOrDefault()
                    .SetValue(objRefference, memberValue);
            }
            catch
            { }
        }

        public static TableAttribute getTableAttribute<T>() 
        {
            Type type = typeof(T);
            return (TableAttribute)type.GetCustomAttributes(true)[0];
        }

        public static FieldAttribute getFieldAttribute<T>(string memberName)
        {
            Type type = typeof(T);
            FieldInfo fieldInfo = type.GetFields().Where(item => item.Name == memberName).FirstOrDefault();
            return (FieldAttribute)fieldInfo.GetCustomAttributes(typeof(FieldAttribute), false)[0];
        }
    }
}
