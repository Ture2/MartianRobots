using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Servicies.Interfaces
{
    public interface IRobotService
    {
        Task<RobotDTO> GetRobot(Guid id);
        Task<List<RobotDTO>> GetRobot(Guid planetId);
        Task DeployRobot(RobotDTO robot);
        Task MoveRobot(IntructionDTO instruction);

        

    }
}
