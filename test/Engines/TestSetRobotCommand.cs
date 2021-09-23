using FluentAssertions;
using MartianRobots.Database.Entities;
using MartianRobots.Helpers.Commands;
using MartianRobots.Helpers.Receivers;
using MartianRobots.Models;
using MartianRobots.Models.Grids;
using MartianRobots.Shared.Interfaces.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MartianRobots.UnitTest.Engines
{
    public class TestSetRobotCommand
    {

        private SetRobotCommandReceiver _receiver = new SetRobotCommandReceiver();
        private RobotDTO dto;

        [Theory]
        [MemberData(nameof(SetRobotData))]
        public void SetRobotTest(ModuleDTO expected, RobotInfo input)
        {
            string[] splitted = input.InitialPosition.Split(" ");
            ModuleDTO dto = _receiver.SetRobot(splitted);
            dto.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(SetRobotNegativeData))]
        public void SetRobotNegativePositionTest(RobotInfo input)
        {
            string[] splitted = input.InitialPosition.Split(" ");
            Action act = () => _receiver.SetRobot(splitted);
            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [MemberData(nameof(SetRobotParseData))]
        public void SetRobotParseTest(RobotInfo input)
        {
            string[] splitted = input.InitialPosition.Split(" ");
            Action act = () => _receiver.SetRobot(splitted);
            Assert.Throws<FormatException>(act);
        }

        [Theory]
        [MemberData(nameof(SetRobotLessOrMoreData))]
        public void SetRobotLessOrMoreTest(RobotInfo input)
        {
            string[] splitted = input.InitialPosition.Split(" ");
            Action act = () => _receiver.SetRobot(splitted);
            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [MemberData(nameof(SetOnGridData))]
        public void SetOnGridTest(GridDTO expected, GridDTO grid, ModuleDTO robotModule, string orientation, string path)
        {
            GridDTO dto = _receiver.SetOnGrid(grid, robotModule, orientation, path);
            dto.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(SetOnGridWrongPathData))]
        public void SetOnGridWrongPathTest(GridDTO grid, ModuleDTO robotModule, string orientation, string path)
        {
            GridDTO dto = _receiver.SetOnGrid(grid, robotModule, orientation, path);
            Assert.Throws<ArgumentException>(() => _receiver.SetOnGrid(grid, robotModule, orientation, path));

        }

        #region Data
        public static IEnumerable<object[]> SetRobotData()
        {

            yield return new object[]
            {
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                }
                ,
                new RobotInfo()
                {
                    InitialPosition = "1 1 E",
                    Path = "RFRFRFRF"
                }
            };
            yield return new object[]
            {
                new ModuleDTO()
                {
                    X = 3,
                    Y = 2,
                    Busy = true,
                    Danger = false
                }
                ,
                new RobotInfo()
                {
                    InitialPosition = "3 2 N",
                    Path = "FRRFLLFFRRFLL"
                }
            };
            yield return new object[]
            {
                new ModuleDTO()
                {
                    X = 0,
                    Y = 3,
                    Busy = true,
                    Danger = false
                }
                ,
                new RobotInfo()
                {
                    InitialPosition = "0 3 W",
                    Path = "LLFFFRFLFL"
                }
            };

        }

        public static IEnumerable<object[]> SetRobotNegativeData()
        {

            yield return new object[]
            {
                new RobotInfo()
                {
                    InitialPosition = "-1 1 E",
                    Path = "RFRFRFRF"
                },
            };
            yield return new object[] {new RobotInfo()
                {
                    InitialPosition = "-2 0 N",
                    Path = "FRRFLLFFRRFLL"
                } };
            yield return new object[] {new RobotInfo()
                {
                    InitialPosition = "0 -40 W",
                    Path = "LLFFFRFLFL"
                }};
            yield return new object[] {new RobotInfo()
                {
                    InitialPosition = "-2 -1 W",
                    Path = "LLFFFRFLFL"
                } };
            yield return new object[] {new RobotInfo()
                {
                    InitialPosition = "0 -1 W",
                    Path = "LLFFFRFLFL"
                } };
            yield return new object[] {new RobotInfo()
                {
                    InitialPosition = "-1 0 W",
                    Path = "LLFFFRFLFL"
                } };




        }

        public static IEnumerable<object[]> SetRobotParseData()
        {

            yield return new object[]
            {
                new RobotInfo()
                {
                    InitialPosition = "1 E",
                    Path = "RFRFRFRF"
                },
            };
            yield return new object[] {new RobotInfo()
                {
                    InitialPosition = "-40 W",
                    Path = "LLFFFRFLFL"
                }};
            yield return new object[] {new RobotInfo()
                {
                    InitialPosition = "-2-1 W",
                    Path = "LLFFFRFLFL"
                } };
            yield return new object[] {new RobotInfo()
                {
                    InitialPosition = "-1 0W",
                    Path = "LLFFFRFLFL"
                } };
            yield return new object[] {new RobotInfo()
                {
                    InitialPosition = "-1F 0W",
                    Path = "LLFFFRFLFL"
                } };




        }

        public static IEnumerable<object[]> SetRobotLessOrMoreData()
        {

            yield return new object[]
            {
                new RobotInfo()
                {
                    InitialPosition = "1",
                    Path = "RFRFRFRF"
                },
            };
            yield return new object[] {new RobotInfo()
                {
                    InitialPosition = "",
                    Path = "LLFFFRFLFL"
                }};
            yield return new object[] {new RobotInfo()
                {
                    InitialPosition = "W",
                    Path = "LLFFFRFLFL"
                } };
            yield return new object[] {new RobotInfo()
                {
                    InitialPosition = "-1",
                    Path = "LLFFFRFLFL"
                } };
            yield return new object[] {new RobotInfo()
                {
                    InitialPosition = "-1 F F A ",
                    Path = "LLFFFRFLFL"
                } };
            yield return new object[] {new RobotInfo()
                {
                    InitialPosition = "-1  A F EE A",
                    Path = "LLFFFRFLFL"
                } };
        }

        public static IEnumerable<object[]> SetOnGridData()
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
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "E",
                "RFRFRFRF"
            };
        }

        public static IEnumerable<object[]> SetOnGridRightPathData()
        {

            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "E",
                "AA"
            };
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "E",
                "FA"
            };
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "E",
                ""
            };
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "E",
                "aaF"
            };
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "E",
                "F F F"
            };
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "E",
                "12 -A"
            };
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "E",
                "FL R "
            };
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "E",
                "FRRR A"
            };
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "",
                "F"
            };
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "",
                ""
            };
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "W E",
                "RRR"
            };
        }

        public static IEnumerable<object[]> SetOnGridWrongPathData()
        {
           
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "E",
                "AA"
            };
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "E",
                "FA"
            };
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "E",
                ""
            };
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "E",
                "aaF"
            };
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "E",
                "F F F"
            };
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "E",
                "12 -A"
            };
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "E",
                "FL R "
            };
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "E",
                "FRRR A"
            };
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "",
                "F"
            };
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "",
                ""
            };
            yield return new object[]
            {
                new GridDTO(){
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                },
                new ModuleDTO()
                {
                    X = 1,
                    Y = 1,
                    Busy = true,
                    Danger = false
                },
                "W E",
                "RRR"
            };
        }

#endregion
    }
}
