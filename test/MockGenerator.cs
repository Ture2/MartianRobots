using MartianRobots.Models.Grids;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.UnitTest
{
    public static class MockGenerator
    {
        public static ModuleDTO[,] GetEmptyGrid(int x, int y)
        {
            x++;
            y++;
            ModuleDTO[,] gridModules = new ModuleDTO[y, x];

            for (int j = 0; j < y; j++)
            {
                for (int i = 0; i < x; i++)
                {
                    gridModules[j, i] = new ModuleDTO()
                    {
                        X = i,
                        Y = j,
                        Danger = false,
                        Busy = false
                    };
                }
            }
            return gridModules;
        }
    }
}
