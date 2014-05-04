using Intelli.Event.Base;
using Intelli.Event.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliCore.Event.Game
{
    public class RequestPlayerMoveEvent : RequestReadEvent
    {
        private int pid;
        private PositionDetail currentPosition;
        private PositionDetail nextPosition;

        public RequestPlayerMoveEvent(int pid, PositionDetail curr, PositionDetail next)
        {
            this.pid = pid;
            this.currentPosition = curr;
            this.nextPosition = next;
        }

        public int getPid()
        {
            return this.pid;
        }

        public PositionDetail getCurrentPosition()
        {
            return this.currentPosition;
        }

        public PositionDetail getNextPosition()
        {
            return this.nextPosition;    
        }
    }
}   
