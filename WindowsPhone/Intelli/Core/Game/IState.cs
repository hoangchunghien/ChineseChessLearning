using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game
{
    public interface IState
    {
        String getName();
        void run();
        Dictionary<String, IState> getTransitionableState();
    }
}
