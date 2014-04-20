using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game.Board
{
    public class BoardRejectedState : IState
    {

        private BoardStateMachine board;
        private Dictionary<String, IState> transitionableStates = new Dictionary<string, IState>();

        public BoardRejectedState(BoardStateMachine board)
        {
            this.board = board;
        }

        public string getName()
        {
            return "board_rejected_state";
        }

        public void run(IEvent e)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, IState> getTransitionableState()
        {
            return this.transitionableStates;
        }
    }
}
