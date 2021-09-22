using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Models
{

    public class DeployDTO
    {
        public string GridSize { get; set; }

        public RobotInfo[] RobotInfoList { get; set; }

    }

    public class RobotInfo
    {
        public string InitialPosition { get; set; }

        public string Path { get; set; }
    }
}
