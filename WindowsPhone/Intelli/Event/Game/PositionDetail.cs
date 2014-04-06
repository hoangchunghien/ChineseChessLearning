using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Event.Game
{
    public class PositionDetail
    {
        private int x;

        private int y;

        public PositionDetail(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int getX()
        {
            return this.x;
        }

        public int getY() {
            return this.y;
        }

    }
}
