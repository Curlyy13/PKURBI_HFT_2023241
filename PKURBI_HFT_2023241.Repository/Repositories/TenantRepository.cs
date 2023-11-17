using PKURBI_HFT_2023241.Models;
using PKURBI_HFT_2023241.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKURBI_HFT_2023241.Repository.Repositories
{
    internal class TenantRepository : ITenantRepository
    {
        AgencyDbContext context;

        public TenantRepository(AgencyDbContext context)
        {
            this.context = context;
        }
        public void Create(Tenant tenant)
        {
            this.context.tenants.Add(tenant);
            this.context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Tenant Read(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tenant> ReadAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Tenant tenant)
        {
            throw new NotImplementedException();
        }
    }
}
