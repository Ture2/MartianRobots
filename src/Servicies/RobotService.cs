using MartianRobots.Database.Entities;
using MartianRobots.Database.Repositores.Base;
using MartianRobots.Helpers.Commands;
using MartianRobots.Helpers.Engines;
using MartianRobots.Models;
using MartianRobots.Models.Grids;
using MartianRobots.Shared.Inferfaces.Servicies;
using MartianRobots.Shared.Interfaces.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Servicies
{
    public class RobotService: IRobotService
    {
        private readonly IRepository<Grid> _gridRepository;


        public RobotService(
            IRepository<Grid> gridRepository
            )
        {
            _gridRepository = gridRepository;
            
        }
        

        public async Task<List<string>> DeployRobot(DeployDTO deployDTO)
        {
            // Grid creation
            DeployEngine engine = new DeployEngine(deployDTO);
            GridDTO gridDTO = engine.Deploy();
            List<string> results = new List<string>();

            // Robot deploys

            var task = Task<List<string>>.Factory.StartNew(() => {
                foreach (RobotInfo r in deployDTO.RobotInfoList)
                {
                    RobotEngine robotEngine = new RobotEngine(r, gridDTO);
                    robotEngine.Execute();
                    results.Add(gridDTO.CurrentRobotExploring.ToString());
                }
                return results;
            });

            await task;
            await AddGrid(gridDTO);

            return results;
        }

        public Task<List<RobotDTO>> GetAllPlanetRobots(Guid planetId)
        {
            throw new NotImplementedException();
        }

        public Task<RobotDTO> GetRobot(Guid id)
        {
            throw new NotImplementedException();
        }

        private async Task AddGrid(GridDTO gridDTO)
        {

            ICollection<Robot> robots = gridDTO.RobotList.Select(r => new Robot()
            {
                Path = r.Path,
                Lost = r.Lost,
                MissionEnded = r.MissionEnded,
                NumberOfMoves = r.NumberOfMoves,
                LastPosition = r.Lost ?
                     new Module() {
                         X = r.LostCoordinates.X,
                         Y = r.LostCoordinates.X,
                         State = State.Danger,
                     } :
                     new Module()
                     {
                         X = r.CurrentPosition.X,
                         Y = r.CurrentPosition.X,
                         State = State.NoDanger,
                     }
            }).ToList();

            ICollection<Module> modList = new List<Module>();
            for (int j = 0; j < gridDTO.YAxisLength; j++)
            {
                for (int i = 0; i < gridDTO.XAxisLength; i++)
                {
                    var module = robots.Where(r => r.LastPosition.X == i || r.LastPosition.Y == j).FirstOrDefault()?.LastPosition;
                    modList.Add((module != null) ? module : new Module()
                    {
                        X = i,
                        Y = j,
                        State = (gridDTO.Grid[j, i].Danger) ? State.Danger : State.NoDanger,
                    });
                }
            }

            var result = await _gridRepository.Add(new Grid()
            {
                XAxisLength = gridDTO.XAxisLength,
                YAxisLength = gridDTO.YAxisLength,
                Planet = gridDTO.Planet,
                Robots = robots,
                Modules = modList
            });

        }
    }
}
