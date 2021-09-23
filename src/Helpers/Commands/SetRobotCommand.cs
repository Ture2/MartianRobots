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
        public SetRobotCommand(RobotInfo data, GridDTO gridDTO) : base(gridDTO)
        {
            _data = data;
        }
        public override void Execute()
        {
            // Set robot

            string[] splitted = _data.InitialPosition.Split(" ");
            Enum.TryParse(splitted[2], out Orientation orientation);
            ModuleDTO robotModule = new ModuleDTO()
            {
                X = int.Parse(splitted[0]),
                Y = int.Parse(splitted[1]),
                Busy = true,
                Danger = IsUnderDangerZone(int.Parse(splitted[0]), int.Parse(splitted[1]), _grid.XAxisLength, _grid.YAxisLength)
            };

            RobotDTO robotDTO = new RobotDTO()
            {
                CurrentPosition = robotModule,
                CurrentOrientation = orientation,
                Path = _data.Path
            };

            // Set robot on planet 

            _grid.Grid[robotModule.Y, robotModule.X] = robotModule;
            _grid.CurrentRobotExploring = robotDTO;
            Result = _grid;

        }
        public GridDTO GetResult()
        {
            return Result;

        }
        private bool IsUnderDangerZone(int xPos, int yPos, int xLength, int yLength)
        {
            bool xAxisDanger = false;
            bool yAxisDanger = false;

            if(xPos == xLength - 1  || xPos == 0)
            {
                yAxisDanger = true;
            }
            if (yPos == yLength - 1 || yPos == 0)
            {
                xAxisDanger = true;
            }
            return xAxisDanger && yAxisDanger;
        }
    }
}
