using MartianRobots.Database.Entities;
using MartianRobots.Models;
using MartianRobots.Models.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Helpers.Commands
{
    public class SetRobotCommand: RobotCommand
    {
        private readonly RobotInfo _data;
        private SetRobotCommandReceiver _receiver;
        public SetRobotCommand(RobotInfo data, GridDTO gridDTO) : base(gridDTO)
        {
            _data = data;
        }
        public override void Execute()
        {
            _receiver = new SetRobotCommandReceiver();

            // Set robot

            string[] splitted = _data.InitialPosition.Split(" ");

            ModuleDTO robotModule = _receiver.SetRobot(splitted);

            Result = _receiver.SetOnGrid(_grid, robotModule, splitted[2], _data.Path);

        }

    }
}
