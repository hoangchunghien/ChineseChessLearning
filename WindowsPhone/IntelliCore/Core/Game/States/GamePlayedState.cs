using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.States
{
    public class GamePlayedState : IGameState
    {
        public static readonly String NAME = "GamePlayedState";

        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        private GameStateMachine gameStateMachine;
        private Dictionary<String, IState> transitionableStates = new Dictionary<string, IState>();

        public GamePlayedState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }
        public string getStateName()
        {
            return NAME;
        }

        public void run(IEvent e)
        {
            LOG.Info("Game played");
        }

        public Dictionary<string, IState> getTransitionableState()
        {
            return this.transitionableStates;
        }

        /// <summary>
        /// In played state, allow only these event
        ///     1. PlayerUndoEvent
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool isSubmachineEvent(IEvent e)
        {

            return false;
        }

        public void submachineConsumeEvent(IEvent e)
        {
            throw new NotImplementedException();
        }
    }
}
