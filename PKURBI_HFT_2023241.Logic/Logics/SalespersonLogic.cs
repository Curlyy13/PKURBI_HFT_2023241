using PKURBI_HFT_2023241.Models;
using PKURBI_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PKURBI_HFT_2023241.Logic
{
    public class SalespersonLogic : ISalespersonLogic
    {
        IRepository<Salesperson> repo;

        public SalespersonLogic(IRepository<Salesperson> repo)
        {
            this.repo = repo;
        }

        public void Create(Salesperson entity)
        {
            if (entity.Age < 16)
            {
                throw new ArgumentException("The Salesperson couldn't be created because he/she is too young!");
            }
            this.repo.Create(entity);
        }

        public void Delete(int id)
        {
            var salesperson = repo.Read(id);
            if (salesperson == null)
            {
                throw new ArgumentException($"The RealEstate with the following ID doesn't exist: {id}");
            }
            this.repo.Delete(id);
        }

        public Salesperson Read(int id)
        {
            var salesperson = repo.Read(id);
            if (salesperson == null)
            {
                throw new ArgumentException($"The RealEstate with the following ID doesn't exist: {id}");
            }
            return salesperson;
        }

        public IQueryable<Salesperson> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Salesperson entity)
        {
            if (entity.Age < 16)
            {
                throw new ArgumentException("The Salesperson couldn't be updated because he/she is too young!");
            }
            this.repo.Update(entity);
        }

        //NON-CRUD 4
        //Returns the top 3 salesman who has the most real estates

        public IEnumerable<string> MostRealEstates()
        {
            var result = this.repo.ReadAll()
                            .OrderByDescending(x => x.Realestates.Count())
                            .Select(x => x.Name)
                            .ToList();
            var finalized = result.Take(3);
            return finalized;
        }
    }
}
