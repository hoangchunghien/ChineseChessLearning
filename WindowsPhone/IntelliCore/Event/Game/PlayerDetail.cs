using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Event.Game
{
    public class PlayerDetail
    {
        List<Core.Game.Board.Pieces.Piece> pieces;
        public PlayerDetail(List<Intelli.Core.Game.Board.Pieces.Piece> pieces)
        {
            this.pieces = pieces;
        }

        public List<Core.Game.Board.Pieces.Piece> getPieces()
        {
            return this.pieces;
        }
    }
}
