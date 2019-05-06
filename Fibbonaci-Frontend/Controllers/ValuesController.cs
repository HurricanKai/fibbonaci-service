using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Messages;
using Microsoft.AspNetCore.Mvc;

namespace Fibbonaci_Frontend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ValuesController : ControllerBase
    {
        private IBus _bus;

        public ValuesController(IBus bus)
        {
            _bus = bus;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 0)
                return NotFound(id);
            return Content((await _bus.RequestAsync<FibbonaciRequest, FibbonaciResponse>(new FibbonaciRequest(id))).Value.ToString());
        }
    }
}
