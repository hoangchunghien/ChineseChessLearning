using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intelli.Event.Base;

namespace Intelli.Event.Game
{
    public class ValidMovesEvent : ReadEvent
    {
        private List<PositionDetail> validMoves;

        public ValidMovesEvent(List<PositionDetail> validMoves)
        {
            this.validMoves = validMoves;
        }

        public List<PositionDetail> getValidMoves() 
        {  
            return this.validMoves; 
        }

        public static ValidMovesEvent notFound()
        {
            ValidMovesEvent ev = new ValidMovesEvent(null);
            ev.accepted = false;
            return ev;
        }
    }
}
