using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using CISS_Background.id.co.cdp.util;
using CISS_Background.id.co.cdp.vo;
using CISS_Background.id.co.cdp.constant;

namespace CISS_Background.id.co.cdp.common.persistence.abstracts
{
    public class PostgreSqlCommon : ICommonDatabase
    {
        private SecurosDbConfigVo config;

        public PostgreSqlCommon(SecurosDbConfigVo config)
        {
            this.config = config;
        }

        public static PostgreSqlCommon getInstance() 
        {
            PostgreSqlCommon db = new PostgreSqlCommon(ConfigurationUtil.getConfigFromFile<SecurosDbConfigVo>(AppConstant.SECUROS_DB_CONFIG));
            return db;
        }

        public object getConnection()
        {
            string str_con = string.Format("Server={0};Port=5432;Database={1};User ID={2};password={3};Pooling=false",
                this.config.server_name, this.config.database_name, this.config.username, this.config.password);
            return new NpgsqlConnection(str_con);
        }

        public void insert<T>(T newInsertedData)
        {
            throw new NotImplementedException();
        }

        public List<T> get<T>(T newInsertedData, string orderField, string sortType, int? limit, string[] additionalCondition)
        {
            throw new NotImplementedException();
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
            NpgsqlConnection con = (NpgsqlConnection)getConnection();
            NpgsqlCommand command = new NpgsqlCommand(sql, con);

            DbUtil.openConnection(con);

            NpgsqlDataReader reader = command.ExecuteReader();
            var result = DbUtil.getOneRetrievedResult<T>(reader);
            con.Close();
            return result;
        }

        public long getCount<T>(T whereObj, string[] addedCondition)
        {
            throw new NotImplementedException();
        }

        public object update<T>(T newInsertedData)
        {
            throw new NotImplementedException();
        }


        public bool isEnableConnection()
        {
            bool result = false;
            try
            {
                NpgsqlConnection con = (NpgsqlConnection)getConnection();
                NpgsqlCommand command = new NpgsqlCommand("select 1", con);

                con.Open();
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
            return config.server_name;
        }
    }
}
