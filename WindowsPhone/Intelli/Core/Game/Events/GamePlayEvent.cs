using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Events
{
    public class GamePlayEvent : IEvent
    {
        public static readonly String NAME = "GamePlayEvent";
        public string getName()
        {
            throw new NotImplementedException();
        }
    }
}
