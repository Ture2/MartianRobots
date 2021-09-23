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
            GridDTO updatedGrid = grid;

            if (robot.CurrentOrientation == Orientation.N)
                y++;
            if (robot.CurrentOrientation == Orientation.S)
                y--;
            if (robot.CurrentOrientation == Orientation.E)
                x++;
            if (robot.CurrentOrientation == Orientation.W)
                x--;
            if (OutOfRange(grid.maxN, grid.maxN, x, y))
            {
                if (!robot.CurrentPosition.Danger)
                {
                    updatedGrid =  FinishRobotOperation(grid); // Danger zone without advise = end
                }
            }
            else
            {
                updatedGrid = UpdateRobotPosition(grid, robot, x, y); // No out of range so continue
            }
            return updatedGrid;
        }

        public RobotDTO TurnRight(RobotDTO robot)
        {
            if (robot.CurrentOrientation - 3 > Orientation.W)
                robot.CurrentOrientation = Orientation.N;
            else
                robot.CurrentOrientation--;
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

        public GridDTO UpdateRobotPosition(GridDTO grid, RobotDTO robot, int x, int y)
        {
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

        public GridDTO FinishRobotOperation(GridDTO grid)
        {
            grid.Grid[grid.CurrentRobotExploring.CurrentPosition.Y, grid.CurrentRobotExploring.CurrentPosition.X].Busy = false;
            grid.Grid[grid.CurrentRobotExploring.CurrentPosition.Y, grid.CurrentRobotExploring.CurrentPosition.X].Danger = true;
            grid.RobotList.Add(grid.CurrentRobotExploring);
            grid.CurrentRobotExploring = null;
            return grid;
        }

        public GridDTO ValidateMove(GridDTO grid)
        {
            
            if (grid.CurrentRobotExploring.NumberOfMoves == 100)
            {
                grid.Grid[grid.CurrentRobotExploring.CurrentPosition.Y, grid.CurrentRobotExploring.CurrentPosition.X].Busy = false;
                grid.Grid[grid.CurrentRobotExploring.CurrentPosition.Y, grid.CurrentRobotExploring.CurrentPosition.X].Danger = true;
                grid.RobotList.Add(grid.CurrentRobotExploring);
                grid.CurrentRobotExploring = null;
            }
            return grid;
        }

        private bool OutOfRange(int maxX, int maxY, int x, int y)
        {
            return x == maxX || y == maxY;
        }

    }
}
