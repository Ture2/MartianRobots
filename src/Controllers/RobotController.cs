using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MartianRobots.Models;
using MartianRobots.Database.Contexts;

namespace MartianRobots.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RobotController : ControllerBase
    {

        private readonly ILogger<RobotController> _logger;
        private readonly MartianRobotsContext _context;

        public RobotController(ILogger<RobotController> logger, MartianRobotsContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<RobotDTO> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new RobotDTO())
            .ToArray();
        }
    }
}
