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
    public class CompetitorDbRepository : InterfaceCompetitor
    {
        private readonly DbUtils DbUtils;
        private static readonly ILog Logger = LogManager.GetLogger("CompetitorDbRepository");
        private IDictionary<string, string> props;
        
        public CompetitorDbRepository(IDictionary<string, string> props)
        {
            this.props = props;
        }
        
        public Competitor Save(Competitor competitor)
        {
            Logger.InfoFormat("saving {0}", competitor);
            using (var com = DbUtils.GetConnection(props).CreateCommand())
            {
                com.CommandText =
                    "insert into Competitor (name, age, challenge_list) values (?,?,?)";
                
                var paramName = com.CreateParameter();
                paramName.ParameterName = "@name";
                paramName.Value = competitor.Name;
                com.Parameters.Add(paramName);
                
                var paramAge = com.CreateParameter();
                paramAge.ParameterName = "@age";
                paramAge.Value = competitor.Age;
                com.Parameters.Add(paramAge);
                
                var paramList = com.CreateParameter();
                paramList.ParameterName = "@challenge_list";
                paramList.Value = string.Join(",", competitor.ChallengeList);
                com.Parameters.Add(paramList);

                var result = com.ExecuteNonQuery();
                if (result == 0)
                {
                    Logger.InfoExt(null);
                }
                
            }

            Logger.InfoExt("Exit");
            return competitor;
        }
        
        
        public Competitor Delete(Competitor entity) {
            throw new NotImplementedException();
        }

        public IEnumerable<Competitor> FindAll() {
            throw new NotImplementedException();
        }

        public Competitor FindOne(int integer) {
            throw new NotImplementedException();
        }
        
        
        public List<Competitor> FindByChallenge(int id) 
        {
            Logger.InfoFormat("Entering FindByChallenge with value {0}", id);
            using (var com = DbUtils.GetConnection(props).CreateCommand())
            {
                List<Competitor> competitors = new List<Competitor>();

                com.CommandText = "select * from Competitor where challenge_list like ('%' || @id || '%') ";
                    
                var paramId = com.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = id;
                com.Parameters.Add(paramId);
                
                using (var dataR = com.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id1 = dataR.GetInt32(dataR.GetOrdinal("id"));
                        string name = dataR.GetString(dataR.GetOrdinal("name"));
                        int age = dataR.GetInt32(dataR.GetOrdinal("age"));
                        string challenge_list = dataR.GetString(dataR.GetOrdinal("challenge_list"));
                        string[] numArray = challenge_list.Replace("[", "").Replace("]", "").Split(',');
                        List<int> numList = new List<int>();
                        foreach (string num in numArray)
                        {
                            numList.Add(int.Parse(num.Trim()));
                        }
                        Competitor competitor = new Competitor(name, age, numList);
                        competitor.ID = id1;
                        competitors.Add(competitor);
                    }
                }

                Logger.InfoFormat("Exiting findByChallenge");
                return competitors;
            }
        }

        
        
        
    }
}