using MartianRobots.Database.Entities;
using MartianRobots.Database.Repositores.Base;
using MartianRobots.Helpers.Commands;
using MartianRobots.Helpers.Engines;
using MartianRobots.Models;
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
        private readonly IRepository<Robot> _robotRepository;
        private readonly IRepository<Grid> _gridRepository;
        private readonly IRepository<Module> _moduleRepository;
        

        public RobotService(
            IRepository<Robot> robotRepository, 
            IRepository<Grid> gridRepository, 
            IRepository<Module> moduleRepository)
        {
            _robotRepository = robotRepository;
            _gridRepository = gridRepository;
            _moduleRepository = moduleRepository;
        }


        public Task DeployRobot(DeployDTO deployDTO)
        {

            DeployEngine engine = new DeployEngine(deployDTO);

            return Task.Factory.StartNew(() => engine.Deploy());

            // Llamar al command handler hay que pasarle los elementos para que me genere un grid, y luego me ejecute la lista del path

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
