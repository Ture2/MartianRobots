namespace MartianRobots.Models.Grids
{
    public abstract class Grid
    {
        public readonly int maxN = 50;
        public readonly int maxM = 50;

        public Module [,] grid = null;

    }
}