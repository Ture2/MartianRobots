using MartianRobots.Models;
using MartianRobots.Models.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Shared.Interfaces.Commands
{
    public interface IDeployCommand
    {
        public void Execute();
        public GridDTO Result { get; }
    }
}
