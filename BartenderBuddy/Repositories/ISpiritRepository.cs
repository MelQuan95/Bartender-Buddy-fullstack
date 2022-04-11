using BartenderBuddy.Models;
using System.Collections.Generic;

namespace BartenderBuddy.Repositories
{
    public interface ISpiritRepository
    {
        void Add(Spirits spirits);
        Spirits GetSpiritsById (int id);
        List<Spirits> GetAll();
        void Update(Spirits spirits);
        void Delete(int id);
    }
}