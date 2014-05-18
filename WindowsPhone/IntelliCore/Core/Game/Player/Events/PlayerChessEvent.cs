using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Player.Events
{
    public class PlayerChessEvent : IEvent
    {
        public static readonly String NAME = "PlayerChessEvent";
        public string getEventName()
        {
            throw new NotImplementedException();
        }
    }
}
