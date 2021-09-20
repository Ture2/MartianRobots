using System.ComponentModel.DataAnnotations.Schema;

namespace MartianRobots.Models.DTOs
{
    public class RobotDTO
    {
        public int RobotID { get; set; }
        public string Path { get; set; }
        public bool Lost { get; set; }

        public int NumberOfMoves { get; set; }

        // Foreign Keys

        [ForeignKey("ModuleID")]
        public int LastPosition { get; set;}
        public ModuleDTO ModuleDTO {get; set;}
    }
}