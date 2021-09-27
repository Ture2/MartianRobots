using MartianRobots.Database.Entities;
using System.Collections.Generic;

namespace MartianRobots.Models.Grids
{
    public class GridDTO
    {
        public readonly int maxN = 50;
        public readonly int maxM = 50;

        public int XAxisLength { get; set; }
        public int YAxisLength { get; set; }
        public Planet Planet { get; set; }

        public ModuleDTO [,] Grid { get; set; }

        public RobotDTO CurrentRobotExploring { get; set; }

        public List<RobotDTO> RobotList { get; set; }

        public GridDTO()
        {
        }

        
        public GridDTO(Grid grid)
        {
            XAxisLength = grid.XAxisLength;
            YAxisLength = grid.XAxisLength;
            Planet = grid.Planet;
        }

    }
}