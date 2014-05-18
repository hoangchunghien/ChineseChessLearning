using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Player.Events
{
    public class PlayerJoinEvent  : IEvent
    {
        public static readonly String NAME = "PlayerJoinEvent";

        private int id;

        // Id of the player
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
