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

        public TenantLogic(IRepository<Tenant> repo)
        {
            this.repo = repo;
        }

        public void Create(Tenant entity)
        {
            if (entity.Phone.ToString().Length != 9)
            {
                throw new ArgumentException($"The Tenant with the following ID coulnd't be created because the phone" +
                    $"length must be 9 characters: {entity.TenantId}");
            }
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
        //Returns the tenants ordered by the count of the RealEstates they rent

        public IEnumerable<Tenants> TenantsByCity()
        {
            return from x in this.repo.ReadAll()
                   group x by x.Name into g
                   orderby g.Select(t => t.Realestates.Count()).Sum() ascending
                   select new Tenants()
                   {
                       Name = g.Key,
                       EstateCount = g.Select(t => t.Realestates.Count()).Sum()
                   };
        }
    }

    public class Tenants
    {
        public int EstateCount { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            Tenants b = obj as Tenants;
            if (b == null)
            {
                return false;
            }
            else
            {
                return this.Name == b.Name && this.EstateCount == b.EstateCount;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Name,this.EstateCount);
        }
    }
}
