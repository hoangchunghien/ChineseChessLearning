using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Event.Game
{
    public class PositionDetail
    {
        private int r;

        private int c;

        public PositionDetail(int r, int c)
        {
            this.r = r;
            this.c = c;
        }

        public int getRow()
        {
            return this.r;
        }

        public int getCol() {
            return this.c;
        }

    }
}
