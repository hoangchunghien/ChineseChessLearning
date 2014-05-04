using Intelli.Event.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliCore.Event.Game
{
    public class RequestPlayerReadyEvent : RequestReadEvent
    {
        private int id;

        public RequestPlayerReadyEvent(int id)
        {
            this.id = id;
        }

        public int getId()
        {
            return this.id;
        }
    }
}
