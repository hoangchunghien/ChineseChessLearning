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
    public class Board
    {
        /// <summary>
        /// A Node is a cross of two lines(+) which is contents any piece chess
        /// </summary>
        public struct Node
        {
            public int Row;
            public int Col;
            public String Side; // Example rook left or rook right; we need to process when eat piece, what rook we eat?
            public String Name;
            public int Color; // Red=1 or Black=-1, red move first, black move after in a new game
            public Boolean IsEmpty;
            public UserControl UsrHint; // The position which the piece can move
        }

        public static Grid GrdBoard;
        public Image ImgBoard;
        public static Node[,] Position = new Node[10, 9];
        public int Row { get; set; }
        public int Col { get; set; }
        // Move pieces declare

        // Contructor
        public Board(Grid girdBoard, MediaElement mda)
        {
            GrdBoard = girdBoard;
            Row = 10;
            Col = 9;
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    Position[i, j].Row = i;
                    Position[i, j].Col = j;
                    Position[i, j].Name = "";
                    Position[i, j].Side = "";
                    Position[i, j].IsEmpty = true;
                    Position[i, j].Color = 1;
                    Position[i, j].UsrHint = new SquareControl2();
                    Position[i, j].UsrHint.Width = 48;
                    Position[i, j].UsrHint.Height = 48;
                    Position[i, j].UsrHint.VerticalAlignment = 0;
                    Position[i, j].UsrHint.HorizontalAlignment = 0;
                    Position[i, j].UsrHint.Margin = new Thickness(5 + j * 53.1, 0 + i * 54, 0, 0);
                    Position[i, j].UsrHint.Visibility = Visibility.Collapsed;
                }
            }
        }

        static Board()
        {
            //GrdBoard = grdBoard;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Position[i, j].Row = i;
                    Position[i, j].Col = j;
                    Position[i, j].Name = "";
                    Position[i, j].Side = "";
                    Position[i, j].IsEmpty = true;
                    Position[i, j].Color = 1;
                    Position[i, j].UsrHint = new SquareControl2();
                    Position[i, j].UsrHint.Width = 48;
                    Position[i, j].UsrHint.Height = 48;
                    Position[i, j].UsrHint.VerticalAlignment = 0;
                    Position[i, j].UsrHint.HorizontalAlignment = 0;
                    Position[i, j].UsrHint.Margin = new Thickness(5 + j * 53.1, 0 + i * 54, 0, 0);
                    Position[i, j].UsrHint.Visibility = Visibility.Collapsed;
                    //Position[i, j].UsrHint.Visibility = Visibility.Visible;
                }
            }
        }

        public static void ResetBoard()
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 9; j++)
                {
                    Position[i, j].Row = i;
                    Position[i, j].Col = j;
                    Position[i, j].Name = "";
                    Position[i, j].Side = "";
                    Position[i, j].IsEmpty = true;
                    Position[i, j].Color = 1;
                    Position[i, j].UsrHint.Visibility = Visibility.Collapsed;
                    //Position[i, j].UsrHint.Visibility = Visibility.Visible;
                }
        }

        public static void RemoveUsrHint()
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 9; j++)
                {
                    GrdBoard.Children.Remove(Board.Position[i, j].UsrHint);
                    GrdBoard.Children.Add(Board.Position[i, j].UsrHint);
                }
        }

        public static void ResetUsrHint()
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 9; j++)
                {
                    Position[i, j].UsrHint.Visibility = Visibility.Collapsed;
                }

            //// Just for hint of color
            //if (Game.PieceBeforeMarkedBlack != null)
            //{
            //    Position[Game.PieceBeforeMarkedBlack.Row, Game.PieceBeforeMarkedBlack.Col].UsrHint.Visibility = Visibility.Visible;
            //    //if (Game.PieceBeforeMarkedRed != null)
            //    //    Position[Game.PieceBeforeMarkedRed.Row, Game.PieceBeforeMarkedRed.Col].UsrHint.Visibility = Visibility.Collapsed;
            //}
            //if (Game.PieceBeforeMarkedRed != null)
            //{
            //    Position[Game.PieceBeforeMarkedRed.Row, Game.PieceBeforeMarkedRed.Col].UsrHint.Visibility = Visibility.Visible;
            //    //if (Game.PieceBeforeMarkedBlack != null)
            //    //    Position[Game.PieceBeforeMarkedBlack.Row, Game.PieceBeforeMarkedBlack.Col].UsrHint.Visibility = Visibility.Collapsed;
            //}
        }

    }
}
