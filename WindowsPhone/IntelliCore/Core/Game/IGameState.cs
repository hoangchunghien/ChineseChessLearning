using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game
{
    /// <summary>
    /// State for GameStateMachine, it's consist of states of game and had two sub-machines: PlayerMachine, BoardMachine
    /// </summary>
    public interface IGameState : IState
    {
        /// <summary>
        /// Check whether event parameter is event of sub-machine
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        bool isSubmachineEvent(IEvent e);

        /// <summary>
        /// Transite to sub-machine to consume event parameter (run the state of sub-machine)
        /// </summary>
        /// <param name="e"></param>
        void submachineConsumeEvent(IEvent e);
    }
}
