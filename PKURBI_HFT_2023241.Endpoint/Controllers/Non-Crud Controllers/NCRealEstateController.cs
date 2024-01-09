using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PKURBI_HFT_2023241.Logic;
using PKURBI_HFT_2023241.Logic.Interfaces;
using PKURBI_HFT_2023241.Models;
using System.Collections.Generic;

namespace PKURBI_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NCRealEstateController : ControllerBase
    {
        IRealEstateLogic logic;

        public NCRealEstateController(IRealEstateLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet("{id}")]
        public double? AvgPriceBySalespersonID(int id)
        {
            return this.logic.AvgPriceBySalespersonID(id);
        }

        [HttpGet("{id}")]
        public BasicInfo BasicInformation(int id)
        {
            return this.logic.BasicInformation(id);
        }

        [HttpGet]
        public IEnumerable<AvgPrices> AvgPriceByCity()
        {
            return this.logic.AvgPriceByCity();
        }
    }
}
