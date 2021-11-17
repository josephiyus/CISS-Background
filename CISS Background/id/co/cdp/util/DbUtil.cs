using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CISS_Background.id.co.cdp.common.attribute;
using MySql.Data.MySqlClient;
using Npgsql;
using System.Windows.Forms;
using CISS.id.co.cdp.exception;

namespace CISS_Background.id.co.cdp.util
{
    public static class DbUtil
    {
        public static List<T> getRetrievedResult<T>(MySqlDataReader reader) 
        {
            List<T> result = new List<T>();
            while (reader.Read())
            {
                result.Add(Activator.CreateInstance<T>());
                T item = result[result.Count - 1];
                foreach (var memberName in AttributesUtil.getAllMembersName<T>())
                {
                    FieldAttribute field = AttributesUtil.getFieldAttribute<T>(memberName);
                    AttributesUtil.setMemberValue<T>(item, memberName, reader[field.name]);
                }
            }
            return result;
        }
        
        public static T getOneRetrievedResult<T>(MySqlDataReader reader)
        {
            List<T> result = new List<T>();
            try
            {
                while (reader.Read())
                {
                    result.Add(Activator.CreateInstance<T>());
                    T item = result[result.Count - 1];
                    foreach (var memberName in AttributesUtil.getAllMembersName<T>())
                    {
                        FieldAttribute field = AttributesUtil.getFieldAttribute<T>(memberName);
                        var val = reader[field.name];
                        if (field.type == typeof(DateTime)) 
                        {
                            //val = DateTimeUtil.parseDateTimeddMMyyyy(val.ToString());
                            val = DateTimeUtil.parseDateTime(val.ToString());
                        }
                        AttributesUtil.setMemberValue<T>(item, memberName, val);
                    }
                }
                if (result.Count > 0)
                {
                    return result[0];
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("err converter = " + e.Message);
            }
            return Activator.CreateInstance<T>();
        }

        public static T getOneRetrievedResult<T>(NpgsqlDataReader reader)
        {
            List<T> result = new List<T>();
            try
            {
                while (reader.Read())
                {
                    result.Add(Activator.CreateInstance<T>());
                    T item = result[result.Count - 1];
                    foreach (var memberName in AttributesUtil.getAllMembersName<T>())
                    {
                        FieldAttribute field = AttributesUtil.getFieldAttribute<T>(memberName);
                        var val = reader[field.name];
                        if (field.type == typeof(DateTime))
                        {
                            //val = DateTimeUtil.parseDateTimeddMMyyyy(val.ToString());
                            val = DateTimeUtil.parseDateTime(val.ToString());
                        }
                        else if (field.type == typeof(Int64))
                        {
                            val = Int64.Parse(val.ToString());
                        }
                        else if (field.type == typeof(Int32))
                        {
                            val = Int32.Parse(val.ToString());
                        }
                        AttributesUtil.setMemberValue<T>(item, memberName, val);
                    }
                }
                if (result.Count > 0)
                {
                    return result[0];
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("err converter = " + e.Message);
            }
            return Activator.CreateInstance<T>();
        }

        public static void openConnection(object con)
        {
            try
            {
                if (con.GetType() == typeof(MySqlConnection))
                {
                    ((MySqlConnection)con).Open();
                }
                else if (con.GetType() == typeof(NpgsqlConnection))
                {
                    ((NpgsqlConnection)con).Open();
                }
            }
            catch (Exception)
            {
                if (con.GetType() == typeof(MySqlConnection))
                {
                    throw new CISSDbFailedConnectionException();
                }
                else if (con.GetType() == typeof(NpgsqlConnection))
                {
                    throw new SecurosDbFailedConnectionException();
                }
            }
        }
    }
}
