using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using persistence.Utils;
namespace persistence.Utils.Connection
{
    internal class SQLiteConnectionFactory : ConnectionFactory
    {
        public override IDbConnection CreateConnection(IDictionary<string, string> props)
        {
            string connectionString = props["ConnectionString"];
            return new SQLiteConnection(connectionString);
        }
    }
}

