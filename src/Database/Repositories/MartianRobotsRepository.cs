using MartianRobots.Database.Contexts;
using MartianRobots.Database.Entities;
using MartianRobots.Database.Repositores.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Database.Repositories
{
    public class MartianRobotsRepository<T> : BaseRepository<T, MartianRobotsContext> 
        where T: BaseEntity
    {
        public MartianRobotsRepository(MartianRobotsContext context) : base(context) { }
    }
}
