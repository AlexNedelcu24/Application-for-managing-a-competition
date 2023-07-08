using System;
using System.Collections.Generic;
using System.Windows.Forms;

using model;
using services;

namespace client
{
    public class ClientCtrl: IObserver
    {
        public event EventHandler<UserEventArgs> updateEvent; //ctrl calls it when it has received an update
        private readonly IServices server;
        private User currentUser;
        public ClientCtrl(IServices server)
        {
            this.server = server;
            currentUser = null;
        }

        public void login(String username, String pass)
        {
            User user=new User(username,pass);
            server.Login(user,this);
            Console.WriteLine("Login succeeded ....");
            currentUser = user;
            Console.WriteLine("Current user {0}", currentUser);
        }

        public void competitorAdded(Competitor competitor)
        {
            String mess = "competitor" + competitor.ToString();
            UserEventArgs userArgs = new UserEventArgs(UserEvent.NewCompetitor,mess);
            Console.WriteLine("Competitor Added");
            OnUserEvent(userArgs);
        }
       /* public void messageReceived(Message message)
        {
            String mess = "[" + message.Sender.Id + "]: " + message.Text;
            ChatUserEventArgs userArgs=new ChatUserEventArgs(ChatUserEvent.NewMessage,mess);
            Console.WriteLine("Message received");
            OnUserEvent(userArgs);
        }*/

        public void logout()
        {
            Console.WriteLine("Ctrl logout");
            server.Logout(currentUser, this);
            currentUser = null;
        }

        protected virtual void OnUserEvent(UserEventArgs e)
        {
            if (updateEvent == null) return;
            updateEvent(this, e);
            Console.WriteLine("Update Event called");
        }
       

       public IEnumerable<Challenge> FindAllChallenges()
       {
           IEnumerable<Challenge> l = new List<Challenge>();
           l = server.FindAllChallenges();
           return l;
       }

       public List<Competitor> FindCompetitors(string challengeName, string category)
        {
            List<Competitor> l  = new List<Competitor>();
            l = server.FindCompetitors(challengeName, category);
            return l;
        }

        public void SaveCompetitor(string name, int age, List<int> challengeList)
        {
            String mess = "new competitor" + name;

            UserEventArgs userArgs = new UserEventArgs(UserEvent.NewCompetitor, mess);
            OnUserEvent(userArgs);
            server.SaveCompetitor(name,age,challengeList);
        }

    }
}
