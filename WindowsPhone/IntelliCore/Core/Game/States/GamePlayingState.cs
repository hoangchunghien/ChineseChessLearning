using Intelli.Core.Game.Board;
using Intelli.Core.Game.Board.Events;
using Intelli.Core.Game.Board.Pieces;
using Intelli.Core.Game.Player.Events;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.States
{
    public class GamePlayingState: IGameState
    {
        public static readonly String NAME = "GamePlayingState";

        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        private GameStateMachine gameStateMachine;
        private Dictionary<String, IState> transitionableStates = new Dictionary<string, IState>();

        public GamePlayingState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }
        public string getStateName()
        {
            return NAME;
        }

        public void run(IEvent e)
        {
            LOG.Info("Game playing");
        }

        public Dictionary<string, IState> getTransitionableState()
        {
            return this.transitionableStates;
        }

        /// <summary>
        ///  In playing state, only allow these events:
        ///    1. BoardMoveEvent
        ///     2. PlayerUndoEvent
        ///     3. PlayerResignEvent
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool isSubmachineEvent(IEvent e)
        {
            

            if (e.GetType().Equals(typeof(BoardMoveEvent)))
            {
                return true;
            }
            else if (e.GetType().Equals(typeof(PlayerUndoEvent)))
            {
                return true;
            }
            else if (e.GetType().Equals(typeof(PlayerResignEvent)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void submachineConsumeEvent(IEvent e)
        {
            LOG.Info("Consuming event '" + e.getEventName() + "'");
            if (e.GetType().Equals(typeof(BoardMoveEvent)))
            {
                LOG.Info("Processing move event");
                BoardMoveEvent _e = ((BoardMoveEvent)e);
                int pid = _e.getPid();
                Position currentPosition = _e.getCurrentPosition();
                Position nextPosition = _e.getNextPosition();
                Piece selectionPiece = this.gameStateMachine.getBoardMachine().getBoard().
                    getPieces()[currentPosition.getRow(), currentPosition.getCol()];

                LOG.Info("Selection piece: " + selectionPiece.getColor());
                if (this.gameStateMachine.getPlayers()[pid].getListPieces().Contains(selectionPiece))
                {

                    this.gameStateMachine.getBoardMachine().consumeEvent(e);

                }
            }

        }
    }
}
