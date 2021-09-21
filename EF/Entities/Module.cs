using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MartianRobots.EF.Entities
{
    public enum State
    {
        NoDanger,
        Danger
    }

    public class Module : BaseEntity
    {

        public int X { get; set; }
        public int Y { get; set; }
        public State State { get; set; }
       
        // Robot 
        public Guid RobotId { get; set; }
        public Robot Robot { get; set; }

        // Grid
        public Guid GridId { get; set; }
        public Grid Grid { get; set; } 
    }
}