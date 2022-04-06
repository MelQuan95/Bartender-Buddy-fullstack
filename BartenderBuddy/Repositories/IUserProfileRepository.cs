using BartenderBuddy.Models;

namespace BartenderBuddy.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile userProfile);
        UserProfile GetByEmail(string email);
    }

}
   