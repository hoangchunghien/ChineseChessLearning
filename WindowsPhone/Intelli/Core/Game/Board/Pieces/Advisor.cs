using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game.Board.Pieces
{
    public class Advisor : Piece
    {

        public Advisor(Board board)
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
            int row = this.currentPosition.getRow();
            int col = this.currentPosition.getCol();

            List<Position> results = new List<Position>();

            // 4h30
            if ( row < 9 && col < 5)
            {
                Position p = new Position(row + 1, col + 1);
                results.Add(p);
            }

            // 7h30
            if (row < 9 && col > 3)
            {
                Position p = new Position(row + 1, col - 1);
                results.Add(p);
            }

            // 10h30
            if (row > 7 && col > 3)
            {
                Position p = new Position(row - 1, col - 1);
                results.Add(p);
            }

            // 1h30
            if (row > 7 && col < 5)
            {
                Position p = new Position(row - 1, col + 1);
                results.Add(p);
            }

            return results;
        }

        private List<Position> _getValidBlackNextPositions()
        {
            int row = this.currentPosition.getRow();
            int col = this.currentPosition.getCol();

            List<Position> results = new List<Position>();

            // 4h30
            if (row < 2 && col < 5)
            {
                Position p = new Position(row + 1, col + 1);
                results.Add(p);
            }

            // 7h30
            if (row < 2 && col > 3)
            {
                Position p = new Position(row + 1, col - 1);
                results.Add(p);
            }

            // 10h30
            if (row > 0 && col > 3)
            {
                Position p = new Position(row - 1, col - 1);
                results.Add(p);
            }

            // 1h30
            if (row > 0 && col < 5)
            {
                Position p = new Position(row - 1, col + 1);
                results.Add(p);
            }

            return results;
        }

    }


}
