using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game.Board
{
    public class Position
    {
        private int r;
        private int c;

        public Position(int r, int c)
        {
            this.r = r;
            this.c = c;
        }

        public int getRow()
        {
            return this.r;
        }

        public void setRow(int r)
        {
            this.r = r;
        }

        public int getCol()
        {
            return this.c;
        }

        public void setCol(int c)
        {
            this.c = c;
        }

        public override bool Equals(object obj)
        {
            if (this == obj == null) return false;

            if (this.r == ((Position)obj).r && this.c == ((Position)obj).c)
                return true;
            else
                return false;
        }

        public override String ToString()
        {
            String result = "";

            result += "(" + this.r + ", " + this.c + ")";
            return result;
        }
    }
}
