using PKURBI_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKURBI_HFT_2023241.Repository
{
    public class RealEstateRepository : Repository<RealEstate>, IRepository<RealEstate>
    {
        public RealEstateRepository(AgencyDbContext ctx) : base(ctx) {}
        public override RealEstate Read(int id)
        {
            return this.ctx.realEstates.FirstOrDefault(t => t.RealEstateId == id);
        }

        public override void Update(RealEstate entity)
        {
            var old = Read(entity.RealEstateId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(entity));
            }
            ctx.SaveChanges();
        }
    }
}
