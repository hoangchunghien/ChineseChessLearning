using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Player.Events
{
    public class PlayerNoValidMoveEvent : IEvent
    {
        public static readonly String NAME = "PlayerNoValidMoveEvent";
        public string getName()
        {
            throw new NotImplementedException();
        }
    }
}
