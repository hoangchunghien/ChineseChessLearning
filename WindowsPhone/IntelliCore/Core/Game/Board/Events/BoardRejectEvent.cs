using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Board.Events
{
    public class BoardRejectEvent : IEvent
    {
        public static readonly String NAME = "BoardRejectEvent";



        public string getEventName()
        {
            return NAME;
        }
    }
}
