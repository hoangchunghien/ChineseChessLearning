using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Event.Game
{
    public class BoardDetail
    {
        char[,] pieces;

        public BoardDetail(char[,] pieces)
        {
            this.pieces = pieces;
        }

        public char[,] getPieces()
        {
            return this.pieces;
        }
    }
}
