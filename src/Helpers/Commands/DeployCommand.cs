using MartianRobots.Database.Entities;
using MartianRobots.Models;
using MartianRobots.Models.Grids;
using MartianRobots.Shared.Interfaces.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MartianRobots.Helpers.Commands
{
    public class DeployCommand : IDeployCommand
    {
        private readonly DeployDTO _deploy;
        private GridDTO _grid;
        public GridDTO Result { get; private set; }

        public DeployCommand(DeployDTO deploy)
        {
            _deploy = deploy;
        }


        public void Execute()
        {
            // Grid Size

            string[] input = _deploy.GridSize.Split(" ");

            var isNumericX = int.TryParse(input[0], out int x);
            var isNumericY = int.TryParse(input[1], out int y);

            if (!isNumericX | !isNumericY | input.Length > 2 | input.Length == 0)
                throw new FormatException("Input format is invalid");

            if (x < 0 || y < 0)
                throw new FormatException("Input must be greater than 0");
            if (x > 50 || y > 50)
                throw new FormatException("Input can not be greater than 50");

            // Grid modules creation
            x++;
            y++;
            ModuleDTO[,] gridModules = new ModuleDTO[y, x];

            for (int j = 0; j < y; j++) {
                for (int i = 0; i < x; i++)
                { 
                    gridModules[j,i] = new ModuleDTO()
                    {
                        X = i,
                        Y = j,
                        Danger = false,
                        Busy = false
                    };
                }
            }


            _grid = new GridDTO()
            {
                XAxisLength = x,
                YAxisLength = y,
                Planet = Planet.Mars,
                Grid = gridModules,
                RobotList = new List<RobotDTO>()
            };


            Result = _grid;

        }

    }

}
