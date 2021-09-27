using MartianRobots.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Shared.Inferfaces.Servicies
{
    public interface IRobotService
    {
        
        Task<List<string>> DeployRobot(DeployDTO robot);
        //Task MoveRobot(IntructionDTO instruction);

        

    }
}
