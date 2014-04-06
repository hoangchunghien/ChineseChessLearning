using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game.Board
{
    public class BoardReadyState : IState
    {

        private BoardStateMachine board;

        public BoardReadyState(BoardStateMachine board)
        {
            this.board = board;
        }

        public string getName()
        {
            throw new NotImplementedException();
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
