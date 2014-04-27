using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game
{
    public interface IStateMachine
    {
        bool consumeEvent(IEvent e);

        void fireStateChangedNotification(INotify notify);
    }
}
