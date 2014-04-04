using Intelli.Event.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.EventHandler
{
    interface GameService
    {
        Event.Game.ValidMovesEvent requestValidMoves(Event.Game.RequestValidMovesEvent e);
    }
}
