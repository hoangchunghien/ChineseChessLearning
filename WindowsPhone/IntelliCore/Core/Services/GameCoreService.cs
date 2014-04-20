using Intelli.Event.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Services
{
    public interface GameCoreService
    {
        Event.Game.ValidMovesEvent requestValidMoves(Event.Game.RequestValidMovesEvent e);

        Event.Game.GameCreatedEvent createGame(Event.Game.CreateGameEvent e);
    }
}
