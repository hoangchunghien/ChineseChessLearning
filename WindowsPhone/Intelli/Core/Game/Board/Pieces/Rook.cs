using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game.Board.Pieces
{
    public class Rook : Piece
    {
        public Rook(Board board)
        {
            this.board = board;
        }

        public override List<Position> getValidNextPositions()
        {
            throw new NotImplementedException();
        }
    }
}
