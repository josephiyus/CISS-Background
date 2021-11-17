using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace CISS_Background.id.co.cdp.common.persistence
{
    public interface ICommonDatabase
    {
        string getServer();

        object getConnection();

        void insert<T>(T newInsertedData);

        List<T> get<T>(T newInsertedData, string orderField, string sortType, int? limit, string[] additionalCondition);

        T getOneOrDefault<T>(T newInsertedData, string orderField, string sortType, string[] additionalCondition);

        long getCount<T>(T whereObj,string[] addedCondition);

        object update<T>(T newInsertedData);

        bool isEnableConnection();
    }
}
