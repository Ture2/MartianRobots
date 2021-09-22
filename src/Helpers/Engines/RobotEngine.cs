using MartianRobots.Helpers.Commands;
using MartianRobots.Models;
using MartianRobots.Shared.Interfaces.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Helpers.Engines
{
    public class RobotEngine
    {
        private MoveRobotCommand _movesCommands;
        private SetRobotCommand _setRobotCommand;
        private readonly RobotDTO _item;
        public RobotInfo RobotInfo { get; set; }

        public RobotEngine() {}

        
        public void SetPositionCommand(SetRobotCommand command)
        {
            _setRobotCommand = command;
        }

        public void MoveRobotCommand(MoveRobotCommand command)
        {
            _movesCommands = command;
        }

        public void Execute()
        {
            // Apply set on mars
            _setRobotCommand.Execute();
            _setRobotCommand.GetResult();

            // Then run list of moves
        } 
       

       
    }
}
