using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using devwarsztaty.messages.Commands;
using RawRabbit;

namespace devwarsztaty.web.Controllers
{
    //[Produces("application/json")]
    //[Route("api/Records")]
    [Route("[controller]")]
    public class RecordsController : Controller
    {
        private readonly IBusClient _client;
        private readonly IStorage _storage;

        public RecordsController(IBusClient client, IStorage storage)
        {
            _client = client;
            _storage = storage;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateRecord command)
        {
            await _client.PublishAsync(command);

            return Accepted();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = $"Persisted keys: <br />" +
                        string.Join("<br /> - ", _storage.GetAll());

            return Ok(response);
        }
    }
}