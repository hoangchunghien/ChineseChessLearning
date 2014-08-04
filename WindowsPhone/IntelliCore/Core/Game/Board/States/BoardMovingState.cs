using Intelli.Core.Game.Board.Events;
using Intelli.Core.Game.Board.Notifies;
using Intelli.Core.Game.Board.Pieces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game.Board
{
    public class BoardMovingState : IState
    {
        public static readonly String NAME = "Board_MovingState";
        private static Logger LOG = LogManager.GetCurrentClassLogger();

        private Dictionary<String, IState> transitionableStates = new Dictionary<string, IState>();
        private BoardStateMachine boardMachine;

        public BoardMovingState(BoardStateMachine boardMachine)
        {
            this.boardMachine = boardMachine;
        }

        public string getStateName()
        {
            return NAME;
        }

        public void run(IEvent e)
        {
            MovingNotify notify = new MovingNotify();
            this.boardMachine.fireStateChangedNotification(notify);

            LOG.Info(NAME);
            if (e.GetType().Equals(typeof(BoardMoveEvent)))
            {
                bool accepted = false;
                Position cPos = ((BoardMoveEvent)e).getCurrentPosition();
                Position nPos = ((BoardMoveEvent)e).getNextPosition();
                Piece p = this.boardMachine.getBoard().getPieces()[cPos.getRow(), cPos.getCol()];
                if (p != null && p.getValidNextPositions().Contains(nPos))
                {
                    accepted = true;
                }

                if (accepted)
                {
                    LOG.Info("Old: \n" + this.boardMachine.getBoard().ToString());

                    // Set current Position is null after moved
                    this.boardMachine.getBoard().getPieces()[cPos.getRow(), cPos.getCol()] = null;

                    // Move piece selected to new position
                    this.boardMachine.getBoard().getPieces()[nPos.getRow(), nPos.getCol()] = p;
                    LOG.Info("New: \n" + this.boardMachine.getBoard().ToString());

                    // Also process "movedEvent" (don't reject) to change to state "moved"
                    this.boardMachine.consumeEvent(new BoardMovedEvent());
                }
                else // Different casual, consume event "RejectEvent" to comeback to ready event
                {
                    this.boardMachine.consumeEvent(new BoardRejectEvent());
                    LOG.Info("Rejected move from: " + cPos.ToString() + " to: " + nPos.ToString());
                }
            }
            else
            {
                LOG.Error("Receive unexpected event, name: " + e.getEventName());
            }
        }


        public Dictionary<string, IState> getTransitionableState()
        {
            return this.transitionableStates;
        }
    }
}
