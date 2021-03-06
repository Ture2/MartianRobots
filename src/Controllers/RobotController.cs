using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MartianRobots.Models;
using MartianRobots.Database.Contexts;
using MartianRobots.Shared.Inferfaces.Servicies;

namespace MartianRobots.Controllers
{
    [ApiController]
    [Route("martianrobots/[controller]")]
    public class RobotController : ControllerBase
    {

        private readonly ILogger<RobotController> _logger;
        private readonly IRobotService _robotService;

        public RobotController(ILogger<RobotController> logger, IRobotService service)
        {
            _logger = logger;
            _robotService = service;
        }

        [HttpPost]
        public async Task<List<string>> Deploy(DeployDTO deployDTO) => await _robotService.DeployRobot(deployDTO);
    }
}
