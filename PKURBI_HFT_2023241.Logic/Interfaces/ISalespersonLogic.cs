using PKURBI_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace PKURBI_HFT_2023241.Logic
{
    public interface ISalespersonLogic
    {
        void Create(Salesperson entity);
        void Delete(int id);
        IEnumerable<string> MostRealEstates();
        Salesperson Read(int id);
        IQueryable<Salesperson> ReadAll();
        void Update(Salesperson entity);
    }
}