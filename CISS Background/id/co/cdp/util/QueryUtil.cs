using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using CISS_Background.id.co.cdp.common.attribute;
using CISS.id.co.cdp.model.based;
using CISS_Background.id.co.cdp.vo;
using CISS_Background.id.co.cdp.constant;

namespace CISS_Background.id.co.cdp.util
{
    public static class QueryUtil
    {
        public static string getSelectQuery<T>(T whereObj, string orderField, string sortType, int? limit, bool countable, string customCondition)
        {
            Type type = typeof(T);
            TableAttribute table = (TableAttribute)type.GetCustomAttributes(true)[0];
            string result = "select " + (countable ? "count(-1)" : String.Join(",", AttributesUtil.getAllTableFieldNames<T>()));
            result += " from " + getTableName<T>(table);
            string whereClause = " where 1=1";

            if (whereObj != null)
            {
                foreach (var memberName in AttributesUtil.getAllMembersName<T>())
                {
                    FieldAttribute field = AttributesUtil.getFieldAttribute<T>(memberName);
                    var memberVal = AttributesUtil.getMemberValue<T>(whereObj, memberName);
                    if (memberVal != null)
                    {
                        if (field.type == typeof(string))
                        {
                            memberVal = "'" + memberVal + "'";
                        }
                        whereClause += " and " + field.name + " = " + memberVal.ToString();
                    }
                }
            }

            result += whereClause + (customCondition != null && customCondition != "" ? " and " + customCondition : "");
            if (countable == false) 
            {
                if (orderField != null)
                {
                    result += " order by " + orderField;
                }

                if (sortType != null)
                {
                    result += " " + sortType;
                }

                if (limit != null)
                {
                    result += " limit " + limit;
                }
            }           

            return result;
        }

        public static string getSelectOneOrDefaultQuery<T>(T whereObj, string orderField, string sortType, string customCondition)
        {
            return getSelectQuery<T>(whereObj, orderField, sortType, 1, false, customCondition);
        }
        
        public static string getInsertQuery<T>(T insertedObj)
        {
            TableAttribute table = AttributesUtil.getTableAttribute<T>();

            string result = "insert into " + getTableName<T>(table) + "(" + String.Join(",", AttributesUtil.getTableFieldNames<T>()) + ")";
            string values = " values(";
            foreach (var memberName in AttributesUtil.getMembersName<T>())
            {
                FieldAttribute field = AttributesUtil.getFieldAttribute<T>(memberName);
                var memberVal = AttributesUtil.getMemberValue<T>(insertedObj, memberName);
                if (memberVal != null)
                {
                    if (field.type == typeof(string))
                    {
                        memberVal = "'" + memberVal.ToString().Replace("'","\"") + "'";
                    }
                    else if (field.type == typeof(DateTime))
                    {
                        memberVal = "STR_TO_DATE('" + ((DateTime)memberVal).ToString("yyyy-MM-dd HH:mm:ss") + "','%Y-%m-%d %H:%i:%s')";
                    }

                    if (values.Length > 8)
                    {
                        values += "," + memberVal;
                    }
                    else
                    {
                        values += memberVal;
                    }
                }
                else
                {
                    if (field.dateSystem && field.type == typeof(DateTime))
                    {
                        values += ",NOW()";
                    }
                    else 
                    {
                        if (values.Length > 8) values += ",null";
                        else values += "null";
                    }
                }
            }

            return result + values + ")";
        }

        public static string getUpdateQuery<T>(T updatedObj)
        {
            TableAttribute table = AttributesUtil.getTableAttribute<T>();
            string basicClause = "update " + getTableName<T>(table);
            string setClause = " set";
            string whereClause = " where";
            foreach (var memberName in AttributesUtil.getAllMembersName<T>())
	        {
                FieldAttribute field = AttributesUtil.getFieldAttribute<T>(memberName);
                object value = AttributesUtil.getMemberValue<T>(updatedObj, memberName);
                                
                if (field.dateSystem && field.type == typeof(DateTime))
                {
                    value = "NOW()";
                }
                else if (field.type == typeof(string))
                {
                    value = "'" + value + "'";
                }
                else if (field.type == typeof(DateTime)) 
                {
                    value = "STR_TO_DATE('" + value + "','%Y-%m-%d %H:%i:%s')";
                }

                if (field.tableId) 
                {
                    if (whereClause.Length > 6)
                    {
                        whereClause += " and " + field.name + " = " + value;
                    }
                    else 
                    {
                        whereClause += " " + field.name + " = " + value;
                    }
                }
                else if (field.updatable && value != null && value.ToString() != "''") 
                {
                    if (setClause.Length > 4)
                    {
                        setClause += "," + field.name + " = " + value;
                    }
                    else
                    {
                        setClause += " " + field.name + " = " + value;
                    }
                }
	        }
            
            return basicClause + setClause + whereClause;
        }

        private static string getTableName<T>(TableAttribute table)
        {
            string schema = null;
            if (typeof(CissBase).IsAssignableFrom(typeof(T)))
                schema = ConfigurationUtil.getConfigFromFile<P1MonitoringDbVo>(AppConstant.P1_MONITORING_DB_CONFIG).schema;
            else if (typeof(SecurosBase).IsAssignableFrom(typeof(T)))
                schema = ConfigurationUtil.getConfigFromFile<SecurosDbConfigVo>(AppConstant.SECUROS_DB_CONFIG).schema;
            return schema == null ? table.tableName : schema + "." + table.tableName;
        }
    }
}
