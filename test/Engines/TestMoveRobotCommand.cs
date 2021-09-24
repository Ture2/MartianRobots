using MartianRobots.Database.Entities;
using MartianRobots.Helpers.Receiver;
using MartianRobots.Models;
using MartianRobots.Models.Grids;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MartianRobots.UnitTest.Engines
{
    public class TestMoveRobotCommand
    {
        private MoveRobotCommandReceiver _receiver = new MoveRobotCommandReceiver();

        [Fact]
        public void TestTurnRigthFromNorth()
        {
            RobotDTO robot = new RobotDTO() { CurrentOrientation = Orientation.N };
            Assert.Equal(Orientation.E, _receiver.TurnRight(robot).CurrentOrientation);
        }
        [Fact]
        public void TestTurnRigthFromEast()
        {
            RobotDTO robot = new RobotDTO() { CurrentOrientation = Orientation.E };
            Assert.Equal(Orientation.S, _receiver.TurnRight(robot).CurrentOrientation);
        }

        [Fact]
        public void TestTurnRigthFromWest()
        {
            RobotDTO robot = new RobotDTO() { CurrentOrientation = Orientation.W };
            Assert.Equal(Orientation.N, _receiver.TurnRight(robot).CurrentOrientation);
        }
        [Fact]
        public void TestTurnRigthFromSouth()
        {
            RobotDTO robot = new RobotDTO() { CurrentOrientation = Orientation.S };
            Assert.Equal(Orientation.W, _receiver.TurnRight(robot).CurrentOrientation);
        }

        [Fact]
        public void TestTurnLeftFromWest()
        {
            RobotDTO robot = new RobotDTO() { CurrentOrientation = Orientation.W };
            Assert.Equal(Orientation.S, _receiver.TurnLeft(robot).CurrentOrientation);
        }
        [Fact]
        public void TestTurnLeftFromSouth()
        {
            RobotDTO robot = new RobotDTO() { CurrentOrientation = Orientation.S };
            Assert.Equal(Orientation.E, _receiver.TurnLeft(robot).CurrentOrientation);
        }
        [Fact]
        public void TestTurnLeftFromNorth()
        {
            RobotDTO robot = new RobotDTO() { CurrentOrientation = Orientation.N };
            Assert.Equal(Orientation.W, _receiver.TurnLeft(robot).CurrentOrientation);
        }
        [Fact]
        public void TestTurnLeftFromEast()
        {
            RobotDTO robot = new RobotDTO() { CurrentOrientation = Orientation.E };
            Assert.Equal(Orientation.N, _receiver.TurnLeft(robot).CurrentOrientation);
        }

        [InlineData(50,50,51,50)]
        [InlineData(50, 50, 51, 51)]
        [InlineData(50, 50, 0, -1)]
        [InlineData(0, 0, 0, 0)]
        [InlineData(1, 1, -1, 0)]
        [InlineData(0, 0, 0, 1)]
        [InlineData(0, 0, 1, 1)]
        [InlineData(2, 3, 1, 3)]
        [InlineData(2, 3, 2, 2)]
        [InlineData(2, 3, -1, 0)]
        [Theory]
        public void TestOutOfRangeTrue(int maxX, int maxY, int x, int y)
        { 
            Assert.True(_receiver.IsOutOfSafeZone(maxX, maxY, x, y));
        }

        [Theory]
        [MemberData(nameof(UpdatePosData))]
        public void TestUpdatePosition(GridDTO grid, RobotDTO robot, int x, int y)
        {
            int prevPositionX = robot.CurrentPosition.X;
            int prevPositionY = robot.CurrentPosition.Y;
            var res = _receiver.UpdateRobotPosition(grid, x, y);

            // Prev pos is not busy 
            Assert.False(res.Grid[prevPositionY, prevPositionX].Busy);

            // New pos is busy
            Assert.True(res.Grid[y, x].Busy);

            // New coordinates are correct
            Assert.Equal(res.CurrentRobotExploring.CurrentPosition.X, x);
            Assert.Equal(res.CurrentRobotExploring.CurrentPosition.Y, y);
        }


        [Theory]
        [MemberData(nameof(MoveFowardData))]
        public void TestMoveFoward(GridDTO grid, RobotDTO robot, int x, int y)
        {
            _receiver.MoveFoward(grid, robot, x, y);
        }



        #region data
        public static IEnumerable<object[]> MoveFowardData()
        {
            yield return new object[]
            {

            };
        }

        public static IEnumerable<object[]> UpdatePosData()
        {
            // Grid expected preparation
            ModuleDTO[,] grid = MockGenerator.GetEmptyGrid(5, 3);
            ModuleDTO toModule = new ModuleDTO()
            {
                X = 1,
                Y = 1,
                Busy = true,
                Danger = false
            };
            grid[1, 1] = toModule;
            RobotDTO robot = new RobotDTO()
            {
                CurrentPosition = toModule,
                CurrentOrientation = Orientation.E,
                Path = "RFRFRFRF"
            };

            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = grid,
                    RobotList = new List<RobotDTO>(),
                    CurrentRobotExploring = robot
                },
                robot,
                1, 
                2 // Up 
            };
            yield return new object[]
           {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = grid,
                    RobotList = new List<RobotDTO>(),
                    CurrentRobotExploring = robot
                },
                robot,
                1,
                0 // Down
           };
            yield return new object[]
           {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = grid,
                    RobotList = new List<RobotDTO>(),
                    CurrentRobotExploring = robot
                },
                robot,
                2, // Right
                1   
           };
            yield return new object[]
           {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = grid,
                    RobotList = new List<RobotDTO>(),
                    CurrentRobotExploring = robot
                },
                robot,
                0, // Left 
                1
           };
        }

        public static IEnumerable<object[]> TurnData()
        {
            yield return new object []
            {
                new RobotDTO() { CurrentOrientation = Orientation.N },
                new RobotDTO() { CurrentOrientation = Orientation.W },
                new RobotDTO() { CurrentOrientation = Orientation.E },
                new RobotDTO() { CurrentOrientation = Orientation.S },

            };
        }

        public static IEnumerable<object[]> ValidateData()
        {
            // Grid expected preparation
            ModuleDTO[,] grid = MockGenerator.GetEmptyGrid(5, 3);
            ModuleDTO toModule = new ModuleDTO()
            {
                X = 1,
                Y = 1,
                Busy = true,
                Danger = false
            };
            grid[1, 1] = toModule;
            RobotDTO robot = new RobotDTO()
            {
                CurrentPosition = toModule,
                NumberOfMoves = 100
            };

            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = grid,
                    RobotList = new List<RobotDTO>(),
                    CurrentRobotExploring = robot
                },
                robot,
                1,
                2 // Up 
            };
        }

        # endregion
    }
}
