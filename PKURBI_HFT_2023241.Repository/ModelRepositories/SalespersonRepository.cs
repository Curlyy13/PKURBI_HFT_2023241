using PKURBI_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKURBI_HFT_2023241.Repository
{
    public class SalespersonRepository : Repository<Salesperson>, IRepository<Salesperson>
    {
        public SalespersonRepository(AgencyDbContext ctx) : base(ctx) {}

        public override Salesperson Read(int id)
        {
            return this.ctx.salespeople.FirstOrDefault(t => t.SalesId == id);
        }

        public override void Update(Salesperson entity)
        {
            var old = Read(entity.SalesId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(entity));
                }
            }
            ctx.SaveChanges();
        }
    }
}
