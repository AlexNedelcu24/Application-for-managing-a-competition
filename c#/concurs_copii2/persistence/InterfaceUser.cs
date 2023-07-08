
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model;
using persistence;

namespace persistence
{
    public interface InterfaceUser : ICrudRepository<int, User>
    {
        User FindLog(string username, string password);
    }
}
