using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.States
{
    public class GameRedoState : IGameState
    {
        public static readonly String NAME = "GameRedoState";

        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        private GameStateMachine gameStateMachine;
        private Dictionary<String, IState> transitionableStates = new Dictionary<string, IState>();

        public GameRedoState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }
        public string getName()
        {
            return NAME;
        }

        public void run(IEvent e)
        {
            LOG.Info("Game redo");
        }

        public Dictionary<string, IState> getTransitionableState()
        {
            return this.transitionableStates;
        }

        public bool isSubmachineEvent(IEvent e)
        {
            // In this state, allow only these event
            //     1. PlayerUndoEvent
            //     2. BoardMoveEvent (This will bring the game back to state playing,
            //        if the move is valid)

            return false;
        }

        public void submachineConsumeEvent(IEvent e)
        {
            throw new NotImplementedException();
        }
    }
}
