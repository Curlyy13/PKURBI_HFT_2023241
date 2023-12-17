using PKURBI_HFT_2023241.Models;
using PKURBI_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
