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
    public class King : Pieces
    {
        public override bool IsLegalMove(int i, int j)
        {
            bool turn = false;
            //int i; int j;
            Pieces tmpPiece = new Pieces();

            if ((i >= 0 && i <= 2 && j >= 3 && j <= 5) || (i >= 7 && i <= 9 && j >= 3 && j <= 5))
                if ((i == Row + 1 && j == Col) || (i == Row - 1 && j == Col) ||
                    (i == Row && j == Col + 1) || (i == Row && j == Col - 1))
                {
                    if (Board.Position[i, j].IsEmpty)
                        turn = true;
                    else
                        if (Board.Position[i, j].Color != this.Color)
                            turn = true;
                }

            // ************Check king face to face*****************
            bool turn2 = true;
            if (this.Color == 1)
            {
                int count = 0;
                if (j == Game.Players[1].King.Col) // Position j of opponent's king
                {
                    // Check row j has any piece, if not exists that means face to face
                    if (Board.Position[i, j].IsEmpty)
                    {
                        for (int t = Row + 1; t < Game.Players[1].King.Row; t++)
                            if (!Board.Position[t, j].IsEmpty)
                                count++;
                        if (count == 0)
                            turn2 = false;
                    }
                    // Check row j has one piece (piece of opponent's color), if exists one that mean face to face, can't eat them
                    else if (!Board.Position[i, j].IsEmpty && Board.Position[i, j].Color == -1)
                    {
                        int r = 0;
                        if (Row == i) r = Row;
                        else r = Row + 1;
                        for (int t = r; t < Game.Players[1].King.Row; t++)
                            if (!Board.Position[t, j].IsEmpty)
                                count++;
                        if (count == 1)
                            turn2 = false;
                    }
                }
            }
            else if (this.Color == -1)
            {
                int count = 0;
                if (j == Game.Players[0].King.Col)
                {
                    // Check row j has any piece, if not exists that means face to face
                    if (Board.Position[i, j].IsEmpty)
                    {
                        for (int t = Row - 1; t > Game.Players[0].King.Row; t--)
                            if (!Board.Position[t, j].IsEmpty)
                                count++;
                        if (count == 0)
                            turn2 = false;
                    }
                    // Check row j has one piece (piece of opponent's color), if exists one that mean face to face, can't eat them
                    else if (!Board.Position[i, j].IsEmpty && Board.Position[i, j].Color == 1)
                    {
                        int r = 0;
                        if (Row == i) r = Row;
                        else r = Row - 1;
                        for (int t = r; t > Game.Players[0].King.Row; t--)
                            if (!Board.Position[t, j].IsEmpty)
                                count++;
                        if (count == 1)
                            turn2 = false;

                    }
                }
            }


            if (turn && turn2)
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

        public override bool HasAnyMove()
        { 
            return base.HasAnyMove();
        }
    }
}
