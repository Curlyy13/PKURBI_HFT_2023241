using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PKURBI_HFT_2023241.Logic;
using PKURBI_HFT_2023241.Models;
using System.Collections.Generic;

namespace PKURBI_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NCTenantController : ControllerBase
    {
        ITenantLogic logic;

        public NCTenantController(ITenantLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Tenants> TenantsByCity()
        {
            return this.logic.TenantsByCity();
        }
    }
}
