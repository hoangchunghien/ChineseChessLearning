using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game.Board.Pieces
{
    public class Cannon : Piece
    {
        public Cannon(Board board)
        {
            this.board = board;
        }

        public override List<Position> getValidNextPositions()
        {
            this.validNextPositions = new List<Position>();

            List<Position> validPositions = _getValidNextPositions();

            foreach (Position  p in validPositions)
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

            #region Check cannon row
            // Row down to zero
            for (int i = row - 1; i >= 0; i--)
            {
                if (this.board.getPieces()[i, col] == null)
                {
                    Position p = new Position(i, col);
                    validPositions.Add(p);
                }
                else
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (this.board.getPieces()[j, col] != null)
                        {
                            Position _p = new Position(j, col);
                            validPositions.Add(_p);
                            break;
                        }
                    }
                    break;
                }
            }

            // Row up to 9
            for (int i = row + 1; i <= 9; i++)
            {
                if (this.board.getPieces()[i, col] == null)
                {
                    Position p = new Position(i, col);
                    validPositions.Add(p);
                }
                else
                {
                    for (int j = i + 1; j <= 9; j++)
                    {
                        if (this.board.getPieces()[j, col] != null)
                        {
                            Position _p = new Position(j, col);
                            validPositions.Add(_p);
                            break;
                        }
                    }
                    break;
                }
            } 
            #endregion

            #region Check cannon col
            // Col down to zero
            for (int i = col - 1; i >= 0; i--)
            {
                if (this.board.getPieces()[row, i] == null)
                {
                    Position p = new Position(row, i);
                    validPositions.Add(p);
                }
                else
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (this.board.getPieces()[row, j] != null)
                        {
                            Position _p = new Position(row, j);
                            validPositions.Add(_p);
                            break;
                        }
                    }
                    break;
                }
            }

            // Col up to 8
            for (int i = col + 1; i <= 8; i++)
            {
                if (this.board.getPieces()[row, i] == null)
                {
                    Position p = new Position(row, i);
                    validPositions.Add(p);
                }
                else
                {
                    for (int j = i + 1; j <= 8; j++)
                    {
                        if (this.board.getPieces()[row, j] != null)
                        {
                            Position _p = new Position(row, j);
                            validPositions.Add(_p);
                            break;
                        }
                    }
                    break;
                }
            } 
            #endregion

            return validPositions;
        }

    }
}
