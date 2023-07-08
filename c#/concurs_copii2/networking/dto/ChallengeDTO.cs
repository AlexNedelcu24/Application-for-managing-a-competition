namespace networking.dto
{
    using System;

    [Serializable]
    public class ChallengeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Enrolled { get; set; }

        public ChallengeDTO(int id, string name, string category, int enrolled)
        {
            Id = id;
            Name = name;
            Category = category;
            Enrolled = enrolled;
        }

        public ChallengeDTO() { }

        public override string ToString()
        {
            return $"ChallengeDTO{{id={Id}, name='{Name}', category='{Category}', enrolled={Enrolled}}}";
        }
    }

}