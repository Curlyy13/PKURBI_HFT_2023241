using PKURBI_HFT_2023241.Logic.Interfaces;
using PKURBI_HFT_2023241.Models;
using PKURBI_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
            repo.Create(entity);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public RealEstate Read(int id)
        {
            var estate = repo.Read(id);
            if (estate == null)
            {
                throw new ArgumentException($"The RealEstate with the following ID doesn't exist: {id}");
            }
            return estate;
        }

        public IQueryable<RealEstate> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(RealEstate entity)
        {
            repo.Update(entity);
        }


        //NON-CRUD 1
        //Returns the avarage price of the estates according to the given Salesperson ID
        public double? AvgPriceBySalespersonID(int id)
        {
            return this.repo
                .ReadAll()
                .Where(t => t.SalesId == id)
                .Average(t => t.RealEstateValue);
        }

        //NON-CRUD 2
        //Return the basic informations about the realestate with the salesperson name and the contact of the renter

        public IEnumerable<BasicInfo> BasicInformation(int id)
        {
            return from x in this.repo.ReadAll()
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

    public class AvgPrices
    {
        public object City { get; set; }
        public object AvgPrice { get; set; }
    }
    public class BasicInfo
    {
        public string Location { get; set; }
        public double Value { get; set; }
        public double Area { get; set; }
        public string Salesperson { get; set; }
        public string Tenant { get; set; }
        public int TenantContact { get; set; }
    }
}
