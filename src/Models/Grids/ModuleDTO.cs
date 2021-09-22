using MartianRobots.Database.Entities;

namespace MartianRobots.Models.Grids
{
    // Also known as Coordinate 
    public class ModuleDTO
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Danger {get;set;}  // Representing the scent 

        public bool Busy { get; set; }  // Representing if a robot is there 

        public ModuleDTO(Module module)
        {
            X = module.X;
            Y = module.Y;
            Danger = (module.State == State.Danger);
        }

        public ModuleDTO()
        {
        }
    }
}