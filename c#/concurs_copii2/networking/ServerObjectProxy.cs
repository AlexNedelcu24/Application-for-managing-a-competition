using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using services;
using model;
using networking;
using networking.dto;
using networking.protocol;

namespace networking{
	
	public class ServerObjectProxy : IServices
	{
		private string host;
		private int port;

		private IObserver client;

		private NetworkStream stream;
		
        private IFormatter formatter;
		private TcpClient connection;

		private Queue<Response> responses;
		private volatile bool finished;
        private EventWaitHandle _waitHandle;
		public ServerObjectProxy(string host, int port)
		{
			this.host = host;
			this.port = port;
			responses=new Queue<Response>();
		}

		public virtual void Login(User user, IObserver client)
		{
			initializeConnection();
			UserDTO udto = DTOUtils.GetDTO(user);
			sendRequest(new LoginRequest(udto));
			Response response =readResponse();
			if (response is OkResponse)
			{
				this.client=client;
				return;
			}
			if (response is ErrorResponse)
			{
				ErrorResponse err =(ErrorResponse)response;
				closeConnection();
				throw new Exceptions(err.Message);
			}
		}

		public virtual void SaveCompetitor(string name, int age, List<int> challengeList)
		{
			Competitor competitor = new Competitor(name, age, challengeList);
			CompetitorDTO cdto =DTOUtils.GetDTO(competitor);
			sendRequest(new SaveCompetitorRequest(cdto));
			Response response =readResponse();
			if (response is ErrorResponse)
			{
				ErrorResponse err =(ErrorResponse)response;
				throw new Exceptions(err.Message);
			}
		}

	public virtual void Logout(User user, IObserver client)
		{
			UserDTO udto =DTOUtils.GetDTO(user);
			sendRequest(new LogoutRequest(udto));
			Response response =readResponse();
			closeConnection();
			if (response is ErrorResponse)
			{
				ErrorResponse err =(ErrorResponse)response;
				throw new Exceptions(err.Message);
			}
		}
	
		public virtual List<Competitor> FindCompetitors(string challengeName, string category)
		{
			Challenge challenge = new Challenge(challengeName, category, 0);
			ChallengeDTO cdto =DTOUtils.GetDTO(challenge);
			sendRequest(new FindCompetitorsRequest(cdto));
			Response response =readResponse();
			if (response is ErrorResponse)
			{
				ErrorResponse err =(ErrorResponse)response;
				throw new Exceptions(err.Message);
			}
			TakeCompetitorsResponse resp =(TakeCompetitorsResponse)response;
			List<CompetitorDTO> ldto =resp.Competitors;
			List<Competitor> f =DTOUtils.GetFromDTO(ldto);
			return f;
		}
		
		public virtual IEnumerable<Challenge> FindAllChallenges()
		{
			sendRequest(new FindAllChallengesRequest());
			Response response =readResponse();
			if (response is ErrorResponse)
			{
				ErrorResponse err =(ErrorResponse)response;
				throw new Exceptions(err.Message);
			}
			TakeChallengesResponse resp =(TakeChallengesResponse)response;
			IEnumerable<ChallengeDTO> ldto =resp.Challenges;
			IEnumerable<Challenge> f =DTOUtils.GetFromDTO(ldto);
			return f;
		}

		private void closeConnection()
		{
			finished=true;
			try
			{
				stream.Close();
			
				connection.Close();
                _waitHandle.Close();
				client=null;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}

		}

		private void sendRequest(Request request)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			
			try
			{
                formatter.Serialize(stream, request);
                stream.Flush();
			}
			catch (Exception e)
			{
				throw new Exceptions("Error sending object "+e);
			}

		}

		private Response readResponse()
		{
			Response response =null;
			try
			{
                _waitHandle.WaitOne();
				lock (responses)
				{
                    //Monitor.Wait(responses); 
                    response = responses.Dequeue();
                
				}
				

			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
			return response;
		}
		private void initializeConnection()
		{
			 try
			 {
				connection=new TcpClient(host,port);
				stream=connection.GetStream();
                formatter = new BinaryFormatter();
				finished=false;
                _waitHandle = new AutoResetEvent(false);
				startReader();
			}
			catch (Exception e)
			{
                Console.WriteLine(e.StackTrace);
			}
		}
		private void startReader()
		{
			Thread tw =new Thread(run);
			tw.Start();
		}


		private void handleUpdate(UpdateResponse update)
		{
			

			if (update is NewCompetitorResponse)
			{
				NewCompetitorResponse nRes =(NewCompetitorResponse)update;
				Competitor competitor =DTOUtils.GetFromDTO(nRes.Competitor);
				try
				{
					client.competitorAdded(competitor);
				}
				catch (Exceptions e)
				{
                    Console.WriteLine(e.StackTrace);
				}
			}
		}
		public virtual void run()
			{
				while(!finished)
				{
					try
					{
                        object response = formatter.Deserialize(stream);
						Console.WriteLine("response received "+response);
						if (response is UpdateResponse)
						{
							 handleUpdate((UpdateResponse)response);
						}
						else
						{
							
							lock (responses)
							{
                                					
								 
                                responses.Enqueue((Response)response);
                               
							}
                            _waitHandle.Set();
						}
					}
					catch (Exception e)
					{
						Console.WriteLine("Reading error "+e);
					}
					
				}
			}
		//}
	}

}