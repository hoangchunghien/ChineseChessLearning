using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliCore.Event.Game
{
    public class PlayerJoinedEvent
    {
        private bool accepted = true;

        public bool isAccepted()
        {
            return this.accepted;
        }

        public static PlayerJoinedEvent notAcceptable()
        {
            PlayerJoinedEvent e = new PlayerJoinedEvent();
            e.accepted = false;
            return e;
        }
    }
}
