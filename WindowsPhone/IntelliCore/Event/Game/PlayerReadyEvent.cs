using Intelli.Event.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliCore.Event.Game
{
    public class PlayerReadyEvent : ReadEvent
    {
        public static PlayerReadyEvent notAcceptable()
        {
            PlayerReadyEvent readyEvent = new PlayerReadyEvent();
            readyEvent.accepted = false;
            return readyEvent;
        }
    }
}
