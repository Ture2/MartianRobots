using System;
using System.ComponentModel.DataAnnotations;
using MartianRobots.Database.Entities;
using MartianRobots.Models.Grids;

namespace MartianRobots.Models
{
    public class RobotDTO
    {

        public int NumberOfMoves { get; set; } = 0;

        public string Path { get; set; } = "";

        public Module CurrentPosition { get; set; } = new Module();
        public Module LostCoordinates { get; set; }

        public bool Lost { get; set; } = false;

        public RobotDTO(Robot robot)
        {
            NumberOfMoves = robot.NumberOfMoves;
            Path = robot.Path;
            Lost = robot.Lost;
            LostCoordinates = robot.LastPosition;
        }

        public RobotDTO(){}

    }
}
