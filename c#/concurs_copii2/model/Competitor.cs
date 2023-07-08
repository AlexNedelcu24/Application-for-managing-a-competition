using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model;

namespace model
{
    public class Competitor : Entity<int>
    {
        private string name;
        private int age;
        private List<int> challengeList;

        public Competitor(string name, int age, List<int> challengeList)
        {
            this.name = name;
            this.age = age;
            this.challengeList = challengeList;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public List<int> ChallengeList
        {
            get { return challengeList; }
            set { challengeList = value; }
        }

        public override string ToString()
        {
            return "Competitor{" +
                   "name='" + name + '\'' +
                   ", age=" + age +
                   ", challengeList=" + string.Join(",", challengeList) +
                   '}';
        }

        
        
    }
}

