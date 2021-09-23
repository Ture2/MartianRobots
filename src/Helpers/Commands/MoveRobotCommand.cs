using MartianRobots.Helpers.Receiver;
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
        private Instruction _i { get; set; }
        private MoveRobotCommandReceiver _receiver;
        public MoveRobotCommand(Instruction instruction, GridDTO gridDTO) : base(gridDTO)
        {
            _i = instruction;
        }
        public override void Execute()
        {
            _receiver = new MoveRobotCommandReceiver();
            GridDTO updatedGrid = _receiver.Move(_grid, _i);
            Result = _receiver.ValidateMove(updatedGrid);
        }
    }
}
