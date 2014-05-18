using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Player.Events
{
    public class PlayerPlayEvent : IEvent
    {
        public static readonly String NAME = "PlayerPlayEvent";
        public string getEventName()
        {
            return NAME;
        }
    }
}
