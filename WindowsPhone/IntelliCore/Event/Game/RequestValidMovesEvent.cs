using Intelli.Event.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Event.Game
{
    public class RequestValidMovesEvent : RequestReadEvent
    {
        private PositionDetail positionDetail;
        private int playerId;

        public RequestValidMovesEvent(PositionDetail positionDetail, int playerId)
        {
            this.positionDetail = positionDetail;
            this.playerId = playerId;
        }

        public PositionDetail getPositionDetail()
        { 
            return this.positionDetail;
        }

        public int PlayerId()
        {
            return this.playerId;
        }

    }
}
