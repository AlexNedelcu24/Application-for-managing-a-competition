using System;
using model;

namespace networking.protocol
{
	using UserDTO = networking.dto.UserDTO;
	using CompetitorDTO = networking.dto.CompetitorDTO;
	using ChallengeDTO = networking.dto.ChallengeDTO;


	public interface Request 
	{
	}


	[Serializable]
	public class LoginRequest : Request
	{
		private UserDTO user;

		public LoginRequest(UserDTO user)
		{
			this.user = user;
		}

		public virtual UserDTO User
		{
			get
			{
				return user;
			}
		}
	}

	[Serializable]
	public class LogoutRequest : Request
	{
		private UserDTO user;

		public LogoutRequest(UserDTO user)
		{
			this.user = user;
		}

		public virtual UserDTO User
		{
			get
			{
				return user;
			}
		}
	}

	[Serializable]
	public class SaveCompetitorRequest : Request
	{
		private CompetitorDTO competitor;

		public SaveCompetitorRequest(CompetitorDTO competitor)
		{
			this.competitor = competitor;
		}

		public virtual CompetitorDTO Competitor
		{
			get
			{
				return competitor;
			}
		}
	}

	[Serializable]
	public class FindCompetitorsRequest : Request
	{
		private ChallengeDTO challenge;

		public FindCompetitorsRequest(ChallengeDTO challenge)
		{
			this.challenge = challenge;
		}

		public virtual ChallengeDTO Challenge
		{
			get
			{
				return challenge;
			}
		}
	}


	[Serializable]
	public class FindAllChallengesRequest : Request
	{
		

		public FindAllChallengesRequest()
		{
			
		}

		/*public virtual void Challenge
		{
			
		}*/
	}
}