using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.States
{
    public class GameEndedState : IState
    {
        public static readonly String NAME = "GameEndedState";

        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        private GameStateMachine gameStateMachine;
        private Dictionary<String, IState> transitionableStates = new Dictionary<string, IState>();

        public GameEndedState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public string getName()
        {
            throw new NotImplementedException();
        }

        public void run(IEvent e)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, IState> getTransitionableState()
        {
            throw new NotImplementedException();
        }
    }
}
