using Microsoft.AspNetCore.Mvc;
using PKURBI_HFT_2023241.Logic.Interfaces;
using PKURBI_HFT_2023241.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PKURBI_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RealEstateController : ControllerBase
    {
        IRealEstateLogic logic;

        public RealEstateController(IRealEstateLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<RealEstate> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public RealEstate Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] RealEstate value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Update([FromBody] RealEstate value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
