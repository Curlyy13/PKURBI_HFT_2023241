using Microsoft.EntityFrameworkCore;
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
                throw new ArgumentException("The Tenant coulnd't be created because the phone length must be 9 characters!");
            }
            repo.Create(entity);
        }

        public void Delete(int id)
        {
            var tenant = repo.Read(id);
            if (tenant == null)
            {
                throw new ArgumentException($"The RealEstate with the following ID doesn't exist: {id}");
            }
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
            if (entity.Phone.ToString().Length != 9)
            {
                throw new ArgumentException("The Tenant coulnd't be updated because the phone length must be 9 characters!");
            }
            repo.Update(entity);
        }

        //NON-CRUD 5
        //Returns the tenants ordered by the count of the RealEstates they rent

        public IEnumerable<Tenants> TenantsByCity()
        {
            var tenants = from x in this.repo.ReadAll()
                   let estateCount = x.Realestates.Count()
                   select new Tenants()
                   {
                       Name = x.Name,
                       EstateCount = estateCount
                   };
            return tenants.OrderByDescending(t=> t.EstateCount);
        }
    }
}
