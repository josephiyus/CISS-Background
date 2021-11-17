using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using CISS_Background.id.co.cdp.constant;
using CISS_Background.id.co.cdp.util;
using CISS_Background.id.co.cdp.vo;
using System.Windows.Forms;
using System.Threading;

namespace CISS_Background.id.co.cdp.common.persistence.abstracts
{
	public class MySqlCommon : ICommonDatabase
	{
        private P1MonitoringDbVo config;

        public MySqlCommon()
        {
            // TODO: Complete member initialization
        }

        public MySqlCommon(P1MonitoringDbVo config)
        {
            this.config = config;
            //AppConstant.APP_SCHEMA = config.schema;
        }

        public static MySqlCommon getInstance() 
        {
            MySqlCommon db = new MySqlCommon(ConfigurationUtil.getConfigFromFile<P1MonitoringDbVo>(AppConstant.P1_MONITORING_DB_CONFIG));
            return db;
        }

        public object getConnection()
        {
            return new MySqlConnection("datasource=" + this.config.data_source
                + ";port=" + this.config.port
                + ";username=" + this.config.username
                + ";password=" + this.config.password);
        }

        public void insert<T>(T insertedData)
        {
            string sql = QueryUtil.getInsertQuery<T>(insertedData);
            MySqlConnection con = (MySqlConnection)getConnection();
            MySqlCommand command = new MySqlCommand(sql, con);

            Monitor.Enter(ThreadLocker._INSERT_SYNCH_LINE);

            DbUtil.openConnection(con);
            command.ExecuteReader();
            con.Close();
            
            //object result = getInsertedId<T>();
            /*
             int amountTrial = 3;
            while (result.ToString().Equals("0")) 
            {
                if (amountTrial == 0) 
                {
                    break;
                }
                result = getInsertedId<T>();
                amountTrial -= 1;
            }
            if (result.ToString().Equals("0"))
                result = insertTrial(insertedData, 3);

             */
            Monitor.Exit(ThreadLocker._INSERT_SYNCH_LINE);

            //return result;
        }

        private object insertTrial<T>(T insertedData, int amountTrial)
        {
            string sql = QueryUtil.getInsertQuery<T>(insertedData);
            MySqlConnection con = (MySqlConnection)getConnection();
            MySqlCommand command = new MySqlCommand(sql, con);
            DbUtil.openConnection(con);
            command.ExecuteReader();
            con.Close();
            object result = getInsertedId<T>();
            if (result.ToString().Equals("0")) 
            {
                if (amountTrial > 0)
                {
                    amountTrial -= 1;
                    return insertTrial(insertedData, amountTrial);
                }
                else 
                {
                    throw new Exception();
                }                
            }
            else
                return result;
        }

        private object getInsertedId<T>()
        {
            string sql = "SELECT LAST_INSERT_ID()";
            MySqlConnection con = (MySqlConnection)getConnection();
            MySqlCommand command = new MySqlCommand(sql, con);

            DbUtil.openConnection(con);
            var result = command.ExecuteScalar();
            con.Close();
            return result;
        }

        public object update<T>(T updatedData)
        {
            int result = 0;
            string sql = QueryUtil.getUpdateQuery<T>(updatedData);
            MySqlConnection con = (MySqlConnection)getConnection();
            MySqlCommand command = new MySqlCommand(sql, con);

            DbUtil.openConnection(con);
            command.ExecuteReader();
            con.Close();
            return result;
        }

        public long getCount<T>(T whereObj, string[] additionalCondition)
        {
            string sql = "";
            if (additionalCondition != null) 
            {
                foreach (var item in additionalCondition)
                {
                    if (Array.IndexOf(additionalCondition, item) > 0)
                        sql += " and " + item;
                    else
                        sql = item;
                }
            }
            sql = QueryUtil.getSelectQuery<T>(whereObj, null, null, null, true, sql);
            MySqlConnection con = (MySqlConnection)getConnection();
            MySqlCommand command = new MySqlCommand(sql, con);

            DbUtil.openConnection(con);
            var result = command.ExecuteScalar();
            con.Close();

            return Int64.Parse(result.ToString());
        }

        public List<T> get<T>(T whereObj, string orderField, string sortType, int? limit, string[] additionalCondition)
        {
            string sql = "";
            if (additionalCondition != null)
            {
                foreach (var item in additionalCondition)
                {
                    if (Array.IndexOf(additionalCondition, item) > 0)
                        sql += " and " + item;
                    else
                        sql = item;
                }
            }

            sql = QueryUtil.getSelectQuery<T>(whereObj, orderField, sortType, limit, false, sql);
            MySqlConnection con = (MySqlConnection)getConnection();
            MySqlCommand command = new MySqlCommand(sql, con);

            DbUtil.openConnection(con);
            MySqlDataReader reader = command.ExecuteReader();
            var result = DbUtil.getRetrievedResult<T>(reader);
            con.Close();
            return result;
        }

        public T getOneOrDefault<T>(T whereObj, string orderField, string sortType, string[] additionalCondition)
        {
            string sql = "";
            if (additionalCondition != null)
            {
                foreach (var item in additionalCondition)
                {
                    if (Array.IndexOf(additionalCondition, item) > 0)
                        sql += " and " + item;
                    else
                        sql = item;
                }
            }

            sql = QueryUtil.getSelectOneOrDefaultQuery<T>(whereObj, orderField, sortType, sql);
            MySqlConnection con = (MySqlConnection)getConnection();
            MySqlCommand command = new MySqlCommand(sql, con);
            DbUtil.openConnection(con);
            MySqlDataReader reader = command.ExecuteReader();
            var result = DbUtil.getOneRetrievedResult<T>(reader);
            con.Close();
            return result;
        }


        public bool isEnableConnection()
        {
            bool result = false;
            try
            {
                MySqlConnection con = (MySqlConnection)getConnection();
                MySqlCommand command = new MySqlCommand("select 1", con);

                DbUtil.openConnection(con);
                var resultStr = command.ExecuteScalar();
                result = resultStr.ToString() == "1";
                con.Close();
            }
            catch
            {
                
            }
            return result;
        }

        public string getServer()
        {
            return config.data_source;
        }
    }
}
