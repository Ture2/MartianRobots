using System.ComponentModel.DataAnnotations.Schema;

namespace MartianRobots.Models.DTOs
{
    public class ModuleDTO
    {
        public int ModuleID { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public string State { get; set; }

        // Foreign Keys

    }
}