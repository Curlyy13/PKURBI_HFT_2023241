using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PKURBI_HFT_2023241.Logic;
using System.Collections.Generic;

namespace PKURBI_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NCSalespersonController : ControllerBase
    {
        ISalespersonLogic logic;

        public NCSalespersonController(ISalespersonLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<string> MostRealEstates()
        {
            return this.logic.MostRealEstates();
        }
    }
}
