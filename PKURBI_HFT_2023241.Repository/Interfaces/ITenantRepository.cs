using PKURBI_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKURBI_HFT_2023241.Repository.Interfaces
{
    internal interface ITenantRepository
    {
        void Create(Tenant tenant);
        Tenant Read(int id);
        IQueryable<Tenant> ReadAll();
        void Update(Tenant tenant);
        void Delete(int id);
    }
}
