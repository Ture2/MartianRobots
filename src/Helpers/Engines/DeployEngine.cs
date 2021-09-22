using MartianRobots.Helpers.Commands;
using MartianRobots.Models;
using MartianRobots.Models.Grids;
using MartianRobots.Shared.Interfaces.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Helpers.Engines
{
    public class DeployEngine
    {
        private IDeployCommand _command;
        private readonly DeployDTO _item;
        

        public DeployEngine(DeployDTO item)
        {
            _item = item;
        }
        public void SetCommand(IDeployCommand command)
        {
            _command = command;
        }

        public GridDTO Deploy()
        {
            SetCommand(new DeployCommand(_item));
            _command.Execute();
            return _command.Result;
        }


    }
}
