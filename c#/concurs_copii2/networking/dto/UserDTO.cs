namespace networking.dto
{
    using System;

    [Serializable]
    public class UserDTO
    {
        private int id;
        private string password;
        private string username;
        
        public virtual int Id { get
            {
                return id;
            }
            set
            {
                this.id = value;
            } 
        }
        public virtual string Username { get
            {
                return username;
            }
        }
        public virtual string Password { get
        {
            return password;
        } }

        public UserDTO(int id, string username, string password)
        {
            this.id = id;
            this.username = username;
            this.password = password;
        }

        public UserDTO(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public UserDTO() { }

        public override string ToString()
        {
            return $"UserDTO{{id={Id}, username='{Username}', password='{Password}'}}";
        }
    }

}