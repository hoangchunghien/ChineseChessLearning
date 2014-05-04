using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Events
{
    public class GameInitializedEvent : IEvent
    {
        public static readonly String NAME = "GameInitializedEvent";
        public string getName()
        {
            return NAME;
        }
    }
}
