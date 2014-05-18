using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Player.States
{
    public class PlayerUndoAcceptedState : IState
    {
        public static readonly String NAME = "PlayerUndoAcceptedState";
        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        private PlayerStateMachine playerStateMachine;

        private Dictionary<String, IState> transitionableStates = new Dictionary<string, IState>();

        public PlayerUndoAcceptedState(PlayerStateMachine playerStateMachine)
        {
            // TODO: Complete member initialization
            this.playerStateMachine = playerStateMachine;
        }

        public string getStateName()
        {
            return NAME;
        }

        public void run(IEvent e)
        {
            LOG.Info("Player turning accepted");
        }

        public Dictionary<string, IState> getTransitionableState()
        {
            return this.transitionableStates;
        }
    }
}
