using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game.Board
{
    public class BoardMovedState : IState
    {
        private BoardStateMachine board;

        public BoardMovedState(BoardStateMachine board)
        {
            this.board = board;
        }

        public string getName()
        {
            return "board_moved_state";
        }

        public void run()
        {
            throw new NotImplementedException();
        }


        public Dictionary<string, IState> getTransitionableState()
        {
            throw new NotImplementedException();
        }
    }
}
