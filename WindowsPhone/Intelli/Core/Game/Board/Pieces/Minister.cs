using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game.Board.Pieces
{
    public class Minister : Piece
    {
        public Minister(Board board)
        {
            this.board = board;
        }



        public override List<Position> getValidNextPositions()
        {

            this.validNextPositions = new List<Position>();
            List<Position> validPositions;

            if (this.color == Color.BLACK)
            {
                validPositions = _getValidBlackNextPositions();
            }
            else
            {
                validPositions = _getValidRedNextPositions();
            }

            foreach (Position p in validPositions)
            {
                this._addNextPosition(p, this.color);
            }

            return this.validNextPositions;
        }

        private List<Position> _getValidRedNextPositions()
        {
            int row = this.getCurrentPosition().getRow();
            int col = this.getCurrentPosition().getCol();

            List<Position> validPositions = new List<Position>();

            // 4h30
            if (row < 9 && col < 8)
            {
                // TODO, move checking to a checking method
                if (this.board.getPieces()[row + 1, col + 1] == null)
                {
                    Position p = new Position(row + 2, col + 2);
                    validPositions.Add(p);
                }
            }

            // 7h30
            if (row < 9 && col > 0)
            {
                if (this.board.getPieces()[row + 1, col - 1] == null)
                {
                    Position p = new Position(row + 2, col - 2);
                    validPositions.Add(p);
                }
            }

            // 10h30
            if (row > 5 && col > 0)
            {
                if (this.board.getPieces()[row - 1, col - 1] == null)
                {
                    Position p = new Position(row - 2, col - 2);
                    validPositions.Add(p);
                }
            }

            // 1h30
            if (row > 5 && col < 8)
            {
                if (this.board.getPieces()[row - 1, col + 1] == null)
                {
                    Position p = new Position(row - 2, col + 2);
                    validPositions.Add(p);
                }
            }

            return validPositions;
        }

        private List<Position> _getValidBlackNextPositions()
        {
            int row = this.getCurrentPosition().getRow();
            int col = this.getCurrentPosition().getCol();

            List<Position> validPositions = new List<Position>();

            // 4h30
            if (row < 4 && col < 8)
            {
                if (this.board.getPieces()[row + 1, col + 1] == null)
                {
                    Position p = new Position(row + 2, col + 2);
                    validPositions.Add(p);
                }
            }

            // 7h30
            if (row < 4 && col > 0)
            {
                if (this.board.getPieces()[row + 1, col - 1] == null)
                {
                    Position p = new Position(row + 2, col - 2);
                    validPositions.Add(p);
                }
            }

            // 10h30
            if (row > 0 && col > 0)
            {
                if (this.board.getPieces()[row - 1, col - 1] == null)
                {
                    Position p = new Position(row - 2, col - 2);
                    validPositions.Add(p);
                }
            }

            // 1h30
            if (row > 0 && col < 8)
            {
                if (this.board.getPieces()[row - 1, col + 1] == null)
                {
                    Position p = new Position(row - 2, col + 2);
                    validPositions.Add(p);
                }
            }

            return validPositions;

        }
    }
}
