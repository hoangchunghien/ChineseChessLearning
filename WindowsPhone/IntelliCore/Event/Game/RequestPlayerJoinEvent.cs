using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliCore.Event.Game
{
    public class RequestPlayerJoinEvent
    {
        int id;

        public RequestPlayerJoinEvent(int id)
        {
            this.id = id;
        }

        public int getId()
        {
            return this.id;
        }
    }
}
