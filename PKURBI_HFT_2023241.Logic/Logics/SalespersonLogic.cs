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
            this.repo.Create(entity);
        }

        public void Delete(int id)
        {
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
            this.repo.Update(entity);
        }

        //NON-CRUD 4
        //Returns the top 3 salesman who has the most real estates

        public IEnumerable<MostRealEstate> MostRealEstates()
        {
            var result = from x in this.repo.ReadAll()
                         group x by x.Name into g
                         orderby g descending
                         select new MostRealEstate()
                         {
                             Name = g.Key,
                         };
            var finalized = result.Take(3);
            return finalized;
        }
    }

    public class MostRealEstate
    {
        public string Name { get; set; }
    }
}
