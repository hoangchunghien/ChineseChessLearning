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
    public class Pieces
    {
        public int Row;// { get; set; }
        public int Col;// { get; set; }
        public String PieceName;// { get; set; }
        public int Color;// { get; set; } // Red=0 or Black=1, red move first, black move after in a new game
        public Boolean IsAlive;// { get; set; }
        public Boolean IsLock;// { get; set; }
        public UserControl UsrPiece;// { get; set; }
        public String Side;// { get; set; } // Example rook left or rook right; we need to process when eat piece, what rook we eat?
        public int CountMove;

        public Pieces()
        {
            //Row = 9;
            //Col = 8;
            PieceName = "";
            Color = 0;
            Side = "";
            IsAlive = false;
            IsLock = true;
            //UsrPiece = new SquareControl();
            CountMove = 0;
        }

        public Pieces(Pieces newPiece)
        {
            this.PieceName = newPiece.PieceName;
            this.Row = newPiece.Row;
            this.Col = newPiece.Col;
            this.Color = newPiece.Color;
            this.IsAlive = newPiece.IsAlive;
            this.UsrPiece = newPiece.UsrPiece;
            this.IsAlive = newPiece.IsLock;
            this.Side = newPiece.Side;
            this.CountMove = newPiece.CountMove;
        }

        public void InitializePiece(int row, int col, string pieceName, int color, bool isAlive, bool isBlock, string side, int countMove)
        {
            Row = row;
            Col = col;
            PieceName = pieceName;
            Color = color;
            Side = side;
            IsAlive = isAlive;
            IsLock = isBlock;
            CountMove = countMove;
            Board.Position[row, col].Row = row;
            Board.Position[row, col].Col = col;
            Board.Position[row, col].Name = pieceName;
            //Board.Position[row, col].Order = order;
            Board.Position[row, col].IsEmpty = false;
            //Add user control
            //usrPiece.MouseLeftButtonDown += new MouseButtonEventHandler(usrPiece_MouseLeftButtonDown);
            UsrPiece = new SquareControl(pieceName, color);
            //UsrPiece.MouseLeftButtonDown += new MouseButtonEventHandler(UsrPiece_MouseLeftButtonDown);
        }

        public virtual Boolean IsLegalMove(int i, int j)
        {
            return false;
        }

        public virtual Boolean IsKingSafe(int i, int j)
        {
            return false;
        }
        
        public virtual Boolean HasAnyMove()
        {
            return true;
        }

        public void DrawPiece()
        {
            this.UsrPiece = new SquareControl(this);
            // Event of piece here
            //...Call back to SquareControl
            UsrPiece.Width = 48;
            UsrPiece.Height = 48;
            UsrPiece.VerticalAlignment = 0;
            UsrPiece.HorizontalAlignment = 0;
            UsrPiece.Margin = new Thickness(5 + Col * 53.1, 0 + Row * 54, 0, 0);

            //
            Board.Position[Row, Col].Row = Row;
            Board.Position[Row, Col].Col = Col;
            Board.Position[Row, Col].Name = PieceName;
            Board.Position[Row, Col].Color = Color;
            Board.Position[Row, Col].Side = Side;
            Board.Position[Row, Col].IsEmpty = false;

        }
    }
}
