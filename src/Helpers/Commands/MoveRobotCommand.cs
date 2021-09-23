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
        public MoveRobotCommand(Instruction instruction, GridDTO gridDTO) : base(gridDTO)
        {
            _i = instruction;
        }
        public override void Execute()
        {
            Move(_grid.CurrentRobotExploring,_i);
        }

        public void Move(RobotDTO robot, Instruction i)
        {
            int x = robot.CurrentPosition.X;
            int y = robot.CurrentPosition.Y;

            if(i == Instruction.F) {

                
                    if (robot.CurrentOrientation == Orientation.N)
                        y++;
                    if (robot.CurrentOrientation == Orientation.S)
                        y--;
                    if (robot.CurrentOrientation == Orientation.E)
                        x++;
                    if (robot.CurrentOrientation == Orientation.W)
                        x--;
                if(OutOfRange(x, y)){
                    if (!robot.CurrentPosition.Danger)
                    {
                        FinishRobotOperation(robot);
                    }
                }
                else
                {
                    UpdateRobotPosition(robot, x, y);
                }

            }
            if (i == Instruction.L){
                if (robot.CurrentOrientation - 1 < Orientation.N)
                    robot.CurrentOrientation = Orientation.W;
                else
                    robot.CurrentOrientation--;
            }
            if (i == Instruction.R)
            {
                if (robot.CurrentOrientation - 3 > Orientation.W)
                    robot.CurrentOrientation = Orientation.N;
                else
                    robot.CurrentOrientation--;
            }

            robot.NumberOfMoves++;

            ValidateMove(robot);
        }
        public void UpdateRobotPosition(RobotDTO robot, int x, int y)
        {
            // Leave module 
            _grid.Grid[robot.CurrentPosition.Y, robot.CurrentPosition.X].Busy = false;

            robot.CurrentPosition.X = x;
            robot.CurrentPosition.Y = y;

            //New module
            _grid.Grid[y, x] = robot.CurrentPosition;
            _grid.Grid[y, x].Busy = true;
            _grid.CurrentRobotExploring = robot;

        }

        public void FinishRobotOperation(RobotDTO robot)
        {
            _grid.Grid[robot.CurrentPosition.Y, robot.CurrentPosition.X].Busy = false;
            _grid.Grid[robot.CurrentPosition.Y, robot.CurrentPosition.X].Danger = true;
            _grid.RobotList.Add(robot);
            _grid.CurrentRobotExploring = null;
        }

        private void ValidateMove(RobotDTO robot)
        {
            if(robot.NumberOfMoves == 100)
            {
                _grid.Grid[robot.CurrentPosition.Y, robot.CurrentPosition.X].Busy = false;
                _grid.Grid[robot.CurrentPosition.Y, robot.CurrentPosition.X].Danger = true;
                _grid.RobotList.Add(robot);
                _grid.CurrentRobotExploring = null;
            }
        }

        private bool OutOfRange(int x, int y) {
            return x == _grid.XAxisLength || y == _grid.YAxisLength;
     }
    }
}
