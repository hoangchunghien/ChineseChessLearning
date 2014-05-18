using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Events
{
    public class GamePlayedEvent : IEvent
    {
        public static readonly String NAME = "GamePlayedEvent";
        public string getEventName()
        {
            throw new NotImplementedException();
        }
    }
}
