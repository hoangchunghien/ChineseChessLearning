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
    public class BoardStateMachine : IStateMachine
    {
        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        public static readonly String _EVENT_READY = "_board_ready";
        public static readonly String EVENT_MOVE = "board_move";
        public static readonly String _EVENT_MOVED = "_board_moved";
        public static readonly String _EVENT_REJECT_MOVE = "_board_reject_move";

        private IState currentState;

        private List<IState> states;

        private Board board;

        public BoardStateMachine()
        {
            _initialize();
        }

        private void _initialize()
        {
            IState initializingState = new BoardInitializingState(this);
            IState movingState = new BoardMovingState(this);
            IState movedState = new BoardMovedState(this);
            IState readyState = new BoardReadyState(this);
            IState rejectedState = new BoardRejectedState(this);

            this.states = new List<IState>();

            this.states.Add(initializingState);
            this.states.Add(movingState);
            this.states.Add(movedState);
            this.states.Add(readyState);
            this.states.Add(rejectedState);

            // From initializing state
            initializingState.getTransitionableState().Add(BoardInitializedEvent.NAME, readyState);

            // From ready state
            readyState.getTransitionableState().Add(BoardMoveEvent.NAME, movingState);

            // From moving state
            movingState.getTransitionableState().Add(BoardRejectEvent.NAME, rejectedState);
            movingState.getTransitionableState().Add(BoardMovedEvent.NAME, movedState);

            // From rejected state
            rejectedState.getTransitionableState().Add(BoardReadyEvent.NAME, readyState);

            // From moved state
            movedState.getTransitionableState().Add(BoardReadyEvent.NAME, readyState);

            this.currentState = initializingState;
            this.currentState.run(null);
        }

        public bool consumeEvent(IEvent e)
        {
            if (currentState.getTransitionableState().Keys.Contains(e.getName()))
            {
                currentState = currentState.getTransitionableState()[e.getName()];
                currentState.run(e);
            }
            else
            {
                LOG.Error("Unexcepted event occur: " + e.getName());
            }
            return true;
        }

        public Board getBoard()
        {
            return this.board;
        }

        public void setBoard(Board board)
        {
            this.board = board;
        }

        public IState getCurrentState()
        {
            return this.currentState;
        }


        public void fireStateChangedNotification(INotify notify)
        {
            if (notify.GetType().Equals(typeof(InitializingNotify)))
            {
                LOG.Info("State changed: initializing");
            }
            else if (notify.GetType().Equals(typeof(ReadyNotify)))
            {
                LOG.Info("State changed: ready");
            }
            else if (notify.GetType().Equals(typeof(MovingNotify)))
            {
                LOG.Info("State changed: moving");
            }
            else if (notify.GetType().Equals(typeof(BoardMovedNotify)))
            {
                LOG.Info("State changed: moved");
            }
            else if (notify.GetType().Equals(typeof(RejectedNotify)))
            {
                LOG.Info("State changed: rejected");
            }
        }

        public List<Piece> getPieces(Color color)
        {
            List<Piece> result = new List<Piece>();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (this.board.getPieces()[i, j] != null &&
                        this.board.getPieces()[i, j].getColor() == color)
                    {
                        result.Add(this.board.getPieces()[i, j]);
                    }
                }   
            }

            return result;
        }
    }
}
