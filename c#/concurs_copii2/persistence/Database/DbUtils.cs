using System;
using System.Collections.Generic;
using log4net;
using log4net.Util;
using System.Data;
using persistence.Utils;
using persistence.Utils.Connection;

namespace persistence.Database
{
    internal class DbUtils
    {
        private static IDbConnection instance = null;

        private static readonly ILog log = LogManager.GetLogger("");
        private DbUtils()
        {

        }

        public static IDbConnection GetConnection(IDictionary<string, string> props)
        {
            if (instance == null || instance.State == ConnectionState.Closed)
            {
                instance = GetNewConnection(props);
                instance.Open();
            }

            return instance;
        }

        private static IDbConnection GetNewConnection(IDictionary<string, string> props)
        {
            log.Info("Connecting to database ...");
            
            return persistence.Utils.Connection.ConnectionFactory.GetInstance().CreateConnection(props);
        }
    }
}