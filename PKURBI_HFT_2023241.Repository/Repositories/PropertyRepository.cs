using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        public void Create(Property property)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Property Read(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Property> ReadAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Property property)
        {
            throw new NotImplementedException();
        }
    }
}
