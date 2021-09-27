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

        [InlineData("-1 -1")]
        [InlineData("0 -1")]
        [InlineData("-1 0")]
        [InlineData("-1 1")]
        [InlineData("1 -1")]
        [InlineData("51 1")]
        [InlineData("1 51")]
        [InlineData("0 51")]
        [InlineData("A 1")]
        [InlineData("1 A")]
        [InlineData("1 1 1")]
        [InlineData("1 A 51")]
        [InlineData("B 0 51")]
        [Theory]
        public void ExcuteWrongData(string input)
        {
            // Ac
            DeployDTO data = new DeployDTO()
            {
                GridSize = input
            };
            _command = new DeployCommand(data);


            // Assert

            Assert.Throws<FormatException>(() => _command.Execute());

        }

        public static IEnumerable<object[]> DeployData()
        {
            yield return new object[]
            {
                new GridDTO(){ 
                    XAxisLength = 6,
                    YAxisLength = 4,
                    Planet = Planet.Mars,
                    Grid = MockGenerator.GetEmptyGrid(5,3),
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

       

    }
}
