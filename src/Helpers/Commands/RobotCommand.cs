using MartianRobots.Models;
using MartianRobots.Models.Grids;
using MartianRobots.Shared.Interfaces.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Helpers.Commands
{
    public class RobotCommand: IRobotCommand
    {
        private readonly Instruction _instruction;
        private readonly GridDTO _grid;

        public RobotCommand(Instruction instruction, GridDTO gridDTO)
        {
            _instruction = instruction;
            _grid = gridDTO;

        }
        public void Execute()
        {
            if (_instruction.Equals(Instruction.F))
            {
                
            }
            if (_instruction.Equals(Instruction.R))
            {

            }
            if (_instruction.Equals(Instruction.L))
            {

            }
           
        }
    }
}
