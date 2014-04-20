using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Player.Events
{
    public class PlayerUndoAcceptEvent : IEvent
    {
        public static readonly String NAME = "PlayerUndoAcceptEvent";
        public string getName()
        {
            throw new NotImplementedException();
        }
    }
}
