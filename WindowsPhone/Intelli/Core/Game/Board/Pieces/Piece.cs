using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game.Board.Pieces
{
    public abstract class Piece
    {
        protected Color color;

        protected Board board;

        protected Position currentPosition;

        protected List<Position> validNextPositions;

        public Color getColor()
        {
            return this.color;
        }

        public void setColor(Color color)
        {
            this.color = color;
        }

        public Position getCurrentPosition()
        {
            return this.currentPosition;
        }

        public void setCurrentPosition(Position p)
        {
            this.currentPosition = p;
        }

        public abstract List<Position> getValidNextPositions();

        protected void _addNextPosition(Position p, Color color)
        {
            if (this.board.getPieces()[p.getRow(), p.getCol()] == null ||
                this.board.getPieces()[p.getRow(), p.getCol()].getColor() != color)
                this.validNextPositions.Add(p);
        }

    }

    public enum Color
    {
        BLACK = 1,
        RED = 0
    }
}
