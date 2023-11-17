using PKURBI_HFT_2023241.Models;
using PKURBI_HFT_2023241.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKURBI_HFT_2023241.Repository.Repositories
{
    internal class SalespersonRepository : ISalespersonRepository
    {
        AgencyDbContext context;

        public SalespersonRepository(AgencyDbContext context)
        {
            this.context = context;
        }
        public void Create(Salesperson salesperson)
        {
            this.context.salespeople.Add(salesperson);
            this.context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Salesperson Read(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Salesperson> ReadAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Salesperson salesperson)
        {
            throw new NotImplementedException();
        }
    }
}
