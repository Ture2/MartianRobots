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


        public void Move(GridDTO grid, Instruction i)
        {
            if (i == Instruction.F)
                MoveFoward(grid);
            if (i == Instruction.L)
            {
                grid.CurrentRobotExploring = TurnLeft(grid.CurrentRobotExploring);
            }
            if (i == Instruction.R)
            {
                grid.CurrentRobotExploring = TurnRight(grid.CurrentRobotExploring);
            }

            grid.CurrentRobotExploring.NumberOfMoves++;
        }

        public void MoveFoward(GridDTO grid)
        {
            RobotDTO robot = grid.CurrentRobotExploring;
            int x = robot.CurrentPosition.X;
            int y = robot.CurrentPosition.Y;

            if (robot.CurrentOrientation == Orientation.N)
                y++;
            if (robot.CurrentOrientation == Orientation.S)
                y--;
            if (robot.CurrentOrientation == Orientation.E)
                x++;
            if (robot.CurrentOrientation == Orientation.W)
                x--;
            if (IsOutOfSafeZone(grid.XAxisLength, grid.YAxisLength, x, y))
            {
                OutOfSafeZoneMove(grid);
            }
            else
            {
                UpdateRobotPosition(grid, x, y); // No out of range so continue
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

        public void UpdateRobotPosition(GridDTO grid, int x, int y)
        {
            if (x < 0 | y < 0)
                throw new ArgumentOutOfRangeException();

            int oldx = grid.CurrentRobotExploring.CurrentPosition.X;
            int oldy = grid.CurrentRobotExploring.CurrentPosition.Y;
            
            grid.Grid[oldy, oldx].Busy = false;
            grid.Grid[y, x].Busy = true;

            grid.CurrentRobotExploring.CurrentPosition = grid.Grid[y, x];
        }

        public void OutOfSafeZoneMove(GridDTO grid)
        {
            if (!grid.CurrentRobotExploring.CurrentPosition.Danger)
            {
                // Danger zone without advise = end
                grid.Grid[grid.CurrentRobotExploring.CurrentPosition.Y, grid.CurrentRobotExploring.CurrentPosition.X].Busy = false;
                grid.Grid[grid.CurrentRobotExploring.CurrentPosition.Y, grid.CurrentRobotExploring.CurrentPosition.X].Danger = true;
                grid.CurrentRobotExploring.Lost = true;
                grid.CurrentRobotExploring.LostCoordinates = grid.CurrentRobotExploring.CurrentPosition;
                grid.CurrentRobotExploring.CurrentPosition = null;
                grid.RobotList.Add(grid.CurrentRobotExploring);
            }
        }

        public void ValidateLastMove(GridDTO grid)
        {
            grid.Grid[grid.CurrentRobotExploring.CurrentPosition.Y, grid.CurrentRobotExploring.CurrentPosition.X].Busy = false;
            grid.RobotList.Add(grid.CurrentRobotExploring);
            grid.CurrentRobotExploring.MissionEnded = true;
        }

        public bool IsOutOfSafeZone(int maxX, int maxY, int x, int y)
        {
            bool top = (x >= maxX | y >= maxY);
            bool bottom = (x < 0 | y < 0);

            return top | bottom;
        }

    }
}
