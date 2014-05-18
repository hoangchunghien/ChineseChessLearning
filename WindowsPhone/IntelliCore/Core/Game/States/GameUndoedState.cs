using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.States
{
    public class GameUndoedState : IGameState
    {
        public static readonly String NAME = "GameUndoedState";

        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        private GameStateMachine gameStateMachine;
        private Dictionary<String, IState> transitionableStates = new Dictionary<string, IState>();

        public GameUndoedState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }
        public string getStateName()
        {
            return NAME;
        }

        public void run(IEvent e)
        {
            LOG.Info("Game undoed");
        }

        public Dictionary<string, IState> getTransitionableState()
        {
            return this.transitionableStates;
        }

        public bool isSubmachineEvent(IEvent e)
        {
            // In the undoed state, allow only these event
            //     1. BoardMoveEvent  (this will bring the game machine turn back to playing state)
            //     2. PlayerRedoEvent

            return false;
        }

        public void submachineConsumeEvent(IEvent e)
        {
            throw new NotImplementedException();
        }
    }
}
