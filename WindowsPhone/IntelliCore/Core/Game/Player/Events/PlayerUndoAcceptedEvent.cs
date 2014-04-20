using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Player.Events
{
    public class PlayerUndoAcceptedEvent : IEvent
    {
        public static readonly String NAME = "PlayerUndoAcceptedEvent";
        public string getName()
        {
            throw new NotImplementedException();
        }
    }
}
