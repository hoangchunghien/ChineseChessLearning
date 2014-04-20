using Intelli.Core.Game.Board;
using Intelli.Core.Game.Events;
using Intelli.Core.Game.Player;
using Intelli.Core.Game.States;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game
{
    public class GameStateMachine : IStateMachine
    {
        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        private IGameState currentState;

        private List<IGameState> states;

        private PlayerStateMachine[] players;

        private BoardStateMachine boardMachine;

        public GameStateMachine()
        {
            _initialize();
        }

        private void _initialize()
        {
            IGameState initializing = new GameInitializingState(this);
            IGameState playing = new GamePlayingState(this);
            IGameState played = new GamePlayedState(this);
            IGameState redoing = new GameRedoingState(this);
            IGameState redo = new GameRedoState(this);
            IGameState undoing = new GameUndoingState(this);
            IGameState undoed = new GameUndoedState(this);
            IGameState ended = new GameEndedState(this);

            // From intitializing
            initializing.getTransitionableState().Add(GameInitializedEvent.NAME, playing);

            // From playing
            playing.getTransitionableState().Add(GamePlayedEvent.NAME, played);
            playing.getTransitionableState().Add(GameUndoEvent.NAME, undoing);

            // From played
            played.getTransitionableState().Add(GameEndEvent.NAME, ended);
            played.getTransitionableState().Add(GameUndoEvent.NAME, undoing);

            // From undoing
            undoing.getTransitionableState().Add(GameUndoedEvent.NAME, undoed);

            // From undoed
            undoed.getTransitionableState().Add(GamePlayEvent.NAME, playing);
            undoed.getTransitionableState().Add(GameRedoEvent.NAME, redoing);

            // From redoing
            redoing.getTransitionableState().Add(GameRedoedEvent.NAME, redo);

            // From redo
            redo.getTransitionableState().Add(GamePlayEvent.NAME, playing);
            redo.getTransitionableState().Add(GameUndoEvent.NAME, undoed);

            this.currentState = initializing;
            this.currentState.run(null);
        }

        public void consumeEvent(IEvent e)
        {
            if (currentState.getTransitionableState().Keys.Contains(e.getName()))
            {

            }
            else if (currentState.isSubmachineEvent(e))
            {
                // Transport event to submachine like PlayerMachine or BoardMachine
                currentState.submachineConsumeEvent(e);
            }
            else
            {
                // Reject unvalid event
            }
        }

        public void fireStateChangedNotification(INotify notify)
        {
            LOG.Info("Not supported yet");
        }

        public PlayerStateMachine[] getPlayers()
        {
            return this.players;
        }

        public void setPlayers(PlayerStateMachine[] players)
        {
            this.players = players;
        }

        public BoardStateMachine getBoardMachine()
        {
            return this.boardMachine;
        }

        public void setBoardMachine(BoardStateMachine boardMachine)
        {
            this.boardMachine = boardMachine;
        }
    }
}
