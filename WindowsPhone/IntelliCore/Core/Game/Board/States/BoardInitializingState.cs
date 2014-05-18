using Intelli.Core.Game.Board.Events;
using Intelli.Core.Game.Board.Notifies;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game.Board
{
    public class BoardInitializingState : IState
    {
        public static readonly String NAME = "BoardInitializingState";
        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        private Dictionary<String, IState> transitionableStates = new Dictionary<string, IState>();
        private BoardStateMachine boardMachine;

        public BoardInitializingState(BoardStateMachine boardMachine)
        {
            this.boardMachine = boardMachine;
        }

        public string getStateName()
        {
            return NAME;
        }

        public void run(IEvent e)
        {
            this.boardMachine.fireStateChangedNotification(new InitializingNotify());
            LOG.Info("Initializing");
            Board board = new Board();
            this.boardMachine.setBoard(board);
            boardMachine.consumeEvent(new BoardInitializedEvent());
        }


        public Dictionary<string, IState> getTransitionableState()
        {
            return this.transitionableStates;
        }
    }
}
