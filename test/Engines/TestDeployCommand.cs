using MartianRobots.Database.Entities;
using MartianRobots.Helpers.Commands;
using MartianRobots.Models;
using MartianRobots.Models.Grids;
using MartianRobots.Shared.Interfaces.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace MartianRobots.UnitTest.Engines
{

    public class TestDeployCommand
    {

        private IDeployCommand _command;
        private DeployDTO dto;

        [Theory]
        [MemberData(nameof(DeployData))]
        public void ExcuteRightDeploys(GridDTO expected, DeployDTO input)
        {
            // Ac
            _command = new DeployCommand(input);
            _command.Execute();

            // Assert

            _command.Result.Should().BeEquivalentTo(expected);
            
        }

        public static IEnumerable<object[]> DeployData()
        {
            yield return new object[]
            {
                new GridDTO(){ 
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = GetEmptyGrid(5,3),
                    RobotList = new List<RobotDTO>()
                }
                ,
            new DeployDTO()
            {
                GridSize = "5 3",
                RobotInfoList = new RobotInfo [] {
                    new RobotInfo()
                    {
                        InitialPosition = "1 1 E",
                        Path = "RFRFRFRF"
                    },
                    new RobotInfo()
                    {
                        InitialPosition = "3 2 N",
                        Path = "FRRFLLFFRRFLL"
                    },
                    new RobotInfo()
                    {
                        InitialPosition = "0 3 W",
                        Path = "LLFFFRFLFL"
                    }
                }
                }
            };
        }

        public static ModuleDTO[,] GetEmptyGrid(int x , int y)
        {
            x++;
            y++;
            ModuleDTO[,] gridModules = new ModuleDTO[y, x];

            for (int j = 0; j < y; j++)
            {
                for (int i = 0; i < x; i++)
                {
                    gridModules[j, i] = new ModuleDTO()
                    {
                        X = i,
                        Y = j,
                        Danger = false,
                        Busy = false
                    };
                }
            }
            return gridModules;
        }
    }
}
