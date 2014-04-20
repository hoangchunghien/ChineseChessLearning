using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.States
{
    public class GamePlayingState: IGameState
    {
        public static readonly String NAME = "GamePlayingState";

        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        private GameStateMachine gameStateMachine;
        private Dictionary<String, IState> transitionableStates = new Dictionary<string, IState>();

        public GamePlayingState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }
        public string getName()
        {
            return NAME;
        }

        public void run(IEvent e)
        {
            LOG.Info("Game playing");
        }

        public Dictionary<string, IState> getTransitionableState()
        {
            return this.transitionableStates;
        }

        public bool isSubmachineEvent(IEvent e)
        {
            // In playing state, only allow these event
            //     1. BoardMoveEvent
            //     2. PlayerUndoEvent
            //     3. PlayerResignEvent

            return false;
        }

        public void submachineConsumeEvent(IEvent e)
        {
            throw new NotImplementedException();
        }
    }
}
