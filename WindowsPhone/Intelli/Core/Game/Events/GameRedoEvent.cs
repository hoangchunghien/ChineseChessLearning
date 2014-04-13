using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Events
{
    public class GameRedoEvent : IEvent
    {
        public static readonly Stirng NAME = "GameRedoEvent";
        public string getName()
        {
            throw new NotImplementedException();
        }
    }
}
