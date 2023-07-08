using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Configuration;
using System.Threading;
using System.Data.SQLite;
using System.Data;
using model;
using networking;
using persistence;
using persistence.Database;
using persistence.Utils;
using persistence.Utils.Connection;
using services;

namespace server
{
    using server;
    public class StartServer
    {
        static void Main(string[] args)
        {
            IDictionary<string, string> props = new SortedList<string, string>();
            props.Add("ConnectionString", GetConnectionStringByName("competition.sqlite"));
            TestDatabaseConnection(props["ConnectionString"]); // Adăugați această linie pentru a testa conexiunea
            
           // IUserRepository userRepo = new UserRepositoryMock();
          //  IUserRepository userRepo=new UserRepositoryDb();
           // IMessageRepository messageRepository=new MessageRepositoryDb();
            InterfaceUser userRepo=new UserDbRepository(props);
            InterfaceChallenge challengeRepo=new ChallengeDbRepository(props);
            InterfaceCompetitor competitorRepo = new CompetitorDbRepository(props);
            IServices serviceImpl = new ServerImpl(userRepo, challengeRepo, competitorRepo);
            

            // IChatServer serviceImpl = new ChatServerImpl();
            SerialServer server = new SerialServer("127.0.0.1", 55555, serviceImpl);
            server.Start();
            Console.WriteLine("Server started ...");
            //Console.WriteLine("Press <enter> to exit...");
            
            Console.ReadLine();
            
        }
        
        public class SerialServer: ConcurrentServer 
            {
                private IServices server;
                private ClientObjectWorker worker;
                public SerialServer(string host, int port, IServices server) : base(host, port)
                    {
                        this.server = server;
                        Console.WriteLine("SerialServer...");
                }
                protected override Thread createWorker(TcpClient client)
                {
                    worker = new ClientObjectWorker(server, client);
                    return new Thread(new ThreadStart(worker.run));
                }
            }
            
        private static string GetConnectionStringByName(string name)
        {
                // Assume failure.
                string returnValue = null;
        
                // Look for the name in the connectionStrings section.
                    
                   
                    
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
        
                // If found, return the connection string.
                if (settings != null)
                    returnValue = settings.ConnectionString;
        
                return returnValue;
        }
        
        private static void TestDatabaseConnection(string connectionString)
            {
                try
                {
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        Console.WriteLine("Conexiunea la baza de date a fost realizată cu succes!");
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Eroare la conectarea la baza de date: " + ex.Message);
                }
            }
    }

    

    
}
