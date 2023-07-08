
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model;
using persistence;

namespace persistence
{
    public interface InterfaceCompetitor : ICrudRepository<int, Competitor>
    {
        List<Competitor> FindByChallenge(int id);
    }
}
