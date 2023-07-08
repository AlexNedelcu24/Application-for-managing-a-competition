namespace networking.dto
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class CompetitorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<int> ChallengeList { get; set; }

        public CompetitorDTO(int id, string name, int age, List<int> challengeList)
        {
            Id = id;
            Name = name;
            Age = age;
            ChallengeList = challengeList;
        }

        public CompetitorDTO() { }

        public override string ToString()
        {
            return $"CompetitorDTO{{id={Id}, name='{Name}', age={Age}, challengeList={ChallengeList}}}";
        }
    }

}