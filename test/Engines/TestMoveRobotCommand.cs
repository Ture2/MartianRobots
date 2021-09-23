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




        [Theory]
        [MemberData(nameof(MoveFowardData))]
        public void TestMoveFoward()
        {
            //_receiver.MoveFoward();
        }



        #region data
        public static IEnumerable<object[]> MoveFowardData()
        {
            yield return new object[]
            {

            };
        }

        public static IEnumerable<object[]> TurnRightData()
        {
            yield return new object []
            {
                new RobotDTO() { CurrentOrientation = Orientation.N },
                new RobotDTO() { CurrentOrientation = Orientation.W },
                new RobotDTO() { CurrentOrientation = Orientation.E },
                new RobotDTO() { CurrentOrientation = Orientation.S },

            };
        }

        # endregion
    }
}
