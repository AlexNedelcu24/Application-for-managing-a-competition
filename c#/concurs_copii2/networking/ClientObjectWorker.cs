using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using services;
using model;
using networking.dto;
using networking.protocol;
namespace networking
{
	
	public class ClientObjectWorker :  IObserver //, Runnable
	{
		private IServices server;
		private TcpClient connection;

		private NetworkStream stream;
		private IFormatter formatter;
		private volatile bool connected;
		public ClientObjectWorker(IServices server, TcpClient connection)
		{
			this.server = server;
			this.connection = connection;
			try
			{
				
				stream=connection.GetStream();
                formatter = new BinaryFormatter();
				connected=true;
			}
			catch (Exception e)
			{
                Console.WriteLine(e.StackTrace);
			}
		}

		public virtual void run()
		{
			while(connected)
			{
				try
				{
                    object request = formatter.Deserialize(stream);
					object response =handleRequest((Request)request);
					if (response!=null)
					{
					   sendResponse((Response) response);
					}
				}
				catch (Exception e)
				{
                    Console.WriteLine(e.StackTrace);
				}
				
				try
				{
					Thread.Sleep(1000);
				}
				catch (Exception e)
				{
                    Console.WriteLine(e.StackTrace);
				}
			}
			try
			{
				stream.Close();
				connection.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error "+e);
			}
		}
		public virtual void competitorAdded(Competitor competitor)
		{
			CompetitorDTO cdto = DTOUtils.GetDTO(competitor);
			Console.WriteLine("Competitor added  "+competitor);
			try
			{
				sendResponse(new NewCompetitorResponse(cdto));
			}
			catch (Exception e)
			{
				throw new Exceptions("Sending error: "+e);
			}
		}

		private Response handleRequest(Request request)
		{
			Response response =null;
			if (request is LoginRequest)
			{
				Console.WriteLine("Login request ...");
				LoginRequest logReq =(LoginRequest)request;
				UserDTO udto =logReq.User;
				User user =DTOUtils.GetFromDTO(udto);
				try
                {
                    lock (server)
                    {
                        server.Login(user, this);
                    }
					return new OkResponse();
				}
				catch (Exceptions e)
				{
					connected=false;
					return new ErrorResponse(e.Message);
				}
			}
			if (request is LogoutRequest)
			{
				Console.WriteLine("Logout request");
				LogoutRequest logReq =(LogoutRequest)request;
				UserDTO udto =logReq.User;
				User user =DTOUtils.GetFromDTO(udto);
				try
				{
                    lock (server)
                    {

                        server.Logout(user, this);
                    }
					connected=false;
					return new OkResponse();

				}
				catch (Exceptions e)
				{
				   return new ErrorResponse(e.Message);
				}
			}
			if (request is SaveCompetitorRequest)
			{
				Console.WriteLine("SaveCompetitorRequest ...");
				SaveCompetitorRequest sReq =(SaveCompetitorRequest)request;
				CompetitorDTO cdto =sReq.Competitor;
				Competitor competitor =DTOUtils.GetFromDTO(cdto);
				try
				{
                    lock (server)
                    {
                        server.SaveCompetitor(competitor.Name,competitor.Age,competitor.ChallengeList);
                    }
                        return new OkResponse();
				}
				catch (Exceptions e)
				{
					return new ErrorResponse(e.Message);
				}
			}

			if (request is FindAllChallengesRequest)
			{
				Console.WriteLine("FindAllChallengesRequest ...");
				
				try
				{
					IEnumerable<Challenge> l;
					lock (server)
					{
						l = server.FindAllChallenges();
					}

					IEnumerable<ChallengeDTO> ldto = DTOUtils.GetDTO(l);
					return new TakeChallengesResponse(ldto);
				}
				catch (Exceptions e)
				{
					return new ErrorResponse(e.Message);
				}
			}
			
			if (request is FindCompetitorsRequest)
			{
				Console.WriteLine("FindCompetitorsRequest ...");
				FindCompetitorsRequest fcReq =(FindCompetitorsRequest)request;
				ChallengeDTO cdto =fcReq.Challenge;
				Challenge challenge =DTOUtils.GetFromDTO(cdto);
				try
				{
					List<Competitor> l;
					lock (server)
					{
						l = server.FindCompetitors(challenge.Name,challenge.Category);
					}

					List<CompetitorDTO> ldto = DTOUtils.GetDTO(l);
					return new TakeCompetitorsResponse(ldto);
				}
				catch (Exceptions e)
				{
					return new ErrorResponse(e.Message);
				}
			}
			
			return response;
		}

	private void sendResponse(Response response)
		{
			Console.WriteLine("sending response "+response);
			lock (stream)
			{
				formatter.Serialize(stream, response);
				stream.Flush();
			}

		}
	}

}