using MartianRobots.Shared.Interfaces.Servicies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MartianRobots.Controllers
{
    [Route("martianrobots/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly IInfoService _infoService;
        private readonly ILogger<InfoController> _logger;

        public InfoController(ILogger<InfoController> logger, IInfoService service)
        {
            _logger = logger;
            _infoService = service;
        }

        // GET api/<InfoController>/5
        [HttpGet]
        public Task Get()
        {
            return _infoService.GetAll();
        }


    }
}
