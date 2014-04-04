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

        public RequestValidMovesEvent(PositionDetail positionDetail)
        {
            this.positionDetail = positionDetail;
        }

        public PositionDetail getPositionDetail()
        { 
            return this.positionDetail;
        }

    }
}
