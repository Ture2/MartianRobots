using MartianRobots.Database.Contexts;
using MartianRobots.Database.Entities;
using MartianRobots.Database.Repositores.Base;
using MartianRobots.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.UnitTest
{
    class StartUp
    {
        private readonly MartianRobotsContext _martianRobotsContext;

        public StartUp()
        {
            var builder = new DbContextOptionsBuilder<MartianRobotsContext>();
            builder.UseInMemoryDatabase(databaseName: "LibraryDbInMemory");

            var dbContextOptions = builder.Options;
            _martianRobotsContext = new MartianRobotsContext(dbContextOptions);
            // Delete existing db before creating a new one
            _martianRobotsContext.Database.EnsureDeleted();
            _martianRobotsContext.Database.EnsureCreated();
        }

        public MartianRobotsRepository<Robot> GetInMemoryReadRepository()
        {
            return new MartianRobotsRepository<Robot>(_martianRobotsContext);
        }

        public MartianRobotsRepository<Robot> GetInMemoryWriteRepository()
        {
            return new MartianRobotsRepository<Robot>(_martianRobotsContext);
        }

    }
}
