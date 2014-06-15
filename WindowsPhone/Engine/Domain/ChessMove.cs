using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Domain
{
    public class ChessMove
    {
        //Piece m_piece;            // The Piece that moves.
        Position m_to;         // position to move to.
        Position m_from;      // position to move from

        //Piece m_captured;

        public ChessMove(int r, int c, int _r, int _c)
        {
            m_from = new Position(r, c);
            m_to = new Position(_r, _c);
        }

        public Position To
        {
            get { return m_to; }
            set { m_to = value; }
        }

        public Position From
        {
            get { return m_from; }
        }

        public override string ToString()
        {
            return (m_from.X.ToString() + m_from.Y.ToString() + m_to.X.ToString() + m_to.ToString());
        }
    }
}
