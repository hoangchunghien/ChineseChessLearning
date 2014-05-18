using Intelli.Core.Game.Player.Events;
using IntelliCore.Core.Game.Player.Notifies;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Player.States
{
    public class PlayerTurningState : IState
    {
        public static readonly String NAME = "PlayerTurningState";
        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        private PlayerStateMachine playerStateMachine;

        private Dictionary<String, IState> transitionableStates = new Dictionary<string, IState>();

        public PlayerTurningState(PlayerStateMachine playerStateMachine)
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
            LOG.Info("Player turning");
            this.playerStateMachine.fireStateChangedNotification(new PlayerTurningNotify());
            this.playerStateMachine.consumeEvent(new PlayerPlayEvent());
        }

        public Dictionary<string, IState> getTransitionableState()
        {
            return this.transitionableStates;
        }
    }
}
