using Intelli.Core.Game.Board.Pieces;
using Intelli.Core.Game.Player.Events;
using Intelli.Core.Game.Player.States;
using IntelliCore.Core.Game.Exceptions;
using IntelliCore.Core.Game.Player.Notifies;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Intelli.Core.Game.Player
{
    public class PlayerStateMachine : IStateMachine
    {
        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        private IState currentState;

        private List<IState> states;

        private List<IStateMachine> listeners = new List<IStateMachine>();

        List<Piece> listPieces;

        public PlayerStateMachine(List<Piece> _listPieces)
        {
            this.listPieces = _listPieces;
            _initialize();
        }

        private void _initialize()
        {
            IState initializing = new PlayerInitializingState(this);
            IState joining = new PlayerJoiningState(this);
            IState rejected = new PlayerRejectedState(this);
            IState joined = new PlayerJoinedState(this);
            IState ready = new PlayerReadyState(this);
            IState playing = new PlayerPlayingState(this);
            IState waiting = new PlayerWaitingState(this);
            IState turning = new PlayerTurningState(this);
            IState chessed = new PlayerChessedState(this);
            IState lost = new PlayerLostState(this);
            IState safe = new PlayerSafeState(this);
            IState resigned = new PlayerResignedState(this);
            IState win = new PlayerWinState(this);
            IState undo = new PlayerUndoState(this);
            IState undoing = new PlayerUndoingState(this);
            IState undoRejected = new PlayerUndoRejectedState(this);
            IState undoAccepting = new PlayerUndoAcceptingState(this);
            IState undoAccepted = new PlayerUndoAcceptedState(this);

            this.states = new List<IState>();

            this.states.Add(initializing);
            this.states.Add(joining);
            this.states.Add(rejected);
            this.states.Add(joined);
            this.states.Add(ready);
            this.states.Add(playing);
            this.states.Add(waiting);
            this.states.Add(turning);
            this.states.Add(chessed);
            this.states.Add(lost);
            this.states.Add(safe);
            this.states.Add(resigned);
            this.states.Add(win);
            this.states.Add(undoing);
            this.states.Add(undoRejected);
            this.states.Add(undoAccepting);
            this.states.Add(undoAccepted);

            // From initializing
            initializing.getTransitionableState().Add(PlayerInitializedEvent.NAME, joining);

            // From joining
            joining.getTransitionableState().Add(PlayerJoinEvent.NAME, joined);
            joining.getTransitionableState().Add(PlayerRejectEvent.NAME, rejected);

            // From Joined
            joined.getTransitionableState().Add(PlayerReadyEvent.NAME, ready);

            // From ready
            ready.getTransitionableState().Add(PlayerPlayEvent.NAME, playing);

            // From playing
            playing.getTransitionableState().Add(PlayerWaitEvent.NAME, waiting);
            playing.getTransitionableState().Add(PlayerChessEvent.NAME, chessed);
            playing.getTransitionableState().Add(PlayerWinEvent.NAME, win);
            playing.getTransitionableState().Add(PlayerUndoAcceptEvent.NAME, undoAccepting);

            // From waiting
            waiting.getTransitionableState().Add(PlayerTurnEvent.NAME, turning);
            waiting.getTransitionableState().Add(PlayerResignEvent.NAME, resigned);
            waiting.getTransitionableState().Add(PlayerUndoEvent.NAME, undoing);

            // From chessed
            chessed.getTransitionableState().Add(PlayerSaveEvent.NAME, safe);
            chessed.getTransitionableState().Add(PlayerNoValidMoveEvent.NAME, lost);

            // From win


            // From undoAccepting
            undoAccepting.getTransitionableState().Add(PlayerUndoAcceptEvent.NAME, undoAccepted);
            undoAccepting.getTransitionableState().Add(PlayerUndoRejectEvent.NAME, undoRejected);

            // From turning
            turning.getTransitionableState().Add(PlayerPlayEvent.NAME, playing);

            // From resigned
            resigned.getTransitionableState().Add(PlayerLoseEvent.NAME, lost);

            // From undoing
            undoing.getTransitionableState().Add(PlayerUndoAcceptedEvent.NAME, undo);
            undoing.getTransitionableState().Add(PlayerUndoRejectEvent.NAME, undoRejected);

            // From safe
            safe.getTransitionableState().Add(PlayerPlayEvent.NAME, playing);

            // From lost 

            // From undoAccepted 
            undoAccepted.getTransitionableState().Add(PlayerPlayEvent.NAME, playing);

            // From undoRejected
            undoRejected.getTransitionableState().Add(PlayerPlayEvent.NAME, playing);

            // From undo
            undo.getTransitionableState().Add(PlayerPlayEvent.NAME, playing);

            this.currentState = initializing;
            this.currentState.run(null);

        }
        public bool consumeEvent(IEvent e)
        {
            if (currentState.getTransitionableState().Keys.Contains(e.getEventName()))
            {
                LOG.Info("Consume event '" + e.getEventName() + "'");
                // Get next state
                currentState = currentState.getTransitionableState()[e.getEventName()];
                currentState.run(e);
                return true;
            }
            else
            {
                LOG.Error("Unexpected event occur: " + e.getEventName());
                throw new EventNotAcceptableException(e.getEventName()
                    + " not acceptable in '" + currentState.getStateName() + "'");
            }
        }

        public void fireStateChangedNotification(INotify notify)
        {
            LOG.Info("state changed");
            this._notifyListeners(notify);
        }

        public IState getCurrentState()
        {
            return this.currentState;
        }

        public List<Piece> getListPieces()
        {
            return this.listPieces; 
        }
        private void _notifyListeners(INotify notify)
        {
            foreach (IStateMachine machine in listeners)
            {
                machine.fireStateChangedNotification(notify);
            }
        }

        public void addListener(IStateMachine listener)
        {
            this.listeners.Add(listener);
        }

        public void removeListener(IStateMachine listener)
        {
            this.listeners.Remove(listener);
        }
    }
}
