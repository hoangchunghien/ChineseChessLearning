using Intelli.Core.Game.Board;
using Intelli.Core.Game.Player;
using Intelli.Core.Game.Player.Events;
using IntelliCore.Core.Game.Exceptions;
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
        public string getStateName()
        {
            return NAME;
        }

        public void run(IEvent e)
        {
            LOG.Info("Game initializing");

            LOG.Info("Init board state machine");
            this.gameStateMachine.setBoardMachine(new BoardStateMachine());
            this.gameStateMachine.getBoardMachine().addListener(this.gameStateMachine);

            LOG.Info("Init players state machine");
            PlayerStateMachine[] players = new PlayerStateMachine[2];
            players[0] = new PlayerStateMachine(this.gameStateMachine.getBoardMachine().getPieces(Board.Pieces.Color.BLACK));
            players[1] = new PlayerStateMachine(this.gameStateMachine.getBoardMachine().getPieces(Board.Pieces.Color.RED));
            players[0].addListener(this.gameStateMachine);
            players[1].addListener(this.gameStateMachine);
            this.gameStateMachine.setPlayers(players);
            //Uwhy don't consume initializedEvent? to get next state playing
        }

        public Dictionary<string, IState> getTransitionableState()
        {
            return this.transitionableStates;
        }

        /// <summary>
        /// In game initializing state, receive only 3 events are:
        ///    1. PlayerJoinEvent
        ///    2. PlayerRejectEvent
        ///    3. PlayerReadyEvent
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool isSubmachineEvent(IEvent e)
        {
            if (e.GetType().Equals(typeof(PlayerJoinEvent)))
            {
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
            try
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
                    // Transport event to player machine
                    this.gameStateMachine.getPlayers()[pid].consumeEvent(e);
                }
                else if (e.GetType().Equals(typeof(PlayerReadyEvent)))
                {
                    int pid = ((PlayerReadyEvent)e).getId();
                    // Transport event to player machine
                    this.gameStateMachine.getPlayers()[pid].consumeEvent(e);
                }
            }
            catch (IndexOutOfRangeException ioore)
            {
                LOG.Error("Invalid player id: " + ((PlayerJoinEvent)e).getId());
                throw new EventNotAcceptableException("Invalid player id");
            }
        }
    }
}
