using Intelli.Core.Game;
using Intelli.Event.Game;
using IntelliCore.Event.Game;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Services.EventHandlers
{
    public class GameCoreEventHandler : GameCoreService// implement interface GameService, so need all of method of it
    {
        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        private GameStateMachine gameMachine;

        public Event.Game.ValidMovesEvent requestValidMoves(Event.Game.RequestValidMovesEvent e)
        {
            throw new NotImplementedException();
        }

        public Event.Game.GameCreatedEvent createGame(Event.Game.CreateGameEvent e)
        {
            gameMachine = new GameStateMachine();
            GameDetail detail = GameDetail.fromGameStateMachine(gameMachine);
            return new GameCreatedEvent(detail);
        }

        public PlayerJoinedEvent requestPlayerJoinEvent(RequestPlayerJoinEvent e)
        {
            LOG.Info("player id=" + e.getId());
            Intelli.Core.Game.Player.Events.PlayerJoinEvent joinEvent = new Game.Player.Events.PlayerJoinEvent();
            joinEvent.setId(e.getId());
            bool accepted = gameMachine.consumeEvent(joinEvent);

            if (accepted)
            {
                LOG.Info("Player join accepted");
                return new PlayerJoinedEvent();
            }
            else
            {
                LOG.Info("Player join rejected");
                return PlayerJoinedEvent.notAcceptable();
            }
        }
    }
}
