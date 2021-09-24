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
        [MemberData(nameof(MoveNormalFowardEast))]
        public void TestNormalMoveFowardEast(params int[] expected)
        {
            // 

            ModuleDTO[,] grid = MockGenerator.GetEmptyGrid(5, 3);
            ModuleDTO toModule = new ModuleDTO()
            {
                X = expected[0],
                Y = expected[1],
                Busy = true,
                Danger = false
            };
            grid[expected[1], expected[0]] = toModule;
            RobotDTO robot = new RobotDTO()
            {
                CurrentPosition = toModule,
                CurrentOrientation = Orientation.E,
            };

            GridDTO dto = new GridDTO()
            {
                XAxisLength = 6,
                YAxisLength = 4,
                Planet = Planet.Mars,
                Grid = grid,
                RobotList = new List<RobotDTO>(),
                CurrentRobotExploring = robot
            };

            var res = _receiver.MoveFoward(dto);


            Assert.Equal(expected[2], res.CurrentRobotExploring.CurrentPosition.X); // X pos
            Assert.Equal(expected[3], res.CurrentRobotExploring.CurrentPosition.Y); // Y pos
            Assert.False(res.Grid[expected[1],expected[0]].Busy); // Busy old
            Assert.True(res.Grid[expected[3], expected[2]].Busy); // Busy new

        }

        [InlineData(2,2,0,2)]
        [Theory]
        public void TestLostRobotMove(int xGridSize, int yGridSize, int xStartPoint, int yStartPoint)
        {

            ModuleDTO[,] grid = MockGenerator.GetEmptyGrid(xGridSize, yGridSize);
            ModuleDTO toModule = new ModuleDTO()
            {
                X = xStartPoint,
                Y = yStartPoint,
                Busy = true,
                Danger = false
            };
            grid[yStartPoint, xStartPoint] = toModule;
            RobotDTO robot = new RobotDTO()
            {
                CurrentPosition = toModule,
                CurrentOrientation = Orientation.E,
            };

            GridDTO dto = new GridDTO()
            {
                XAxisLength = xGridSize + 1,
                YAxisLength = yGridSize + 1,
                Planet = Planet.Mars,
                Grid = grid,
                RobotList = new List<RobotDTO>(),
                CurrentRobotExploring = robot
            };

            var res = _receiver.OutOfSafeZoneMove(dto);


            Assert.Null(res.CurrentRobotExploring.CurrentPosition); // old busy
        }

        [InlineData(2, 2, 0, 0, Instruction.F, 0, 1, Orientation.N, Orientation.N, 1, true)]
        [InlineData(2, 2, 0, 1, Instruction.L, 0, 1, Orientation.W, Orientation.S, 1, false)]
        [InlineData(2, 2, 0, 2, Instruction.R, 0, 2, Orientation.E, Orientation.S, 1, false)]
        [InlineData(2, 2, 0, 2, Instruction.F, 0, 1, Orientation.S, Orientation.S, 1, true)]
        [InlineData(2, 2, 1, 1, Instruction.F, 2, 1, Orientation.E, Orientation.E, 1, true)]
        [InlineData(2, 2, 2, 2, Instruction.F, 1, 2, Orientation.W, Orientation.W, 1, true)]
        [Theory]
        public void TestMove(int xGridSize, int yGridSize, int xStartPoint, int yStartPoint, Instruction i,
            int expectedX, int expectedY, Orientation orientation, Orientation expectedOrientation, int numMoves, bool checkBusy)
        {

            ModuleDTO[,] grid = MockGenerator.GetEmptyGrid(xGridSize, yGridSize);
            ModuleDTO toModule = new ModuleDTO()
            {
                X = xStartPoint,
                Y = yStartPoint,
                Busy = true,
                Danger = false
            };
            grid[yStartPoint, xStartPoint] = toModule;
            RobotDTO robot = new RobotDTO()
            {
                CurrentPosition = toModule,
                CurrentOrientation = orientation,
            };

            GridDTO dto = new GridDTO()
            {
                XAxisLength = xGridSize + 1,
                YAxisLength = yGridSize + 1,
                Planet = Planet.Mars,
                Grid = grid,
                RobotList = new List<RobotDTO>(),
                CurrentRobotExploring = robot
            };

            var res = _receiver.Move(dto, i);


            Assert.Equal(res.CurrentRobotExploring.CurrentPosition.X, expectedX);
            Assert.Equal(res.CurrentRobotExploring.CurrentPosition.Y, expectedY);
            Assert.Equal(res.CurrentRobotExploring.CurrentOrientation, expectedOrientation);
            if (checkBusy)
            {
                Assert.True(res.Grid[expectedY, expectedX].Busy);
                Assert.False(res.Grid[yStartPoint, xStartPoint].Busy);
            }
            Assert.Equal(res.CurrentRobotExploring.CurrentPosition, res.Grid[expectedY, expectedX]);
            Assert.Equal(res.CurrentRobotExploring.NumberOfMoves, numMoves);
        }

        [InlineData(2, 2, 2, 2, Orientation.N)]
        [InlineData(2, 2, 2, 2, Orientation.E)]
        [InlineData(2, 2, 0, 2, Orientation.N)]
        [InlineData(2, 2, 0, 2, Orientation.W)]
        [InlineData(2, 2, 2, 0, Orientation.E)]
        [InlineData(2, 2, 2, 0, Orientation.S)]
        [InlineData(2, 2, 0, 0, Orientation.S)]
        [InlineData(2, 2, 0, 0, Orientation.W)]
        [InlineData(50, 50, 0, 0, Orientation.S)]
        [InlineData(50, 50, 0, 0, Orientation.W)]
        [InlineData(50, 50, 0, 50, Orientation.W)]
        [InlineData(50, 50, 0, 50, Orientation.N)]
        [InlineData(50, 50, 50, 50, Orientation.N)]
        [InlineData(50, 50, 50, 50, Orientation.E)]
        [InlineData(50, 50, 50, 0, Orientation.E)]
        [InlineData(50, 50, 50, 0, Orientation.S)]
        [Theory]
        public void TestMoveOutOfIndexRange(int xGridSize, int yGridSize, int xStartPoint, int yStartPoint,
           Orientation orientation)
        {

            ModuleDTO[,] grid = MockGenerator.GetEmptyGrid(xGridSize, yGridSize);
            ModuleDTO toModule = new ModuleDTO()
            {
                X = xStartPoint,
                Y = yStartPoint,
                Busy = true,
                Danger = false
            };
            grid[yStartPoint, xStartPoint] = toModule;
            RobotDTO robot = new RobotDTO()
            {
                CurrentPosition = toModule,
                CurrentOrientation = orientation,
            };

            GridDTO dto = new GridDTO()
            {
                XAxisLength = xGridSize + 1,
                YAxisLength = yGridSize + 1,
                Planet = Planet.Mars,
                Grid = grid,
                RobotList = new List<RobotDTO>(),
                CurrentRobotExploring = robot
            };

            var res = _receiver.Move(dto, Instruction.F);



            Assert.True(res.CurrentRobotExploring.Lost);
            Assert.Null(res.CurrentRobotExploring.CurrentPosition);
            Assert.Equal(res.CurrentRobotExploring.LostCoordinates, res.Grid[yStartPoint, xStartPoint]);
            Assert.False(res.Grid[yStartPoint, xStartPoint].Busy);
            Assert.Contains(res.CurrentRobotExploring, res.RobotList);
            
        }

        [Theory]
        [InlineData(2, 2, 2, 2, Orientation.N)]
        [InlineData(2, 2, 2, 2, Orientation.E)]
        [InlineData(2, 2, 0, 2, Orientation.N)]
        [InlineData(2, 2, 0, 2, Orientation.W)]
        [InlineData(2, 2, 2, 0, Orientation.E)]
        [InlineData(2, 2, 2, 0, Orientation.S)]
        [InlineData(2, 2, 0, 0, Orientation.S)]
        [InlineData(2, 2, 0, 0, Orientation.W)]
        [InlineData(50, 50, 0, 0, Orientation.S)]
        [InlineData(50, 50, 0, 0, Orientation.W)]
        [InlineData(50, 50, 0, 50, Orientation.W)]
        [InlineData(50, 50, 0, 50, Orientation.N)]
        [InlineData(50, 50, 50, 50, Orientation.N)]
        [InlineData(50, 50, 50, 50, Orientation.E)]
        [InlineData(50, 50, 50, 0, Orientation.E)]
        [InlineData(50, 50, 50, 0, Orientation.S)]
        public void TestMoveOutOfIndexRangeWithDangerFlag(int xGridSize, int yGridSize, int xStartPoint, int yStartPoint, Orientation orientation)
        {
            // Arrange
            ModuleDTO[,] grid = MockGenerator.GetEmptyGrid(xGridSize, yGridSize);
            ModuleDTO toModule = new ModuleDTO()
            {
                X = xStartPoint,
                Y = yStartPoint,
                Busy = true,
                Danger = true
            };
            grid[yStartPoint, xStartPoint] = toModule;
            RobotDTO robot = new RobotDTO()
            {
                CurrentPosition = toModule,
                CurrentOrientation = orientation,
            };

            GridDTO dto = new GridDTO()
            {
                XAxisLength = xGridSize + 1,
                YAxisLength = yGridSize + 1,
                Planet = Planet.Mars,
                Grid = grid,
                RobotList = new List<RobotDTO>(),
                CurrentRobotExploring = robot
            };

            //Act
            var res = _receiver.Move(dto, Instruction.F);

            //Assert
            Assert.Equal(res, dto);
            

        }

        #region data
        public static IEnumerable<object[]> MoveNormalFowardEast()
        {
            // FFFF 
            yield return new object[] { 3, 3, 1, 1, 2, 1 };
            yield return new object[] { 3, 3, 2, 1, 3, 1 };
            yield return new object[] { 3, 3, 3, 1, 4, 1 };
            yield return new object[] { 3, 3, 4, 1, 5, 1 };

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
