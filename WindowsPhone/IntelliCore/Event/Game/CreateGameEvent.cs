using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Event.Game
{
    public class GameCreateEvent
    {
        private bool isCreated;
        public GameCreateEvent(bool isCreated)
        {
            this.isCreated = isCreated;
        }

        public void setIsCreate(bool isCreated)
        {
            this.isCreated = isCreated;
        }

        public bool getIsCreated()
        {
            return this.isCreated;
        }
    }
}
