﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Player.States
{
    public class PlayerTurningState : IState
    {
        private PlayerStateMachine playerStateMachine;

        public PlayerTurningState(PlayerStateMachine playerStateMachine)
        {
            // TODO: Complete member initialization
            this.playerStateMachine = playerStateMachine;
        }
    }
}
