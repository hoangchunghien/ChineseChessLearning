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

            this.validNextPositions = new List<Position>();
            List<Position> validPositions;

            validPositions = _getValidNextPositions();

            foreach (Position p in validPositions)
            {
                _addNextPosition(p, this.color);
            }

            return this.validNextPositions;
        }

        private List<Position> _getValidNextPositions()
        {
            int row = this.getCurrentPosition().getRow();
            int col = this.getCurrentPosition().getCol();

            List<Position> validPositions = new List<Position>();

            // Row 
            for (int i = row - 1; i >= 0; i--)
            {
                Position p = new Position(i, col);
                validPositions.Add(p);
                if (this.board.getPieces()[i, col] != null)
                {
                    break;
                }
            }
            for (int i = row + 1; i <= 9; i++)
            {
                Position p = new Position(i, col);
                validPositions.Add(p);
                if (this.board.getPieces()[i, col] != null)
                {
                    break;
                }
            }

            // Col
            for (int i = col - 1; i >= 0; i--)
            {
                Position p = new Position(row, i);
                validPositions.Add(p);
                if (this.board.getPieces()[row, i] != null)
                {
                    break;
                }
            }

            for (int i = col + 1; i <= 8; i++)
            {
                Position p = new Position(row, i);
                validPositions.Add(p);
                if (this.board.getPieces()[row, i] != null)
                {
                    break;
                }
            }

            return validPositions;
        }

    }
}
