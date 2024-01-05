using PKURBI_HFT_2023241.Models;
using PKURBI_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        //NON-CRUD 5
        //Returns the tenants ordered by cities

        public IEnumerable<Tenants> TenantsByCity(string city)
        {
            return from x in this.repo.ReadAll()
                   group x by x.Name into g
                   select new Tenants()
                   {
                       City = g.Select(t => t.Realestates.Where(x => x.RealEstateCity == city)),
                       Name = g.Key,
                   };
        }
    }

    public class Tenants
    {
        public IEnumerable<IEnumerable<RealEstate>> City { get; set; }
        public string Name { get; set; }
    }
}
