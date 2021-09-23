using MartianRobots.Models;
using MartianRobots.Models.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MartianRobots.Helpers.Receivers
{
    public class SetRobotCommandReceiver
    {
        public SetRobotCommandReceiver() { }

        public ModuleDTO SetRobot(string[] input)
        {
            if (input.Length < 2 || input.Equals(null) || input.Length > 4)
                throw new ArgumentException("Input position must have 2 coordinates and just one orientation");
           
            var isNumericX = int.TryParse(input[0], out int x);
            var isNumericY = int.TryParse(input[1], out int y);

            if (!isNumericX || !isNumericY)
                throw new FormatException("Input format is invalid");

            if (x < 0 || y < 0)
                throw new ArgumentException("Coodinates can not be negative");

            ModuleDTO robotModule = new ModuleDTO()
            {
                X = x,
                Y = y,
                Busy = true,
                Danger = false
            };
            return robotModule;
        }

        public GridDTO SetOnGrid(GridDTO grid, ModuleDTO robotModule, string orientation, string path)
        {
            Regex rPath = new Regex("^[frlFRL]+$");
            Regex rOrientation = new Regex("^[nsewNSEW]+$");
            if (!rPath.IsMatch(path))
                throw new ArgumentException("Path argument just use F, L or R letters");
            if(!rOrientation.IsMatch(orientation))
                throw new ArgumentException("Orientation argument just use N, W, E or S letters");
            
            RobotDTO robotDTO = new RobotDTO()
            {
                CurrentPosition = robotModule,
                CurrentOrientation = OrientationParse(orientation),
                Path = path
            };

            // Set robot on planet 

            grid.Grid[robotModule.Y, robotModule.X] = robotModule;
            grid.CurrentRobotExploring = robotDTO;
            return grid;
        }

        private Orientation OrientationParse(string o)
        {
            switch (o)
            {
                case "N": return Orientation.N;
                case "S": return Orientation.S;
                case "W": return Orientation.W;
                case "E": return Orientation.E;
                default: return Orientation.N;
            }
        }
    }
}
