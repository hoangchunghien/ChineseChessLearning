using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game
{
    public interface IState
    {
        String getStateName();
        void run(IEvent e);
        Dictionary<String, IState> getTransitionableState();
    }
}
