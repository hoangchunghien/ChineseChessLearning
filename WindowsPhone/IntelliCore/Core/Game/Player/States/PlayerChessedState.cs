using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Player.States
{
    public class PlayerChessedState : IState
    {
        public static readonly String NAME = "PlayerJoinedState";
        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        private PlayerStateMachine playerStateMachine;

        private Dictionary<String, IState> transitionableStates = new Dictionary<string, IState>();

        public PlayerChessedState(PlayerStateMachine playerStateMachine)
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
            LOG.Info("Player chessed");
        }

        public Dictionary<string, IState> getTransitionableState()
        {
            return this.transitionableStates;
        }
    }
}
