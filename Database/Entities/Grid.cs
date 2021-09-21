using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Database.Entities
{
    public enum Planet
    {
        Mars // Created for future developments
    }
    public class Grid : BaseEntity
    {
        public int XAxisLength { get; set; }
        public int YAxisLength { get; set; }
        public Planet Planet { get; set; }

        public ICollection<Module> Modules{get;set;}

    }
}
