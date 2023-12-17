using PKURBI_HFT_2023241.Models;
using System.Linq;

namespace PKURBI_HFT_2023241.Logic
{
    public interface ITenantLogic
    {
        void Create(Tenant entity);
        void Delete(int id);
        Tenant Read(int id);
        IQueryable<Tenant> ReadAll();
        void Update(Tenant entity);
    }
}