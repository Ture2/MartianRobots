using MartianRobots.Models;
using MartianRobots.Models.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Helpers.Commands
{
    public class MoveRobotCommand : RobotCommand
    {
        public MoveRobotCommand(Instruction instruction, GridDTO gridDTO) : base(instruction, gridDTO)
        {

        }
        public override void Execute()
        {
            
        }
    }
}
