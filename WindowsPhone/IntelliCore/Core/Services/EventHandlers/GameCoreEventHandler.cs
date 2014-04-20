using Intelli.Core.Game;
using Intelli.Event.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Services.EventHandlers
{
    public class GameCoreEventHandler : GameCoreService// implement interface GameService, so need all of method of it
    {
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

    }
}
