using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Services.EventHandlers
{
    class GameCoreEventHandler : GameCoreService// implement interface GameService, so need all of method of it
    {
        public Event.Game.ValidMovesEvent requestValidMoves(Event.Game.RequestValidMovesEvent e)
        {
            throw new NotImplementedException();
        }

    }
}
