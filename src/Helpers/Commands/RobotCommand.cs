using MartianRobots.Models;
using MartianRobots.Models.Grids;
using MartianRobots.Shared.Interfaces.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Helpers.Commands
{
    // Abstract class for robot instructions
    public abstract class RobotCommand: IRobotCommand
    {
        protected readonly GridDTO _grid;
        public GridDTO Result { get; protected set; }
        public RobotCommand(GridDTO gridDTO)
        {
            _grid = gridDTO;

        }
        public abstract void Execute();
    }
}
