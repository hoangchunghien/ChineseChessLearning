using Intelli.Core.Game.Board.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game.Board
{
    public class Board
    {
        private Piece[,] pieces;

        public Board()
        {
            _initialize();
        }

        private void _initialize()
        {
            this.pieces = new Piece[10, 9];
            _initializeBlackSide();
            _initializeRedSide();
        }

        private void _initializeBlackSide()
        {
            // Black master
            Piece master = new Master(this);
            master.setColor(Color.BLACK);
            master.setCurrentPosition(new Position(0, 4));
            this.pieces[0, 4] = master;

            // Black advisor
            Piece advisor1 = new Advisor(this);
            advisor1.setColor(Color.BLACK);
            advisor1.setCurrentPosition(new Position(0, 3));
            this.pieces[0, 3] = advisor1;

            Piece advisor2 = new Advisor(this);
            advisor2.setColor(Color.BLACK);
            advisor2.setCurrentPosition(new Position(0, 5));
            this.pieces[0, 5] = advisor2;

            // Black minister
            Piece minister1 = new Minister(this);
            minister1.setColor(Color.BLACK);
            minister1.setCurrentPosition(new Position(0, 2));
            this.pieces[0, 2] = minister1;

            Piece minister2 = new Minister(this);
            minister2.setColor(Color.BLACK);
            minister2.setCurrentPosition(new Position(0, 6));
            this.pieces[0, 6] = minister2;

            // Black knight
            Piece knight1 = new Knight(this);
            knight1.setColor(Color.BLACK);
            knight1.setCurrentPosition(new Position(0, 1));
            this.pieces[0, 1] = knight1;

            Piece knight2 = new Knight(this);
            knight2.setColor(Color.BLACK);
            knight2.setCurrentPosition(new Position(0, 7));
            this.pieces[0, 7] = knight2;

            // Black rook
            Piece rook1 = new Rook(this);
            rook1.setColor(Color.BLACK);
            rook1.setCurrentPosition(new Position(0, 0));
            this.pieces[0, 0] = rook1;

            Piece rook2 = new Rook(this);
            rook2.setColor(Color.BLACK);
            rook2.setCurrentPosition(new Position(0, 8));
            this.pieces[0, 8] = rook2;

            // Black cannon
            Piece cannon1 = new Cannon(this);
            cannon1.setColor(Color.BLACK);
            cannon1.setCurrentPosition(new Position(2, 1));
            this.pieces[2, 1] = cannon1;

            Piece cannon2 = new Cannon(this);
            cannon2.setColor(Color.BLACK);
            cannon2.setCurrentPosition(new Position(2, 7));
            this.pieces[2, 7] = cannon2;

            // Black pawn
            Piece pawn1 = new Pawn(this);
            pawn1.setColor(Color.BLACK);
            pawn1.setCurrentPosition(new Position(3, 0));
            this.pieces[3, 0] = pawn1;

            Piece pawn2 = new Pawn(this);
            pawn2.setColor(Color.BLACK);
            pawn2.setCurrentPosition(new Position(3, 2));
            this.pieces[3, 2] = pawn2;

            Piece pawn3 = new Pawn(this);
            pawn3.setColor(Color.BLACK);
            pawn3.setCurrentPosition(new Position(3, 4));
            this.pieces[3, 4] = pawn3;

            Piece pawn4 = new Pawn(this);
            pawn4.setColor(Color.BLACK);
            pawn4.setCurrentPosition(new Position(3, 6));
            this.pieces[3, 6] = pawn4;

            Piece pawn5 = new Pawn(this);
            pawn5.setColor(Color.BLACK);
            pawn5.setCurrentPosition(new Position(3, 8));
            this.pieces[3, 8] = pawn5;
        }

        private void _initializeRedSide()
        {
            // Red master
            Piece master = new Master(this);
            master.setColor(Color.RED);
            master.setCurrentPosition(new Position(9, 4));
            this.pieces[9, 4] = master;

            // Red advisor
            Piece advisor1 = new Advisor(this);
            advisor1.setColor(Color.RED);
            advisor1.setCurrentPosition(new Position(9, 3));
            this.pieces[9, 3] = advisor1;

            Piece advisor2 = new Advisor(this);
            advisor2.setColor(Color.RED);
            advisor2.setCurrentPosition(new Position(9, 5));
            this.pieces[9, 5] = advisor2;

            // Red minister
            Piece minister1 = new Minister(this);
            minister1.setColor(Color.RED);
            minister1.setCurrentPosition(new Position(9, 2));
            this.pieces[9, 2] = minister1;

            Piece minister2 = new Minister(this);
            minister2.setColor(Color.RED);
            minister2.setCurrentPosition(new Position(9, 6));
            this.pieces[9, 6] = minister2;

            // Red knight
            Piece knight1 = new Knight(this);
            knight1.setColor(Color.RED);
            knight1.setCurrentPosition(new Position(9, 1));
            this.pieces[9, 1] = knight1;

            Piece knight2 = new Knight(this);
            knight2.setColor(Color.RED);
            knight2.setCurrentPosition(new Position(9, 7));
            this.pieces[9, 7] = knight2;

            // Red rook
            Piece rook1 = new Rook(this);
            rook1.setColor(Color.RED);
            rook1.setCurrentPosition(new Position(9, 0));
            this.pieces[9, 0] = rook1;

            Piece rook2 = new Rook(this);
            rook2.setColor(Color.RED);
            rook2.setCurrentPosition(new Position(9, 8));
            this.pieces[9, 8] = rook2;

            // Read cannon
            Piece cannon1 = new Cannon(this);
            cannon1.setColor(Color.RED);
            cannon1.setCurrentPosition(new Position(7, 1));
            this.pieces[7, 1] = cannon1;

            Piece cannon2 = new Cannon(this);
            cannon2.setColor(Color.RED);
            cannon2.setCurrentPosition(new Position(7, 7));
            this.pieces[7, 7] = cannon2;

            // Red pawn
            Piece pawn1 = new Pawn(this);
            pawn1.setColor(Color.RED);
            pawn1.setCurrentPosition(new Position(6, 0));
            this.pieces[6, 0] = pawn1;

            Piece pawn2 = new Pawn(this);
            pawn2.setColor(Color.RED);
            pawn2.setCurrentPosition(new Position(6, 2));
            this.pieces[6, 2] = pawn2;

            Piece pawn3 = new Pawn(this);
            pawn3.setColor(Color.RED);
            pawn3.setCurrentPosition(new Position(6, 4));
            this.pieces[6, 4] = pawn3;

            Piece pawn4 = new Pawn(this);
            pawn4.setColor(Color.RED);
            pawn4.setCurrentPosition(new Position(6, 6));
            this.pieces[6, 6] = pawn4;

            Piece pawn5 = new Pawn(this);
            pawn5.setColor(Color.RED);
            pawn5.setCurrentPosition(new Position(6, 8));
            this.pieces[6, 8] = pawn5;
        }

        public Piece[,] getPieces()
        {
            return this.pieces;
        }

        public void deserialize(char[,] bArr)
        {
            /*
             * [[r,k,m,a,g,a,m,k,r],
             *  [ , , , , , , , , ],
             *  [ ,c, , , , , ,c, ],
             *  [p, ,p, ,p, ,p, ,p],
             *  [ , , , , , , , , ],
             *  [ , , , , , , , , ],
             *  [P, ,P, ,P, ,P, ,P],
             *  [ ,C, , , , , ,C, ],
             *  [ , , , , , , , , ],
             *  [R,K,M,A,G,A,M,K,R]]
             */
            this.pieces = new Piece[10, 9];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Piece p = _getPiece(bArr[i, j]);
                    if (p != null)
                        p.setCurrentPosition(new Position(i, j));
                    this.pieces[i, j] = p;
                }
            }
        }

        public char[,] serialize()
        {
            char[,] cBoards = new char[10, 9];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cBoards[i, j] = _getChar(this.getPieces()[i, j]);
                }
            }
            return cBoards;
        }

        public override String ToString()
        {
            char [,] b = this.serialize();
            String result = "[\n";
            for (int i = 0; i < 10; i++)
            {
                result += "[";
                for (int j = 0; j < 9; j++)
                {
                    result += b[i,j] + " ";
                }
                result += "]\n";
            }
            result += "]";

            return result;
        }

        private Piece _getPiece(char c)
        {
            Piece p;
            switch (c)
            {
                case 'r':
                    p = new Rook(this);
                    p.setColor(Color.BLACK);
                    break;
                case 'k':
                    p = new Knight(this);
                    p.setColor(Color.BLACK);
                    break;
                case 'm':
                    p = new Minister(this);
                    p.setColor(Color.BLACK);
                    break;
                case 'a':
                    p = new Advisor(this);
                    p.setColor(Color.BLACK);
                    break;
                case 'g':
                    p = new Master(this);
                    p.setColor(Color.BLACK);
                    break;
                case 'c':
                    p = new Cannon(this);
                    p.setColor(Color.BLACK);
                    break;
                case 'p':
                    p = new Pawn(this);
                    p.setColor(Color.BLACK);
                    break;
                case 'R':
                    p = new Rook(this);
                    p.setColor(Color.RED);
                    break;
                case 'K':
                    p = new Knight(this);
                    p.setColor(Color.RED);
                    break;
                case 'M':
                    p = new Minister(this);
                    p.setColor(Color.RED);
                    break;
                case 'A':
                    p = new Advisor(this);
                    p.setColor(Color.RED);
                    break;
                case 'G':
                    p = new Master(this);
                    p.setColor(Color.RED);
                    break;
                case 'C':
                    p = new Cannon(this);
                    p.setColor(Color.RED);
                    break;
                case 'P':
                    p = new Pawn(this);
                    p.setColor(Color.RED);
                    break;
                default:
                    p = null;
                    break;
            }

            return p;
        }

        private char _getChar(Piece p)
        {
            char c = ' ';
            if (p == null)
            {
                return c;
            }
            if (p.GetType().Equals(typeof(Advisor)))
            {
                c = 'a';
            }
            else if (p.GetType().Equals(typeof(Cannon)))
            {
                c = 'c';
            }
            else if (p.GetType().Equals(typeof(Knight)))
            {
                c = 'k';
            }
            else if (p.GetType().Equals(typeof(Master)))
            {
                c = 'g';
            }
            else if (p.GetType().Equals(typeof(Minister)))
            {
                c = 'm';
            }
            else if (p.GetType().Equals(typeof(Pawn)))
            {
                c = 'p';
            }
            else if (p.GetType().Equals(typeof(Rook)))
            {
                c = 'r';
            }

            if (p.getColor() == Color.BLACK)
            {
                c = Char.ToLower(c);
            }
            else
            {
                c = Char.ToUpper(c);
            }
            return c;
        }
    }
}
