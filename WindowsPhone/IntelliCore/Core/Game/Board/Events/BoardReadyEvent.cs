using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Board.Events
{
    public class BoardReadyEvent : IEvent
    {
        public static readonly String NAME = "BoardReadyEvent";
        public string getEventName()
        {
            return NAME;
        }
    }
}
