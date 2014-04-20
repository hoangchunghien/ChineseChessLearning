﻿using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Player.States
{
    public class PlayerResignedState : IState
    {
        public static readonly String NAME = "PlayerResignedState";
        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        private PlayerStateMachine playerStateMachine;

        private Dictionary<String, IState> transitionableStates = new Dictionary<string, IState>();

        public PlayerResignedState(PlayerStateMachine playerStateMachine)
        {
            // TODO: Complete member initialization
            this.playerStateMachine = playerStateMachine;
        }

        public string getName()
        {
            return NAME;
        }

        public void run(IEvent e)
        {
            LOG.Info("Player resigned");
        }

        public Dictionary<string, IState> getTransitionableState()
        {
            return this.transitionableStates;
        }
    }
}
