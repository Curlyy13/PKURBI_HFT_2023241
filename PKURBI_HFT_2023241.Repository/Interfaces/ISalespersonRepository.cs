using PKURBI_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKURBI_HFT_2023241.Repository.Interfaces
{
    internal interface ISalespersonRepository
    {
        void Create(Salesperson salesperson);
        Property Read(int id);
        IQueryable<Salesperson> ReadAll();
        void Update(Salesperson salesperson);
        void Delete(int id);
    }
}
