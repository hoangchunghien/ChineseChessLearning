using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Events
{
    public class GameEndEvent : IEvent
    {
        public static readonly String NAME = "GameEndEvent";
        public string getEventName()
        {
            throw new NotImplementedException();
        }
    }
}
