using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game
{
    interface IStateMachine
    {
        void consumeEvent(String eventName);
    }
}
