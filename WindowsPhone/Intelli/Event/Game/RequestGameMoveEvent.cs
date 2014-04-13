using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Event.Game
{
    public class RequestGameMoveEvent
    {
        private int pid;

        private PositionDetail cPos;

        private PositionDetail nPos;

        public RequestGameMoveEvent(int pid, PositionDetail cPos, PositionDetail nPos)
        {
            this.pid = pid;
            this.cPos = cPos;
            this.nPos = nPos;
        }

        public int getPlayerId()
        {
            return this.pid;
        }

        public PositionDetail getCurrentPosition()
        {
            return this.cPos;
        }

        public PositionDetail getNextPosition()
        {
            return this.nPos;
        }
    }
}
