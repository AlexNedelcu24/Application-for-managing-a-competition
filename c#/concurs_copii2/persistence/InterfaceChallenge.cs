
using model;
using persistence;

namespace persistence
{
    public interface InterfaceChallenge : ICrudRepository<int, Challenge>
    {
        void UpdateEnrolled(int id);
        int FindByNameAndCategory(string challengeName, string category);
    }
}

