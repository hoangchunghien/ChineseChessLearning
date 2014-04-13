using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Board.Events
{
    public class ReadyEvent : IEvent
    {
        public static readonly String NAME = "BoardReadyEvent";
        public string getName()
        {
            return NAME;
        }
    }
}
