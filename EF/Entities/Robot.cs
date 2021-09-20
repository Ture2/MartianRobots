using System.ComponentModel.DataAnnotations.Schema;

namespace MartianRobots.EF.Entities
{
    public class Robot : BaseEntity
    {
        public string Path { get; set; }
        public bool Lost { get; set; }

        public int NumberOfMoves { get; set; }

        // Foreign Keys
        [ForeignKey("ModuleId")]
        public Module LastPosition { get; set; }
    }
}