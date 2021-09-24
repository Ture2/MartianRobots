using MartianRobots.Database.Entities;
using MartianRobots.Database.Repositores.Base;
using MartianRobots.Helpers.Commands;
using MartianRobots.Helpers.Engines;
using MartianRobots.Models;
using MartianRobots.Models.Grids;
using MartianRobots.Shared.Inferfaces.Servicies;
using MartianRobots.Shared.Interfaces.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Servicies
{
    public class RobotService: IRobotService
    {
        //private readonly IRepository<Robot> _robotRepository;
        //private readonly IRepository<Grid> _gridRepository;
        //private readonly IRepository<Module> _moduleRepository;
        

        public RobotService(
            //IRepository<Robot> robotRepository, 
            //IRepository<Grid> gridRepository, 
            //IRepository<Module> moduleRepository
            )
        {
            //_robotRepository = robotRepository;
            //_gridRepository = gridRepository;
            //_moduleRepository = moduleRepository;
        }


        public Task DeployRobot(DeployDTO deployDTO)
        {
            // Grid creation
            DeployEngine engine = new DeployEngine(deployDTO);
            GridDTO gridDTO = engine.Deploy();

            // Robot deploys
           
            return Task.Factory.StartNew(() => {
                foreach (RobotInfo r in deployDTO.RobotInfoList)
                {
                    RobotEngine robotEngine = new RobotEngine(r, gridDTO);
                    robotEngine.Execute();
                }
            });
        }

        public Task<List<RobotDTO>> GetAllPlanetRobots(Guid planetId)
        {
            throw new NotImplementedException();
        }

        public Task<RobotDTO> GetRobot(Guid id)
        {
            throw new NotImplementedException();
        }

      
    }
}
