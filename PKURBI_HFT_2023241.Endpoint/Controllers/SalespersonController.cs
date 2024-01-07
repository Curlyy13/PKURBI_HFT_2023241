using Microsoft.AspNetCore.Mvc;
using PKURBI_HFT_2023241.Logic;
using PKURBI_HFT_2023241.Models;
using System.Collections.Generic;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PKURBI_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SalespersonController : ControllerBase
    {
        ISalespersonLogic logic;

        public SalespersonController(ISalespersonLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Salesperson> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Salesperson Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Salesperson value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Update([FromBody] Salesperson value)
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
