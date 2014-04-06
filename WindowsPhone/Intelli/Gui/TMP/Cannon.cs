using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Intelli.GUI
{
    public class Cannon: Pieces
    {
        public override bool IsLegalMove(int i, int j)
        {
            bool turn = true;
            if (i != Row && j != Col) turn = false;
            //if (row < 0 || row > 9 || col < 0 || col > 8) turn = false;
            if (i >= 0 && i <= 9 && j >= 0 && j <= 8 && (i == Row || j == Col))
            {
                if (Board.Position[i, j].IsEmpty)
                {
                    if (i == Row && j >= 0 && j <= 8)
                    {
                        if (j > Col)
                        {
                            for (int t = Col + 1; t <= j; t++)
                            {
                                if (!Board.Position[i, t].IsEmpty)
                                {
                                    turn = false;
                                    break;
                                }
                            }
                        }
                        if (j < Col)
                        {
                            for (int t = j; t <= Col - 1; t++)
                            {
                                if (!Board.Position[i, t].IsEmpty)
                                {
                                    turn = false;
                                    break;
                                }
                            }
                        }
                    }
                    if (j == Col && i >= 0 && i <= 9)
                    {
                        if (i > Row)
                        {
                            for (int t = Row + 1; t <= i; t++)
                            {
                                if (!Board.Position[t, j].IsEmpty)
                                {
                                    turn = false;
                                    break;
                                }
                            }
                        }
                        if (i < Row)
                        {
                            for (int t = i; t <= Row - 1; t++)
                            {
                                if (!Board.Position[t, j].IsEmpty)
                                {
                                    turn = false;
                                    break;
                                }
                            }
                        }
                    }
                }
                else // Not emplty
                {
                    // Cannon can eat opponent's piece through any piece

                    if (Board.Position[i, j].Color != this.Color)
                    {
                        int count = 0;
                        if (i == Row && j >= 0 && j <= 8)
                        {
                            if (j < Col)
                            {
                                for (int t = j + 1; t <= Col - 1; t++)
                                    if (!Board.Position[i, t].IsEmpty)
                                        count++;
                            }
                            if (j > Col)
                            {
                                for (int t = Col + 1; t < j; t++)
                                    if (!Board.Position[i, t].IsEmpty)
                                        count++;
                            }
                        }
                        if (j == Col && i >= 0 && i <= 9)
                        {
                            if (i < Row)
                            {
                                for (int t = i + 1; t <= Row - 1; t++)
                                    if (!Board.Position[t, j].IsEmpty)
                                        count++;
                            }
                            if (i > Row)
                            {
                                for (int t = Row + 1; t < i; t++)
                                    if (!Board.Position[t, j].IsEmpty)
                                        count++;
                            }
                        }

                        if (count != 1) turn = false;
                    }
                    if (Board.Position[i, j].Color == this.Color)
                        turn = false;
                }
            }

            // Check face to face
            if (Game.Players[0].King.Col == Game.Players[1].King.Col &&
                Col == Game.Players[0].King.Col)
            {
                int count = 0;
                if (j != Col)
                {
                    // Try moving this piece to other position
                    Game.FreeNode(this.Row, this.Col);
                    for (int t = Game.Players[0].King.Row + 1; t < Game.Players[1].King.Row; t++)
                        if (!Board.Position[t, Col].IsEmpty)
                            count++;
                    if (count == 0)
                        turn = false;
                    Game.RollBackNode(this, this.Row, this.Col);
                }
                if (j == Col && (i < Game.Players[0].King.Row || i > Game.Players[1].King.Row))
                {
                    // Try moving this piece to other position
                    Game.FreeNode(this.Row, this.Col);
                    for (int t = Game.Players[0].King.Row + 1; t < Game.Players[1].King.Row; t++)
                        if (!Board.Position[t, Col].IsEmpty)
                            count++;
                    if (count == 0)
                        turn = false;
                    Game.RollBackNode(this, this.Row, this.Col);
                }
            }

            if (turn)
            {
                //if (IsKingSafe(i, j))
                    return true;
            }
            return false;
        }

        public override bool IsKingSafe(int i, int j)
        {
            bool turn = true;

            Pieces tmpPiece = new Pieces();
            int c = 0;
            if (this.Color == -1) c = 1;
           
            bool _isEmpty = Board.Position[i, j].IsEmpty;
            int _color = Board.Position[i, j].Color;
            int p = 0;
            if (Board.Position[i, j].Color == -1) p = 1;

            if (Board.Position[i, j].IsEmpty == false)// i, j is piece of opponent
            {
                if (Board.Position[i, j].Name == "king")
                    tmpPiece = Game.Players[p].King;
                else if (Board.Position[i, j].Name == "advisor")
                {
                    if (Board.Position[i, j].Side == "left")
                        tmpPiece = Game.Players[p].Advisors[0];
                    if (Board.Position[i, j].Side == "right")
                        tmpPiece = Game.Players[p].Advisors[1];
                }
                else if (Board.Position[i, j].Name == "minister")
                {
                    if (Board.Position[i, j].Side == "left")
                        tmpPiece = Game.Players[p].Ministers[0];
                    if (Board.Position[i, j].Side == "right")
                        tmpPiece = Game.Players[p].Ministers[1];
                }
                else if (Board.Position[i, j].Name == "rook")
                {
                    if (Board.Position[i, j].Side == "left")
                        tmpPiece = Game.Players[p].Rooks[0];
                    if (Board.Position[i, j].Side == "right")
                        tmpPiece = Game.Players[p].Rooks[1];
                }
                else if (Board.Position[i, j].Name == "cannon")
                {
                    if (Board.Position[i, j].Side == "left")
                        tmpPiece = Game.Players[p].Cannons[0];
                    if (Board.Position[i, j].Side == "right")
                        tmpPiece = Game.Players[p].Cannons[1];
                }
                else if (Board.Position[i, j].Name == "knight")
                {
                    if (Board.Position[i, j].Side == "left")
                        tmpPiece = Game.Players[p].Knights[0];
                    if (Board.Position[i, j].Side == "right")
                        tmpPiece = Game.Players[p].Knights[1];
                }
                else if (Board.Position[i, j].Name == "pawn")
                {
                    if (Board.Position[i, j].Side == "0")
                        tmpPiece = Game.Players[p].Pawns[0];
                    if (Board.Position[i, j].Side == "1")
                        tmpPiece = Game.Players[p].Pawns[1];
                    if (Board.Position[i, j].Side == "2")
                        tmpPiece = Game.Players[p].Pawns[2];
                    if (Board.Position[i, j].Side == "3")
                        tmpPiece = Game.Players[p].Pawns[3];
                    if (Board.Position[i, j].Side == "4")
                        tmpPiece = Game.Players[p].Pawns[4];
                }
            }
            // Try moving this piece to i, j(i, j is legal move) to specify that king is safe?
            Game.FreeNode(this.Row, this.Col);
            Board.Position[i, j].IsEmpty = false;
            Board.Position[i, j].Name = this.PieceName;
            Board.Position[i, j].Side = this.Side;
            Board.Position[i, j].Color = this.Color;
            int _r = this.Row;
            int _c = this.Col;
            this.Row = i;
            this.Col = j;
            tmpPiece.IsAlive = false;
            // Check that is king safe?
            if (Game.IsKingSafe(this.Color, Game.Players[c].King.Row, Game.Players[c].King.Col) == false)
                turn = false;

            // Roll back to previous move
            Game.RollBackNode(this, _r, _c);

            // If i, j is empty, rollback the node i, j to tmpPiece
            if (!_isEmpty)
                tmpPiece.IsAlive = true;
            Board.Position[i, j].IsEmpty = _isEmpty;
            Board.Position[i, j].Name = tmpPiece.PieceName;
            Board.Position[i, j].Side = tmpPiece.Side;
            //Board.Position[i, j].Color = tmpPiece.Color;
            Board.Position[i, j].Color = _color;

            if (turn)
                return true;
            else return false;
        }
    }
}
