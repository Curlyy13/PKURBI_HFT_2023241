using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PKURBI_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKURBI_HFT_2023241.Repository.Interfaces
{
    internal interface IPropertyRepository
    {
        void Create(RealEstate realestate);
        RealEstate Read(int id);
        IQueryable<RealEstate> ReadAll();
        void Update(RealEstate realestate);
        void Delete(int id);
    }
}
