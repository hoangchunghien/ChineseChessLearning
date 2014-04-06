using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game.Board
{
    public class BoardStateMachine : IStateMachine
    {
        public static readonly String _EVENT_INITIALIZED = "_board_initialized";
        public static readonly String _EVENT_READY = "_board_ready";
        public static readonly String EVENT_MOVE = "board_move";
        public static readonly String _EVENT_MOVED = "_board_moved";
        public static readonly String _EVENT_REJECT_MOVE = "_board_reject_move";

        private IState currentState;

        private List<IState> states;

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

            this.states.Add(initializingState);
            this.states.Add(movingState);
            this.states.Add(movedState);
            this.states.Add(readyState);
            this.states.Add(rejectedState);

            // From initializing state
            initializingState.getTransitionableState().Add(_EVENT_INITIALIZED, readyState);

            // From ready state
            readyState.getTransitionableState().Add(EVENT_MOVE, movingState);

            // From moving state
            movingState.getTransitionableState().Add(_EVENT_REJECT_MOVE, rejectedState);
            movingState.getTransitionableState().Add(_EVENT_MOVED, movedState);

            // From rejected state
            rejectedState.getTransitionableState().Add(_EVENT_READY, readyState);

            // From moved state
            movedState.getTransitionableState().Add(_EVENT_READY, readyState);

            this.currentState = initializingState;
            this.currentState.run();
        }

        public void consumeEvent(String eventName)
        {
            currentState = currentState.getTransitionableState()[eventName];
            currentState.run();
        }
    }
}
