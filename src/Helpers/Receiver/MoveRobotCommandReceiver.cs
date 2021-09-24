using MartianRobots.Models;
using MartianRobots.Models.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Helpers.Receiver
{
    public class MoveRobotCommandReceiver
    {

        public MoveRobotCommandReceiver() { }


        public GridDTO Move(GridDTO grid, Instruction i)
        {
            RobotDTO robot = grid.CurrentRobotExploring;
            int x = robot.CurrentPosition.X;
            int y = robot.CurrentPosition.Y;

            if (i == Instruction.F)
                grid = MoveFoward(grid, robot, x, y);
            if (i == Instruction.L)
            {
                grid.CurrentRobotExploring = TurnLeft(robot);
            }
            if (i == Instruction.R)
            {
                grid.CurrentRobotExploring = TurnRight(robot);
            }

            grid.CurrentRobotExploring.NumberOfMoves++;

            return grid;
            
        }

        public GridDTO MoveFoward(GridDTO grid, RobotDTO robot, int x, int y)
        {
            if (robot.CurrentOrientation == Orientation.N)
                y++;
            if (robot.CurrentOrientation == Orientation.S)
                y--;
            if (robot.CurrentOrientation == Orientation.E)
                x++;
            if (robot.CurrentOrientation == Orientation.W)
                x--;
            if (IsOutOfSafeZone(grid.maxN, grid.maxN, x, y))
            {
                return OutOfSafeZoneMove(grid);
            }
            else
            {
                return UpdateRobotPosition(grid, x, y); // No out of range so continue
            }
        }

        public RobotDTO TurnRight(RobotDTO robot)
        {
            if (robot.CurrentOrientation + 1 > Orientation.W)
                robot.CurrentOrientation = Orientation.N;
            else
                robot.CurrentOrientation++;
            return robot;
        }

        public RobotDTO TurnLeft(RobotDTO robot)
        {
            if (robot.CurrentOrientation - 1 < Orientation.N)
                robot.CurrentOrientation = Orientation.W;
            else
                robot.CurrentOrientation--;
            return robot;
        }

        public GridDTO UpdateRobotPosition(GridDTO grid, int x, int y)
        {
            RobotDTO robot = grid.CurrentRobotExploring;

            // Leave module 
            grid.Grid[robot.CurrentPosition.Y, robot.CurrentPosition.X].Busy = false;

            robot.CurrentPosition.X = x;
            robot.CurrentPosition.Y = y;

            //New module
            grid.Grid[y, x] = robot.CurrentPosition;
            grid.Grid[y, x].Busy = true;
            grid.CurrentRobotExploring = robot;

            return grid;
        }

        public GridDTO OutOfSafeZoneMove(GridDTO grid)
        {
            if (!grid.CurrentRobotExploring.CurrentPosition.Danger)
            {
                // Danger zone without advise = end
                grid.Grid[grid.CurrentRobotExploring.CurrentPosition.Y, grid.CurrentRobotExploring.CurrentPosition.X].Busy = false;
                grid.Grid[grid.CurrentRobotExploring.CurrentPosition.Y, grid.CurrentRobotExploring.CurrentPosition.X].Danger = true;
                grid.CurrentRobotExploring.Lost = true;
                grid.CurrentRobotExploring.LostCoordinates = new ModuleDTO { X = grid.CurrentRobotExploring.CurrentPosition.X, Y = grid.CurrentRobotExploring.CurrentPosition.Y };
                grid.RobotList.Add(grid.CurrentRobotExploring);
            }
            return grid;
        }

        public GridDTO ValidateMove(GridDTO grid)
        {
            
            if (grid.CurrentRobotExploring.NumberOfMoves == 100)
            {
                grid.Grid[grid.CurrentRobotExploring.CurrentPosition.Y, grid.CurrentRobotExploring.CurrentPosition.X].Busy = false;
                grid.RobotList.Add(grid.CurrentRobotExploring);
                grid.CurrentRobotExploring.MissionEnded = true;
            }
            return grid;
        }

        public bool IsOutOfSafeZone(int maxX, int maxY, int x, int y)
        {
            bool top = (x >= maxX | y >= maxY);
            bool bottom = (x < 0 | y < 0);

            return top | bottom;
        }

    }
}
