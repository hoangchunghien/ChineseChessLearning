using Intelli.Core.Game.Player.Events;
using Intelli.Core.Game.Player.Notifies;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Player.States
{
    public class PlayerInitializingState : IState
    {
        public static readonly String NAME = "PlayerInitializingState";

        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        private PlayerStateMachine playerMachine;
        private Dictionary<String, IState> transitionableStates = new Dictionary<string, IState>();

        public PlayerInitializingState(PlayerStateMachine playerMachine)
        {
            this.playerMachine = playerMachine;
        }

        public string getName()
        {
            return NAME;
        }

        public void run(IEvent e)
        {
            this.playerMachine.fireStateChangedNotification(new PlayerInitializingNotify());
            LOG.Info(NAME);

            playerMachine.consumeEvent(new PlayerInitializedEvent());
        }

        public Dictionary<string, IState> getTransitionableState()
        {
            return this.transitionableStates;
        }
    }
}
