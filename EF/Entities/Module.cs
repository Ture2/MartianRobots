using System.ComponentModel.DataAnnotations.Schema;

namespace MartianRobots.EF.Entities
{
    public class Module : BaseEntity
    {

        public int X { get; set; }
        public int Y { get; set; }
        public string State { get; set; }
        
    }
}