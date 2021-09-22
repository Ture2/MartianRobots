using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MartianRobots.Database.Entities
{
    public class Robot : BaseEntity
    {
        public string Path { get; set; } // Input 
        public bool Lost { get; set; }

        public int NumberOfMoves { get; set; }

        public Module LastPosition { get; set; }

    }
}