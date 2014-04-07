using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace Intelli.GUI
{
    public partial class SquareControl : UserControl
    {
        public SquareControl()
        {
            InitializeComponent();
        }

        public SquareControl(String name, int color)
        {
            InitializeComponent();

            // Red pieces
            if (color == 1)
            {
                if (name == "king")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/rk.png", UriKind.Relative));
                if (name == "advisor")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/ra.png", UriKind.Relative));
                if (name == "minister")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/rb.png", UriKind.Relative));
                if (name == "rook")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/rr.png", UriKind.Relative));
                if (name == "cannon")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/rc.png", UriKind.Relative));
                if (name == "knight")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/rn.png", UriKind.Relative));
                if (name == "pawn")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/rp.png", UriKind.Relative));
            }

            // Black pieces
            if (color == -1)
            {
                if (name == "king")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/bk.png", UriKind.Relative));
                if (name == "advisor")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/ba.png", UriKind.Relative));
                if (name == "minister")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/bb.png", UriKind.Relative));
                if (name == "rook")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/br.png", UriKind.Relative));
                if (name == "cannon")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/bc.png", UriKind.Relative));
                if (name == "knight")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/bn.png", UriKind.Relative));
                if (name == "pawn")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/bp.png", UriKind.Relative));
            }
        }

        public Pieces Piece { get; set; }
        /// <summary>
        /// Creat a control with specify piece name and the turn of color
        /// </summary>
        /// <param name="piece.PieceName"></param>
        /// <param name="color"></param>
        public SquareControl(Pieces piece)
        {
            InitializeComponent();

            this.Piece = piece;

            // Red pieces
            if (piece.Color == 1)
            {
                if (piece.PieceName == "king")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/rk.png", UriKind.Relative));
                if (piece.PieceName == "advisor")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/ra.png", UriKind.Relative));
                if (piece.PieceName == "minister")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/rb.png", UriKind.Relative));
                if (piece.PieceName == "rook")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/rr.png", UriKind.Relative));
                if (piece.PieceName == "cannon")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/rc.png", UriKind.Relative));
                if (piece.PieceName == "knight")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/rn.png", UriKind.Relative));
                if (piece.PieceName == "pawn")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/rp.png", UriKind.Relative));
            }
            
            // Black pieces
            if (piece.Color == -1)
            {
                if (piece.PieceName == "king")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/bk.png", UriKind.Relative));
                if (piece.PieceName == "advisor")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/ba.png", UriKind.Relative));
                if (piece.PieceName == "minister")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/bb.png", UriKind.Relative));
                if (piece.PieceName == "rook")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/br.png", UriKind.Relative));
                if (piece.PieceName == "cannon")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/bc.png", UriKind.Relative));
                if (piece.PieceName == "knight")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/bn.png", UriKind.Relative));
                if (piece.PieceName == "pawn")
                    imgPiece.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/bp.png", UriKind.Relative));
            }

            // The mask hint for the piece
            //mask.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/mask.png"));
            MouseLeftButtonDown += new MouseButtonEventHandler(SquareControl_MouseLeftButtonDown);
            MouseLeave += new MouseEventHandler(SquareControl_MouseLeave);
            MouseEnter += new MouseEventHandler(SquareControl_MouseEnter);
        }

        void SquareControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Board.ResetUsrHint();
            if (this.Piece.IsAlive)
            {
                if (!this.Piece.IsLock)
                {
                    //imgMask.Source = new BitmapImage(new Uri("/Intelli;component/GUI/TMP/PNG/mask.png", UriKind.Relative));
                    Game.Marked = true;
                    Game.PieceMarked = new Pieces();
                    Game.PieceMarked = this.Piece;
                    Game.PieceMarked.UsrPiece = this;
                    
                    //SetPieceBeforeMarked(Game.PieceMarked);
                    for (int i = 0; i < 10; i++)
                        for (int j = 0; j < 9; j++)
                        {
                            if (i != this.Piece.Row || j != this.Piece.Col)
                                if (this.Piece.IsLegalMove(i, j))
                                {
                                    if (this.Piece.IsKingSafe(i, j))
                                        Board.Position[i, j].UsrHint.Visibility = Visibility.Visible;
                                }
                        }
                }

            }
        }

        private void SetPieceBeforeMarked(Pieces p)
        {
            //Board.Position[p.Row, p.Color].UsrHint.Visibility = Visibility.Visible;
            if (p.Color == -1)
            {
                Game.PieceBeforeMarkedBlack = new Pieces(p); // Save before piece
                //if (Game.PieceBeforeMarkedRed != null)
                //    Game.PieceBeforeMarkedRed.UsrPiece.Visibility = Visibility.Collapsed;
            }
            if (p.Color == 1)
            {
                Game.PieceBeforeMarkedRed = new Pieces(p); // Save before piece
                //if (Game.PieceBeforeMarkedBlack != null)
                //    Game.PieceBeforeMarkedBlack.UsrPiece.Visibility = Visibility.Collapsed;
            }
            //this.imgMask.Source = null;
        }

        void SquareControl_MouseLeave(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            //MessageBox.Show("You leave");
        }

        void SquareControl_MouseEnter(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            //MessageBox.Show("You enter");
        }
    }
}
