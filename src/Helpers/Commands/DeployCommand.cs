using MartianRobots.Database.Entities;
using MartianRobots.Models;
using MartianRobots.Models.Grids;
using MartianRobots.Shared.Interfaces.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
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
            string[] splitted = _deploy.GridSize.Split(" ");
            int x = int.Parse(splitted[0]) + 1;
            int y = int.Parse(splitted[1]) + 1;


            // First robot module

            //ModuleDTO robotInitialPosition = new ModuleDTO
            //{
            //    X = int.Parse(_deploy.RobotInfos.RobotInitialPosition[0]),
            //    Y = int.Parse(_deploy.RobotInitialPosition[1]),
            //};


            // Grid modules creation

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

            //gridModules[robotInitialPosition.Y, robotInitialPosition.X].Busy = true;

            _grid = new GridDTO()
            {
                XAxisLength = x,
                YAxisLength = y,
                Planet = Planet.Mars,
                Grid = gridModules,
                RobotList = new List<RobotDTO>()
            };

            /*_grid = new GridDTO()
            {
                XAxisLength = x,
                YAxisLength = y,
                Planet = Planet.Mars,
                Grid = gridModules,
                CurrentRobotExploring = new RobotDTO() 
                {
                    Path = _deploy.Path
                },
                RobotList = new List<RobotDTO>()
            };*/

            Result = _grid;

        }

    }

}
