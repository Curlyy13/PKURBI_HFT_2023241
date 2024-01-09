using PKURBI_HFT_2023241.Logic.Interfaces;
using PKURBI_HFT_2023241.Models;
using PKURBI_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PKURBI_HFT_2023241.Logic
{
    public class RealEstateLogic : IRealEstateLogic
    {
        IRepository<RealEstate> repo;

        public RealEstateLogic(IRepository<RealEstate> repo)
        {
            this.repo = repo;
        }

        public void Create(RealEstate entity)
        {
            if (entity.BasicArea <= 20)
            {
                throw new ArgumentException("The RealEstate couldn't be created because the Area is too small!");
            }
            repo.Create(entity);
        }

        public void Delete(int id)
        {
            var estate = repo.Read(id);
            if (estate == null)
            {
                throw new ArgumentException($"The RealEstate with the following ID doesn't exist: {id}");
            }
            repo.Delete(id);
        }

        public RealEstate Read(int id)
        {
            var estate = repo.Read(id);
            if (estate == null)
            {
                throw new ArgumentException($"The RealEstate with the following ID doesn't exist: {id}");
            }
            return repo.Read(id);
        }

        public IQueryable<RealEstate> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(RealEstate entity)
        {
            if (entity.BasicArea <= 20)
            {
                throw new ArgumentException("The RealEstate couldn't be updated because the Area is too small!");
            }
            repo.Update(entity);
        }


        //NON-CRUD 1
        //Returns the avarage price of the estates according to the given Salesperson ID
        public double? AvgPriceBySalespersonID(int id)
        {
            var ExceptionTest = this.repo.ReadAll().Where(t=>t.SalesId == id).ToList();
            if (ExceptionTest.Count() == 0)
            {
                throw new ArgumentException("Nem található az megadott ID-val RealEstate, így nem végezhető el a feladat.");
            }
            return this.repo
                .ReadAll()
                .Where(t => t.SalesId == id)
                .Average(t => t.RealEstateValue);
        }

        //NON-CRUD 2
        //Return the basic informations about the realestate with the salesperson name and the contact of the renter

        public BasicInfo BasicInformation(int id)
        {
            var basic = from x in this.repo.ReadAll()
                   where x.RealEstateId == id
                   select new BasicInfo()
                   {
                       Location = x.RealEstateCity,
                       Value = x.RealEstateValue,
                       Area = x.BasicArea,
                       Salesperson = x.Salesperson.Name,
                       Tenant = x.Tenant.Name,
                       TenantContact = x.Tenant.Phone
                   };
            var test = basic.ToArray();
            if (test.Length != 0)
            { 
                if (test[0].Salesperson == null || test[0].Value == null || test[0].TenantContact == null || test[0].Tenant == null || test[0].Area == null || test[0].Location == null) { throw new ArgumentException("The NonCrud method couldn't be performed because the RealEstate has missing parts which are required to do this task."); }
            }
            else { throw new ArgumentException("The NonCrud method couldn't be performed because the RealEstate with the following ID doesn't exist"); }
            basic.ToList();
            BasicInfo info = basic.First();
            return info;
        }

        //NON-CRUD 3
        //Returns the avarage prices grouped by cities
        public IEnumerable<AvgPrices> AvgPriceByCity()
        {
            return from x in this.repo.ReadAll()
                   group x by x.RealEstateCity into g
                   select new AvgPrices()
                   {
                       City = g.Key,
                       AvgPrice = g.Sum(t => t.RealEstateValue)
                   };
        }
    }

}
