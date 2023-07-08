using System;
using System.Collections.Generic;
using model;
using networking.dto;

namespace networking.protocol
{
	using UserDTO = networking.dto.UserDTO;
	using CompetitorDTO = networking.dto.CompetitorDTO;
	using ChallengeDTO = networking.dto.ChallengeDTO;

	public interface Response 
	{
	}

	[Serializable]
	public class OkResponse : Response
	{
		
	}

    [Serializable]
	public class ErrorResponse : Response
	{
		private string message;

		public ErrorResponse(string message)
		{
			this.message = message;
		}

		public virtual string Message
		{
			get
			{
				return message;
			}
		}
	}

	[Serializable]
	public class TakeCompetitorsResponse : Response
	{
		private List<CompetitorDTO> f;

		public TakeCompetitorsResponse(List<CompetitorDTO> f)
		{
			this.f = f;
		}

		public virtual List<CompetitorDTO> Competitors
		{
			get
			{
				return f;
			}
		}
	}
	
	[Serializable]
	public class TakeChallengesResponse : Response
	{
		private IEnumerable<ChallengeDTO> f;

		public TakeChallengesResponse(IEnumerable<ChallengeDTO> f)
		{
			this.f = f;
		}

		public virtual IEnumerable<ChallengeDTO> Challenges
		{
			get
			{
				return f;
			}
		}
	}
	
	public interface UpdateResponse : Response
	{
	}


	[Serializable]
	public class NewCompetitorResponse : UpdateResponse
	{
		
		private CompetitorDTO competitor;

		public NewCompetitorResponse(CompetitorDTO competitor)
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

}