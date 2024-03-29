using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PKURBI_HFT_2023241.Endpoint.Services;
using PKURBI_HFT_2023241.Logic;
using PKURBI_HFT_2023241.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PKURBI_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {

        ITenantLogic logic;
        IHubContext<SignalRHub> hub;

        public TenantController(ITenantLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Tenant> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Tenant Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Tenant value)
        {
             this.logic.Create(value);
             this.hub.Clients.All.SendAsync("TenantCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Tenant value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("TenantUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var tenanttodelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("TenantDeleted", tenanttodelete);
        }
    }
}
