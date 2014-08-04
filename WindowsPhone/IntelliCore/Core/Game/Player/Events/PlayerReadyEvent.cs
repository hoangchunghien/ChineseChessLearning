using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Player.Events
{
    public class PlayerReadyEvent : IEvent
    {
        public static readonly String NAME = "PlayerReadyEvent";

        private int id;

        public PlayerReadyEvent(int id)
        {
            this.id = id;
        }

        public int getId()
        {
            return this.id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public string getEventName()
        {
            return NAME;
        }
    }
}
