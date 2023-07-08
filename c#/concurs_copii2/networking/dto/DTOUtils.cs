using System.Collections.Generic;
using model;

namespace networking.dto
{
    public class DTOUtils
    {
        public static Challenge GetFromDTO(ChallengeDTO challengeDTO)
        {
            return new Challenge(challengeDTO.Name, challengeDTO.Category, challengeDTO.Enrolled);
        }

        public static ChallengeDTO GetDTO(Challenge challenge)
        {
            ChallengeDTO challengeDTO = new ChallengeDTO
            {
                Id = challenge.ID,
                Name = challenge.Name,
                Category = challenge.Category,
                Enrolled = challenge.Enrolled
            };
            return challengeDTO;
        }

        public static Competitor GetFromDTO(CompetitorDTO competitorDTO)
        {
            return new Competitor(competitorDTO.Name, competitorDTO.Age, competitorDTO.ChallengeList);
        }

        public static CompetitorDTO GetDTO(Competitor competitor)
        {
            CompetitorDTO competitorDTO = new CompetitorDTO
            {
                Id = competitor.ID,
                Name = competitor.Name,
                Age = competitor.Age,
                ChallengeList = competitor.ChallengeList
            };
            return competitorDTO;
        }

        public static User GetFromDTO(UserDTO userDTO)
        {
            string username =userDTO.Username;
            string pass =userDTO.Password;
            return new User(username, pass);
            return new User(userDTO.Username, userDTO.Password);
        }

        public static UserDTO GetDTO(User user)
        {
            string username =user.Username;
            string pass =user.Password;
            return new UserDTO(username, pass);
        }
        
        public static List<CompetitorDTO> GetDTO(List<Competitor> competitors)
        {
            List<CompetitorDTO> competitorDTOs = new List<CompetitorDTO>(competitors.Count);
            foreach (Competitor competitor in competitors)
            {
                competitorDTOs.Add(GetDTO(competitor));
            }
            return competitorDTOs;
        }

        public static List<Competitor> GetFromDTO(List<CompetitorDTO> competitorDTOs)
        {
            List<Competitor> competitors = new List<Competitor>(competitorDTOs.Count);
            foreach (CompetitorDTO competitorDTO in competitorDTOs)
            {
                competitors.Add(GetFromDTO(competitorDTO));
            }
            return competitors;
        }
        
        public static List<ChallengeDTO> GetDTO(IEnumerable<Challenge> challenges)
        {
            List<ChallengeDTO> challengeDTOs = new List<ChallengeDTO>();
            foreach (Challenge challenge in challenges)
            {
                challengeDTOs.Add(GetDTO(challenge));
            }
            return challengeDTOs;
        }

        public static List<Challenge> GetFromDTO(IEnumerable<ChallengeDTO> challengeDTOs)
        {
            List<Challenge> challenges = new List<Challenge>();
            foreach (ChallengeDTO challengeDTO in challengeDTOs)
            {
                challenges.Add(GetFromDTO(challengeDTO));
            }
            return challenges;
        }
    }

}