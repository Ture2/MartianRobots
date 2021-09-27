using MartianRobots.Database.Contexts;
using MartianRobots.Database.Entities;
using MartianRobots.Database.Repositores.Base;
using Moq;
using System;
using Xunit;

namespace MartianRobots.UnitTest.Database
{
    public class Repositories
    {
        private StartUp _startUp = new StartUp();
        private IRepository<Robot> _read;
        private BaseRepository<Robot> _write;


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
