using Intelli.Core.Game.Board.Events;
using Intelli.Core.Game.Board.Notifies;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game.Board
{
    public class BoardRejectedState : IState
    {
        public static readonly String NAME = "BoardRejectedState";
        private static Logger LOG = LogManager.GetCurrentClassLogger();
        private BoardStateMachine boardMachine;
        private Dictionary<String, IState> transitionableStates = new Dictionary<string, IState>();

        public BoardRejectedState(BoardStateMachine board)
        {
            this.boardMachine = board;
        }

        public string getStateName()
        {
            return "board_rejected_state";
        }

        public void run(IEvent e)
        {
            // Rejected because invalid moved position
            RejectedNotify notify = new RejectedNotify();
            this.boardMachine.fireStateChangedNotification(notify);
            LOG.Info(NAME);
            this.boardMachine.consumeEvent(new BoardReadyEvent());
        }

        public Dictionary<string, IState> getTransitionableState()
        {
            return this.transitionableStates;
        }
    }
}
