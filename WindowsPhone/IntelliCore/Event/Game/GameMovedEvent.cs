using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Event.Game
{
    public class GameMovedEvent
    {

        public static readonly int ST_ACCEPTED = 1;
        public static readonly int ST_REJECTED = 2;

        private int status;

        private BoardDetail boardDetail;
    }
}
