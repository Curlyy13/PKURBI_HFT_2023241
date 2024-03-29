using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using PKURBI_HFT_2023241.Endpoint.Services;
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
        IHubContext<SignalRHub> hub;
        public RealEstateController(IRealEstateLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("RealEstateCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] RealEstate value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("RealEstateUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var realestatetodelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("RealEstateDeleted", realestatetodelete);
        }
    }
}
