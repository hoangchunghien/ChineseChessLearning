using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Event.Game
{
    public class GameCreatedEvent
    {
        private GameDetail detail;

        public GameCreatedEvent(GameDetail detail)
        {
            this.detail = detail;
        }

        public GameDetail getDetail()
        {
            return this.detail;
        }


        internal static GameCreatedEvent CreateFail()
        {
            return new GameCreatedEvent(null);
        }
    }
}
