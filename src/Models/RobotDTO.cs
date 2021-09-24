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

        public Orientation CurrentOrientation { get; set; }

        public ModuleDTO CurrentPosition { get; set; } = null;
        public ModuleDTO LostCoordinates { get; set; } = null;

        public bool Lost { get; set; } = false;
        public bool MissionEnded { get; set; } = false;

        public RobotDTO(Robot robot)
        {
            NumberOfMoves = robot.NumberOfMoves;
            Path = robot.Path;
            Lost = robot.Lost;
            LostCoordinates = new ModuleDTO()
            {
                X = robot.LastPosition.X,
                Y = robot.LastPosition.Y
            };
            MissionEnded = robot.MissionEnded;
        }

        public RobotDTO(){}

    }
}
