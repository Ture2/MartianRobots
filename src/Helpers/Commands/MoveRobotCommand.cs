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
        private MoveRobotCommandReceiver _receiver = new MoveRobotCommandReceiver();
        public MoveRobotCommand(Instruction instruction, GridDTO gridDTO) : base(gridDTO)
        {
            _i = instruction;
        }
        public override void Execute()
        {
            _receiver.Move(_grid, _i);
            
        }

        public void Validate()
        {
            _receiver.ValidateLastMove(_grid);
        }
    }
}
