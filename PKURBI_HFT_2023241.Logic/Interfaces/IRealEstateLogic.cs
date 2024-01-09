using PKURBI_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace PKURBI_HFT_2023241.Logic.Interfaces
{
    public interface IRealEstateLogic
    {
        IEnumerable<AvgPrices> AvgPriceByCity();
        double? AvgPriceBySalespersonID(int id);
        BasicInfo BasicInformation(int id);
        void Create(RealEstate entity);
        void Delete(int id);
        RealEstate Read(int id);
        IQueryable<RealEstate> ReadAll();
        void Update(RealEstate entity);
    }
}