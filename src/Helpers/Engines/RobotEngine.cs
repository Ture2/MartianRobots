﻿using MartianRobots.Helpers.Commands;
using MartianRobots.Models;
using MartianRobots.Models.Grids;
using MartianRobots.Shared.Interfaces.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Helpers.Engines
{
    public class RobotEngine
    {
        private List<IRobotCommand> _moveCommands = new List<IRobotCommand>();
        private IRobotCommand _setRobotCommand;
        private readonly GridDTO _gridDTO;
        private RobotInfo _item { get; set; }

        public RobotEngine(RobotInfo item, GridDTO gridDTO) {
            _item = item;
            _gridDTO = gridDTO;
        }

        
        public void SetPositionCommand(IRobotCommand command)
        {
            _setRobotCommand = command;
        }

        public void MoveRobotCommand(IRobotCommand command)
        {
            _moveCommands.Add(command);
        }

        public void Execute()
        {
            // Apply set on mars
            SetPositionCommand(new SetRobotCommand(_item, _gridDTO));
            _setRobotCommand.Execute();
            _setRobotCommand.GetResult();

            // Then run list of moves

            foreach (char i in _item.Path)
            {
                //Pasar _grid como out
                MoveRobotCommand(new MoveRobotCommand(InstructionParse(i), _gridDTO));
            }

            foreach(MoveRobotCommand command in _moveCommands)
            {
                if (command.GetResult().CurrentRobotExploring.Lost == true |
                    command.GetResult().CurrentRobotExploring.MissionEnded)
                    break;
                command.Execute();
            }

        } 
       

        private Instruction InstructionParse(char i)
        {
            switch (i)
            {
                case 'F': return Instruction.F;
                case 'L': return Instruction.L;
                case 'R': return Instruction.R;
                default: return Instruction.F;
            }
        }
    }
}
