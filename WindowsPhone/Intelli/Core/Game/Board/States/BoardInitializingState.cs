using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game.Board
{
    public class BoardInitializingState : IState
    {

        private Dictionary<String, IState> transitionableStates = new Dictionary<string, IState>();
        private BoardStateMachine board;

        public BoardInitializingState(BoardStateMachine board)
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
            return this.transitionableStates;
        }
    }
}
