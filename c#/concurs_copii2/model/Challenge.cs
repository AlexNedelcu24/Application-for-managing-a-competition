using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model;

namespace model
{
    public class Challenge : Entity<int>
    {
        private string name;
        private string category;
        private int enrolled;

        public Challenge(string name, string category, int enrolled)
        {
            this.name = name;
            this.category = category;
            this.enrolled = enrolled;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public int Enrolled
        {
            get { return enrolled; }
            set { enrolled = value; }
        }

        public override string ToString()
        {
            return "Challange{" +
                   "name='" + name + '\'' +
                   ", category='" + category + '\'' +
                   ", enrolled='" + enrolled + '\'' +
                   '}';
        }
        
    }
}

