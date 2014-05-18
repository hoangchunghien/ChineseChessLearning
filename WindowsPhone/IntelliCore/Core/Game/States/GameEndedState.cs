﻿using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.States
{
    public class GameEndedState : IGameState
    {
        public static readonly String NAME = "GameEndedState";

        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        private GameStateMachine gameStateMachine;
        private Dictionary<String, IState> transitionableStates = new Dictionary<string, IState>();

        public GameEndedState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public string getStateName()
        {
            return NAME;
        }

        public void run(IEvent e)
        {
            LOG.Info("Game ended");
        }

        public Dictionary<string, IState> getTransitionableState()
        {
            return this.transitionableStates;
        }

        public bool isSubmachineEvent(IEvent e)
        {
            throw new NotImplementedException();
        }

        public void submachineConsumeEvent(IEvent e)
        {
            throw new NotImplementedException();
        }
    }
}
