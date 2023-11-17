using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PKURBI_HFT_2023241.Models;
using PKURBI_HFT_2023241.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKURBI_HFT_2023241.Repository.Repositories
{
    internal class PropertyRepository : IPropertyRepository
    {
        AgencyDbContext context;

        public PropertyRepository(AgencyDbContext context)
        {
            this.context = context;
        }
        public void Create(RealEstate realestate)
        {
            this.context.realEstates.Add(realestate);
            this.context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public RealEstate Read(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RealEstate> ReadAll()
        {
            throw new NotImplementedException();
        }

        public void Update(RealEstate realestate)
        {
            throw new NotImplementedException();
        }
    }
}
