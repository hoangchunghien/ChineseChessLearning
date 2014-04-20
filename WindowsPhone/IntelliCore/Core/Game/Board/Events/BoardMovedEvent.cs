using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Board.Events
{
    public class BoardMovedEvent : IEvent
    {
        public static readonly String NAME = "BoardMovedEvent";

        public string getName()
        {
            return NAME;
        }
    }
}
