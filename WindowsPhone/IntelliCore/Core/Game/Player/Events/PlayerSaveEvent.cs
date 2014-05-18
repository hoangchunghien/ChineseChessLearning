using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Player.Events
{
    public class PlayerSaveEvent : IEvent
    {
        public static readonly String NAME = "PlayerSaveEvent";
        public string getEventName()
        {
            throw new NotImplementedException();
        }
    }
}
