using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game.Board
{
    public class BoardReadyState : IState
    {
        public static readonly String NAME = "BoardReadyState";
        private static Logger LOG = LogManager.GetCurrentClassLogger();

        private BoardStateMachine board;
        private Dictionary<String, IState> transitionableStates = new Dictionary<string, IState>();

        public BoardReadyState(BoardStateMachine board)
        {
            this.board = board;
        }

        public string getStateName()
        {
            return NAME;
        }

        public void run(IEvent e)
        {
            LOG.Info("Ready");
        }


        public Dictionary<string, IState> getTransitionableState()
        {
            return this.transitionableStates;
        }
    }
}
