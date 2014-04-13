using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Events
{
    public class GameUndoedEvent : IEvent
    {
        public static readonly String NAME = "GameUndoedEvent";
        public string getName()
        {
            throw new NotImplementedException();
        }
    }
}
