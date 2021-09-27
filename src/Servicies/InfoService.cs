using MartianRobots.Database.Entities;
using MartianRobots.Database.Repositores.Base;
using MartianRobots.Models;
using MartianRobots.Shared.Interfaces.Servicies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Servicies
{
    public class InfoService : IInfoService
    {
        private readonly IRepository<Robot> _robotRepository;
        private readonly IRepository<Module> _moduleRepository;
        private readonly IRepository<Grid> _gridRepository;

        public InfoService(IRepository<Robot> robotRepository,
            IRepository<Module> moduleRepository,
            IRepository<Grid> gridRepository)
        {
            _robotRepository = robotRepository;
            _moduleRepository = moduleRepository;
            _gridRepository = gridRepository;
        }

        public Task<RobotDTO> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task GetAll()
        {
            throw new NotImplementedException();
        }

        public Task GetByDanger(bool danger)
        {
            throw new NotImplementedException();
        }

        public Task GetByPlanet(Guid id)
        {
            throw new NotImplementedException();
        }

        Task IInfoService.Get(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
