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

        private IState currentState;

        private List<IState> states;

        private PlayerStateMachine[] players;

        private BoardStateMachine boardMachine;

        public GameStateMachine()
        {
            _initialize();
            boardMachine = new BoardStateMachine();
            players = new PlayerStateMachine[2];
            players[0] = new PlayerStateMachine(boardMachine.getPieces(Board.Pieces.Color.BLACK));
            players[1] = new PlayerStateMachine(boardMachine.getPieces(Board.Pieces.Color.RED));
        }

        private void _initialize()
        {
            IState initializing = new GameInitializingState(this);
            IState playing = new GamePlayingState(this);
            IState played = new GamePlayedState(this);
            IState redoing = new GameRedoingState(this);
            IState redo = new GameRedoState(this);
            IState undoing = new GameUndoingState(this);
            IState undoed = new GameUndoedState(this);
            IState ended = new GameEndedState(this);

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
            throw new NotImplementedException();
        }

        public void fireStateChangedNotification(INotify notify)
        {
            throw new NotImplementedException();
        }

        public PlayerStateMachine[] getPlayers()
        {
            return this.players;
        }

        public BoardStateMachine getBoardMachine()
        {
            return this.boardMachine;
        }
    }
}
