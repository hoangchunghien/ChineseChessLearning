using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game.Board.Pieces
{
    public class Pawn: Piece
    {
        public Pawn(Board board)
        {
            this.board = board;
        }

        public override List<Position> getValidNextPositions()
        {
            if (this.color == Color.RED)
            {
                return _getRedValidPostitions();
            }
            else
            {
                return _getBlackValidPositions();
            }
        }

        private List<Position> _getRedValidPostitions()
        {
            this.validNextPositions = new List<Position>();

            int row = this.currentPosition.getRow();
            int col = this.currentPosition.getCol();
            if (row >= 5)
            {
                Position p = new Position(row - 1, this.currentPosition.getCol());
                _addNextPosition(p, Color.RED);
            }
            else
            {
                if (row > 0)
                {
                    Position p = new Position(row - 1, col);
                    _addNextPosition(p, Color.RED);
                }

                if (col < 8)
                {
                    Position p = new Position(row, col + 1);
                    _addNextPosition(p, Color.RED);
                }

                if (col > 0)
                {
                    Position p = new Position(row, col - 1);
                    _addNextPosition(p, Color.RED);
                }
            }

            return this.validNextPositions;
        }

        private List<Position> _getBlackValidPositions()
        {
            this.validNextPositions = new List<Position>();

            int row = this.currentPosition.getRow();
            int col = this.currentPosition.getCol();
            if (row < 5)
            {
                Position p = new Position(row + 1, this.currentPosition.getCol());
                _addNextPosition(p, Color.BLACK);
            }
            else
            {
                if (row < 9)
                {
                    Position p = new Position(row + 1, col);
                    _addNextPosition(p, Color.BLACK);
                }

                if (col < 8)
                {
                    Position p = new Position(row, col + 1);
                    _addNextPosition(p, Color.BLACK);
                }

                if (col > 0)
                {
                    Position p = new Position(row, col - 1);
                    _addNextPosition(p, Color.BLACK);
                }
            }

            return this.validNextPositions;
        }

    }
}
