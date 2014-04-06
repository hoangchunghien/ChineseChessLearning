using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game.Board.Pieces
{
    public class Master : Piece
    {

        public Master(Board board)
        {
            this.board = board;
        }

        public override List<Position> getValidNextPositions()
        {
            this.validNextPositions = new List<Position>();

            List<Position> validPositions;
            if (this.color == Color.RED)
            {
                validPositions = _getValidRedNextPositions();
            }
            else
            {
                validPositions = _getValidBlackNextPositions();
            }

            foreach (Position p in validPositions)
            {
                _addNextPosition(p, this.color);
            }

            return this.validNextPositions;
        }

        private List<Position> _getValidRedNextPositions()
        {
            int col = this.getCurrentPosition().getCol();
            int row = this.getCurrentPosition().getRow();

            List<Position> validPositions = new List<Position>();
            if (row > 7)
            {
                Position p = new Position(row - 1, col);
                validPositions.Add(p);
            }
            if (row < 9)
            {
                Position p = new Position(row + 1, col);
                validPositions.Add(p);
            }
            if (col > 3)
            {
                Position p = new Position(row, col - 1);
                validPositions.Add(p);
            }
            if (col < 5)
            {
                Position p = new Position(row, col + 1);
                validPositions.Add(p);
            }

            return validPositions;
        }

        private List<Position> _getValidBlackNextPositions()
        {
            int col = this.getCurrentPosition().getCol();
            int row = this.getCurrentPosition().getRow();

            List<Position> validPositions = new List<Position>();
            if (row > 0)
            {
                Position p = new Position(row - 1, col);
                validPositions.Add(p);
            }
            if (row < 2)
            {
                Position p = new Position(row + 1, col);
                validPositions.Add(p);
            }
            if (col > 3)
            {
                Position p = new Position(row, col - 1);
                validPositions.Add(p);
            }
            if (col < 5)
            {
                Position p = new Position(row, col + 1);
                validPositions.Add(p);
            }

            return validPositions;
        }
    }
}
