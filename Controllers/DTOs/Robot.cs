using System;
using System.ComponentModel.DataAnnotations;
using MartianRobots.Models.Grids;

namespace MartianRobots.Models
{
    public class Robot
    {
        public int Id { get; set; }

        [Required]
        public Coordinate CurrentCoordinate { get; set; }

        [Required]
        public int NumberOfMoves { get; set; }

        [Required]
        public Orientation CurrentOrientation { get; set; }

        public Instruction LastInstruction { get; set; }

        public Coordinate LostCoordinates { get; set; }

    }
}
