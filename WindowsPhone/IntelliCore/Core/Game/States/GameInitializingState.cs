using Intelli.Core.Game.Board;
using Intelli.Core.Game.Player;
using Intelli.Core.Game.Player.Events;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.States
{
    public class GameInitializingState : IGameState
    {
        public static readonly String NAME = "GameInitializingState";

        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        private GameStateMachine gameStateMachine;
        private Dictionary<String, IState> transitionableStates = new Dictionary<string, IState>();

        public GameInitializingState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }
        public string getName()
        {
            return NAME;
        }

        public void run(IEvent e)
        {
            LOG.Info("Game initializing");

            LOG.Info("Init board state machine");
            this.gameStateMachine.setBoardMachine(new BoardStateMachine());

            LOG.Info("Init players state machine");
            PlayerStateMachine[] players = new PlayerStateMachine[2];
            players[0] = new PlayerStateMachine(this.gameStateMachine.getBoardMachine().getPieces(Board.Pieces.Color.BLACK));
            players[1] = new PlayerStateMachine(this.gameStateMachine.getBoardMachine().getPieces(Board.Pieces.Color.RED));
            players[0].addListener(this.gameStateMachine);
            players[1].addListener(this.gameStateMachine);
            this.gameStateMachine.setPlayers(players);
        }

        public Dictionary<string, IState> getTransitionableState()
        {
            return this.transitionableStates;
        }

        public bool isSubmachineEvent(IEvent e)
        {
            // In game initializing state, receive only 3 event is
            //    1. PlayerJoinEvent
            //    2. PlayerRejectEvent
            //    3. PlayerReadyEvent
            if (e.GetType().Equals(typeof(PlayerJoinEvent))) {
                return true;
            }
            else if (e.GetType().Equals(typeof(PlayerRejectEvent)))
            {
                return true;
            }
            else if (e.GetType().Equals(typeof(PlayerReadyEvent)))
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
            if (e.GetType().Equals(typeof(PlayerJoinEvent)))
            {
                int pid = ((PlayerJoinEvent)e).getId();
                // Transport event to player machine
                this.gameStateMachine.getPlayers()[pid].consumeEvent(e);
            }
            else if (e.GetType().Equals(typeof(PlayerRejectEvent)))
            {
                int pid = ((PlayerRejectEvent)e).getId();
                this.gameStateMachine.getPlayers()[pid].consumeEvent(e);
            }
            else if (e.GetType().Equals(typeof(PlayerReadyEvent)))
            {
                int pid = ((PlayerReadyEvent)e).getId();
                this.gameStateMachine.getPlayers()[pid].consumeEvent(e);
            }
        }
    }
}
