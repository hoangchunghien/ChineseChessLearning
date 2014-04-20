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
            this.validNextPositions = new List<Position>();
            List<Position> validPositions = _getValidNextPositions();

            foreach (Position p in validPositions)
            {
                this._addNextPosition(p, this.color);
            }

            return this.validNextPositions;
        }

        private List<Position> _getValidNextPositions()
        {
            int row = this.getCurrentPosition().getRow();
            int col = this.getCurrentPosition().getCol();

            List<Position> validPositions = new List<Position>();

            // 12h
            if ((row - 1) > 0 && this.board.getPieces()[row -1, col] == null)
            {
                if (col > 0) // Left col
                {
                    Position p = new Position(row - 2, col - 1);
                    validPositions.Add(p);
                }
                if (col < 8) // Right col
                {
                    Position p = new Position(row - 2, col + 1);
                    validPositions.Add(p);
                }
            }
            
            // 3h
            if ((col + 1 ) < 8 && this.board.getPieces()[row, col + 1] == null)
            {
                if (row > 0) // Down row
                {
                    Position p = new Position(row - 1, col + 2);
                    validPositions.Add(p);
                }
                if (row < 9) // Up row
                {
                    Position p = new Position(row + 1, col + 2);
                    validPositions.Add(p);
                }
            }

            // 6h
            if ((row + 1) < 9 && this.board.getPieces()[row + 1, col] == null)
            {
                if (col > 0) // Left col
                {
                    Position p = new Position(row + 2, col - 1);
                    validPositions.Add(p);
                }
                if (col < 8) // Right col
                {
                    Position p = new Position(row + 2, col + 1);
                    validPositions.Add(p);
                }
            }

            // 9h
            if ((col - 1) > 0 && this.board.getPieces()[row, col - 1] == null)
            {
                if (row > 0) // Down row
                {
                    Position p = new Position(row - 1, col - 2);
                    validPositions.Add(p);
                }
                if (row < 9) // Up row
                {
                    Position p = new Position(row + 1, col - 2);
                    validPositions.Add(p);
                }
            }

            return validPositions;
        }
    }
}
