using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;

using log4net;
using log4net.Util;
using model;
using persistence.Utils;

namespace persistence.Database
{
    public class ChallengeDbRepository : InterfaceChallenge
    {
        private readonly DbUtils DbUtils;
        private static readonly ILog Logger = LogManager.GetLogger("ChallengeDbRepository");
        private IDictionary<string, string> props;
        
        public ChallengeDbRepository(IDictionary<string, string> props)
        {
            this.props = props;
        }

        public void UpdateEnrolled(int id)
        {
            using (var com = DbUtils.GetConnection(props).CreateCommand())
            {
                com.CommandText = "update Challenge set enrolled= enrolled+1 where id= @id";

                var paramId = com.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                com.Parameters.Add(paramId);
                
                var result = com.ExecuteNonQuery();
                if (result == 0)
                {
                    Logger.InfoExt(null);
                }
            }

            Logger.InfoExt("Exit");
        }
        
        
        public int FindByNameAndCategory(String challenge_name, String category) {
            Logger.InfoFormat("Entering FindByNameAndCategory with value {0} and {1}", challenge_name, category);
            using (var com = DbUtils.GetConnection(props).CreateCommand())
            {
                int id = 0;
                com.CommandText = "select * from Challenge where name = @name and category = @category";
                var paramName = com.CreateParameter();
                paramName.ParameterName = "@name";
                paramName.Value = challenge_name;
                com.Parameters.Add(paramName);
                
                var paramCategory = com.CreateParameter();
                paramCategory.ParameterName = "@category";
                paramCategory.Value = category;
                com.Parameters.Add(paramCategory);
                
                using (var dataR = com.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        id = dataR.GetInt32(dataR.GetOrdinal("id"));
                    }
                }
                
                
                Logger.InfoFormat("Exiting findByNameAndCategory");
                return id;
            }
        }

        public IEnumerable<Challenge> FindAll()
        {
            Logger.InfoFormat("Entering FindAll");
            
            IList<Challenge> challenges = new List<Challenge>();
            using (var comm = DbUtils.GetConnection(props).CreateCommand())
            {
                comm.CommandText = "select * from Challenge";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id = dataR.GetInt32(dataR.GetOrdinal("id"));
                        string name = dataR.GetString(dataR.GetOrdinal("name"));
                        string category = dataR.GetString(dataR.GetOrdinal("category"));
                        int enrolled = dataR.GetInt32(dataR.GetOrdinal("enrolled"));
                        Challenge challenge = new Challenge(name,category,enrolled);
                        challenges.Add(challenge);
                    }
                }
            }

            Logger.InfoFormat("Exiting findAll");
            return challenges;
        }
        
        
        public Challenge Delete(Challenge entity) {
            throw new NotImplementedException();
        }

        public Challenge FindOne(int integer) {
            throw new NotImplementedException();
        }
        
        public Challenge Save(Challenge entity) {
            throw new NotImplementedException();
        }
        
        
    }
}