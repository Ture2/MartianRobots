using MartianRobots.Database.Entities;
using System.Collections.Generic;

namespace MartianRobots.Models.Grids
{
    public abstract class GridDTO
    {
        public readonly int maxN = 50;
        public readonly int maxM = 50;

        public int XAxisLength { get; set; }
        public int YAxisLength { get; set; }
        public Planet Planet { get; set; }

        public ModuleDTO [,] grid = null;

        public List<Robot> RobotList { get; set; }

    /** TODO: adapt grid to bidimensional array
     */
    public GridDTO(Grid grid)
        {
            XAxisLength = grid.XAxisLength;
            YAxisLength = grid.XAxisLength;
            Planet = grid.Planet;
            // grid = grid.Modules.fo

    }
}
}