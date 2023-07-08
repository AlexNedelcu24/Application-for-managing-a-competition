using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Net.Mime;

using log4net;
using log4net.Util;
using model;
using persistence;
using persistence.Database;
using persistence.Utils;

namespace persistence.Database
{
    public class UserDbRepository
        : InterfaceUser
    {
        private readonly DbUtils DbUtils;
        private static readonly ILog Logger = LogManager.GetLogger("UserDbRepository");

        private IDictionary<string, string> props;
        public UserDbRepository(IDictionary<string, string> props)
        {
            this.props = props;
        }


        public User Save(User user)
        {
            Logger.InfoFormat("saving {0}", user);
            using (var com = DbUtils.GetConnection(props).CreateCommand())
            {
                com.CommandText =
                    "insert into User (username, password) values (@username, @password)";

                var paramUsername = com.CreateParameter();
                paramUsername.ParameterName = "@username";
                paramUsername.Value = user.Username;
                com.Parameters.Add(paramUsername);
                
                var paramPassword = com.CreateParameter();
                paramPassword.ParameterName = "@password";
                paramPassword.Value = user.Password;
                com.Parameters.Add(paramPassword);
                
                
                //ApplicationConfiguration.Initialize();
                //MediaTypeNames.Application.Run(new Form1());
                
                var result = com.ExecuteNonQuery();
                if (result == 0)
                {
                    Logger.InfoExt(null);
                }
                
            }

            Logger.InfoExt("Exit");
            return user;
        }
        
        
        public User Delete(User entity) {
            throw new NotImplementedException();
        }

        public IEnumerable<User> FindAll() {
            throw new NotImplementedException();
        }

        public User FindOne(int integer) {
            throw new NotImplementedException();
        }

        
        public User FindLog(string username, string password) 
        {
            Logger.InfoFormat("Entering FindLog with value {0},{1}", username,password);
            using (var com = DbUtils.GetConnection(props).CreateCommand())
            {
                

                com.CommandText = "select * from User where username = @username and password = @password";
                    
                var paramUsername = com.CreateParameter();
                paramUsername.ParameterName = "@username";
                paramUsername.Value = username;
                com.Parameters.Add(paramUsername);
                
                var paramPassword = com.CreateParameter();
                paramPassword.ParameterName = "@password";
                paramPassword.Value = password;
                com.Parameters.Add(paramPassword);
                
                using (var dataR = com.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id = dataR.GetInt32(dataR.GetOrdinal("id"));
                        string username2 = dataR.GetString(dataR.GetOrdinal("username"));
                        string password2 = dataR.GetString(dataR.GetOrdinal("password"));
                        
                        User user = new User(username2, password2);
                        user.ID = id;

                        return user;
                    }
                }

                Logger.InfoFormat("Exiting findLog");
                return null;
            }
        }
        
        
        
        
    }
}

