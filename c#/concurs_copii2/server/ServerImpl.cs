using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using model;
using persistence;
using services;

namespace server
{
    public class ServerImpl: IServices
    {
        private InterfaceUser userRepo;
        private InterfaceChallenge challengeRepo;
        private InterfaceCompetitor competitorRepo;
        private readonly IDictionary<string, IObserver> loggedClients;

        public ServerImpl(InterfaceUser userRepo, InterfaceChallenge challengeRepo, InterfaceCompetitor competitorRepo)
        {
            this.userRepo = userRepo;
            this.challengeRepo = challengeRepo;
            this.competitorRepo = competitorRepo;
            loggedClients = new Dictionary<string, IObserver>();
        }

        public  void Login(User user, IObserver client)  {
            User userOk=userRepo.FindLog(user.Username,user.Password);
            if (userOk!=null){
                if(loggedClients.ContainsKey(user.Username))
                    throw new Exceptions("User already logged in.");
                else
                {
                    loggedClients[user.Username] = client;
                }
            }else
                throw new Exceptions("Authentication failed.");

        }
        
        public  void Logout(User user, IObserver client) {
            IObserver localClient=loggedClients[user.Username];
            if (localClient==null)
                throw new Exceptions("User "+user.ID+" is not logged in.");
            loggedClients.Remove(user.Username);
        }

        public IEnumerable<Challenge> FindAllChallenges()
        {
            return challengeRepo.FindAll();
        }

        public List<Competitor> FindCompetitors(string challengeName, string category)
        {
            int id = challengeRepo.FindByNameAndCategory(challengeName, category);

            return competitorRepo.FindByChallenge(id);
        }

        public void SaveCompetitor(string name, int age, List<int> challengeList)
        {
            Competitor com = new Competitor(name, age, challengeList);
            Competitor competitor = competitorRepo.Save(com);

            foreach (int id in challengeList)
            {
                challengeRepo.UpdateEnrolled(id);
            }

            // Notify all observers about the new competitor
            NotifyCompetitorAdded(competitor);
        }

        private void NotifyCompetitorAdded(Competitor competitor)
        {
            foreach (IObserver observer in loggedClients.Values)
            {
                try
                {
                    Task.Run(() => observer.competitorAdded(competitor));
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"Error notifying observer: {e}");
                }
            }
        }

    }
}