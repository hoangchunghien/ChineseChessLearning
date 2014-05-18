using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Board.Events
{
    public class BoardInitializedEvent : IEvent
    {
        public static readonly String NAME = "InitializedEvent";
        public string getEventName()
        {
            return NAME;
        }
    }
}
