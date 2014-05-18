using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Player.Events
{
    public class PlayerResignEvent : IEvent
    {
        public static readonly String NAME = "PlayerResignEvent";
        public string getEventName()
        {
            throw new NotImplementedException();
        }
    }
}
