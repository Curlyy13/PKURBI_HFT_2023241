using PKURBI_HFT_2023241.Models;
using System.Linq;

namespace PKURBI_HFT_2023241.Logic
{
    public interface IRealEstateLogic
    {
        void Create(RealEstate entity);
        void Delete(int id);
        RealEstate Read(int id);
        IQueryable<RealEstate> ReadAll();
        void Update(RealEstate entity);
    }
}