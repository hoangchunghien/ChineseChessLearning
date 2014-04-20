using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Player.Events
{
    public class PlayerUndoRejectEvent : IEvent
    {
        public static readonly String NAME = "PlayerUndoRejectEvent";
        public string getName()
        {
            throw new NotImplementedException();
        }
    }
}
