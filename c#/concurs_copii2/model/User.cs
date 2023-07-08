using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using chat.model;
using model;

namespace model
{
    public class User : Entity<int>
    {
        private string username;
        private string password;

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }

        public override string ToString()
        {
            return "User{" + 
                   "username='" + username + '\'' +
                ", password='" + password + '\'' +
                '}';
        }

        
        
    }

}
