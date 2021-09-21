using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MartianRobots.Models;
using MartianRobots.EF.Contexts;

namespace MartianRobots.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MartianRobotsController : ControllerBase
    {

        private readonly ILogger<MartianRobotsController> _logger;
        private readonly MartianRobotsContext _context;

        public MartianRobotsController(ILogger<MartianRobotsController> logger, MartianRobotsContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Robot> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Robot())
            .ToArray();
        }
    }
}
