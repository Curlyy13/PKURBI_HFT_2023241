using Microsoft.Extensions.Caching.Memory;
using PKURBI_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKURBI_HFT_2023241.Repository
{
    public class TenantRepository : Repository<Tenant>, IRepository<Tenant>
    {
        public TenantRepository(AgencyDbContext ctx) : base(ctx) {}

        public override Tenant Read(int id)
        {
            return this.ctx.tenants.FirstOrDefault(t => t.TenantId == id);
        }

        public override void Update(Tenant entity)
        {
            var old = Read(entity.TenantId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(entity));
            }
            ctx.SaveChanges();
        }
    }
}
