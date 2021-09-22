using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Models
{
    public class DeployDTO
    {
        public List<int> GridSize { get; set; }
        public List<string> RobotInitialPosition { get; set; }
        public string Path { get; set; }
    }
}
