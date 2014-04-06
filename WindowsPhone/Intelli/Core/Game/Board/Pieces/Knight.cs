using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game.Board.Pieces
{
    public class Knight : Piece
    {
        public Knight(Board board)
        {
            this.board = board;
        }

        public override List<Position> getValidNextPositions()
        {
            throw new NotImplementedException();
        }
    }
}
