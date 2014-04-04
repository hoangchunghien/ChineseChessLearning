using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.EventHandler.Implements
{
    class GameCoreEventHandler : GameService// implement interface GameService, so need all of method of it
    {
        public Event.Game.ValidMovesEvent requestValidMoves(Event.Game.RequestValidMovesEvent e)
        {
            throw new NotImplementedException();
        }

    }
}
