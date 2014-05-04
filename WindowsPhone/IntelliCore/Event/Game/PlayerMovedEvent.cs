using Intelli.Event.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliCore.Event.Game
{
    public class PlayerMovedEvent: ReadEvent
    {

        public static PlayerMovedEvent notAcceptable()
        {
            PlayerMovedEvent readyEvent = new PlayerMovedEvent();
            readyEvent.accepted = false;
            return readyEvent;
        }
    }
}
