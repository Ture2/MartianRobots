using System;
using System.ComponentModel.DataAnnotations;
using MartianRobots.Database.Entities;
using MartianRobots.Models.Grids;

namespace MartianRobots.Models
{
    public class RobotDTO
    {

        public int NumberOfMoves { get; set; }

        public string Path { get; set; }

        public Module LostCoordinates { get; set; }

        public bool Lost { get; set; }

        public RobotDTO(Robot robot)
        {
            NumberOfMoves = robot.NumberOfMoves;
            Path = robot.Path;
            Lost = robot.Lost;
            LostCoordinates = robot.LastPosition;
        }

    }
}
