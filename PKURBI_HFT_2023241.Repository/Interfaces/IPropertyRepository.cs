using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKURBI_HFT_2023241.Repository.Interfaces
{
    internal interface IPropertyRepository
    {
        void Create(Property property);
        Property Read(int id);
        IQueryable<Property> ReadAll();
        void Update(Property property);
        void Delete(int id);
    }
}
