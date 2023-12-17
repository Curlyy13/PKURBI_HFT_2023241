using PKURBI_HFT_2023241.Models;
using PKURBI_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKURBI_HFT_2023241.Logic
{
    public class TenantLogic : ITenantLogic
    {
        IRepository<Tenant> repo;
        public void Create(Tenant entity)
        {
            repo.Create(entity);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public Tenant Read(int id)
        {
            var tenant = repo.Read(id);
            if (tenant == null)
            {
                throw new ArgumentException($"The RealEstate with the following ID doesn't exist: {id}");
            }
            return tenant;
        }

        public IQueryable<Tenant> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(Tenant entity)
        {
            repo.Update(entity);
        }
    }
}
