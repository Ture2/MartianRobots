using MartianRobots.Database.Contexts;
using MartianRobots.Database.Entities;
using MartianRobots.Database.Repositories;
using Moq;
using System;
using Xunit;

namespace MartianRobots.UnitTest.Database
{
    public class Repositories
    {
        private StartUp _startUp = new StartUp();
        private MartianRobotsRepository<Robot> _read;
        private MartianRobotsRepository<Robot> _write;


        [Fact]
        public void Init()
        {
            _read = _startUp.GetInMemoryReadRepository();
            _write = _startUp.GetInMemoryWriteRepository();
        }

        [Fact]
        public void test1()
        {
            // Arrange
           

            // Ac

            // Assert
        }
    }
}
