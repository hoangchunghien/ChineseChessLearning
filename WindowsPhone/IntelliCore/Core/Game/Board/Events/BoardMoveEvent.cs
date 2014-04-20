using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Core.Game.Board.Events
{
    public class BoardMoveEvent : IEvent
    {
        public static readonly String NAME = "BoardMoveEvent";

        private Position currentPosition;
        private Position nextPosition;

        public string getName()
        {
            return NAME;
        }

        public BoardMoveEvent(Position current, Position next)
        {
            this.currentPosition = current;
            this.nextPosition = next;
        }

        public Position getCurrentPosition()
        {
            return this.currentPosition;
        }

        public Position getNextPosition()
        {
            return this.nextPosition;
        }
    }
}
