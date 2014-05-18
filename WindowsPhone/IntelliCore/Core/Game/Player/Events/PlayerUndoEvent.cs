using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Player.Events
{
    public class PlayerUndoEvent : IEvent
    {
        public static readonly String NAME = "PlayerUndoEvent";
        public string getEventName()
        {
            throw new NotImplementedException();
        }
    }
}
