using MartianRobots.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Shared.Interfaces.Servicies
{
    public interface IInfoService
    {
        Task<RobotDTO> Get(Guid id);

        Task Get(int x, int y);

        Task GetAll();
        Task GetByPlanet(Guid id);
        Task GetByDanger(bool danger);
        
    }
}
