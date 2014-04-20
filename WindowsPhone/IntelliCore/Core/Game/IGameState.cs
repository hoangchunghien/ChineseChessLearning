using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game
{
    public interface IGameState : IState
    {
        bool isSubmachineEvent(IEvent e);

        void submachineConsumeEvent(IEvent e);
    }
}
