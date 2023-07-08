using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using model;
namespace services
{
    public interface IServices
    {
        //User findLog(String username, String password)  throws SQLException, Exceptions;
        IEnumerable<Challenge> FindAllChallenges();
        List<Competitor> FindCompetitors(string challengeName, string category);
        void SaveCompetitor(string name, int age, List<int> challengeList);
        void Login(User user, IObserver client);
        void Logout(User user, IObserver client);
    }
}
