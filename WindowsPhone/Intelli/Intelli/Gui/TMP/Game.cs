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
    public class Game
    {
        public static Board Board;// { get; set; }
        public static Player[] Players ;//{ get; set; }
        public static Boolean IsPlaying;// { get; set; }
        public static Grid GrdBoard;// { get; set; }
        public static int Turn = 1; // who turn?
        public static Pieces PieceMarked;
        public static Pieces PieceBeforeMarkedBlack; // For show usrhint between two move of an opponent
        public static Pieces PieceBeforeMarkedRed;
        public static Boolean Marked = false;
        public static String Winner = "";
        
        static Game()
        {
            // For player
            Turn = 1;
            Players = new Player[2];
            Players[0] = new Player(1, 1);
            Players[1] = new Player(-1, -1);
            IsPlaying = false;
        }

        public static void NewGame()
        {
            Turn = 1;
            RemoveAllPiecesFromBoard();
            Array.Resize<Player>(ref Players, 0);
            Array.Resize<Player>(ref Players, 2);
            Players[0] = new Player(1, 1);
            Players[1] = new Player(-1, -1);

            Board.ResetBoard();
            DrawPiecesForPlayer();
        }

        public Game(Grid grid, MediaElement mde)
        {
            GrdBoard = grid;
            Board = new Board(GrdBoard, mde);
            Players = new Player[2];
            Players[0] = new Player(0, 1);
            Players[1] = new Player(0, -1);
            Marked = false;
        }

        private static void DrawPiecesForPlayer()
        {
            // Player with red color
            Players[0].King.DrawPiece();
            Players[0].Advisors[0].DrawPiece();
            Players[0].Advisors[1].DrawPiece();
            Players[0].Ministers[0].DrawPiece();
            Players[0].Ministers[1].DrawPiece();
            Players[0].Rooks[0].DrawPiece();
            Players[0].Rooks[1].DrawPiece();
            Players[0].Cannons[0].DrawPiece();
            Players[0].Cannons[1].DrawPiece();
            Players[0].Knights[0].DrawPiece();
            Players[0].Knights[1].DrawPiece();
            Players[0].Pawns[0].DrawPiece();
            Players[0].Pawns[1].DrawPiece();
            Players[0].Pawns[2].DrawPiece();
            Players[0].Pawns[3].DrawPiece();
            Players[0].Pawns[4].DrawPiece();

            // Player with black color
            Players[1].King.DrawPiece();
            Players[1].Advisors[0].DrawPiece();
            Players[1].Advisors[1].DrawPiece();
            Players[1].Ministers[0].DrawPiece();
            Players[1].Ministers[1].DrawPiece();
            Players[1].Rooks[0].DrawPiece();
            Players[1].Rooks[1].DrawPiece();
            Players[1].Cannons[0].DrawPiece();
            Players[1].Cannons[1].DrawPiece();
            Players[1].Knights[0].DrawPiece();
            Players[1].Knights[1].DrawPiece();
            Players[1].Pawns[0].DrawPiece();
            Players[1].Pawns[1].DrawPiece();
            Players[1].Pawns[2].DrawPiece();
            Players[1].Pawns[3].DrawPiece();
            Players[1].Pawns[4].DrawPiece();
        }

        public static void RemoveAllPiecesFromBoard()
        {
            // Remove for player 0
            Players[0].King.UsrPiece.Visibility = Visibility.Collapsed;
            Players[0].Advisors[0].UsrPiece.Visibility = Visibility.Collapsed;
            Players[0].Advisors[1].UsrPiece.Visibility = Visibility.Collapsed;
            Players[0].Ministers[0].UsrPiece.Visibility = Visibility.Collapsed;
            Players[0].Ministers[1].UsrPiece.Visibility = Visibility.Collapsed;
            Players[0].Rooks[0].UsrPiece.Visibility = Visibility.Collapsed;
            Players[0].Rooks[1].UsrPiece.Visibility = Visibility.Collapsed;
            Players[0].Cannons[0].UsrPiece.Visibility = Visibility.Collapsed;
            Players[0].Cannons[1].UsrPiece.Visibility = Visibility.Collapsed;
            Players[0].Knights[0].UsrPiece.Visibility = Visibility.Collapsed;
            Players[0].Knights[1].UsrPiece.Visibility = Visibility.Collapsed;
            Players[0].Pawns[0].UsrPiece.Visibility = Visibility.Collapsed;
            Players[0].Pawns[1].UsrPiece.Visibility = Visibility.Collapsed;
            Players[0].Pawns[2].UsrPiece.Visibility = Visibility.Collapsed;
            Players[0].Pawns[3].UsrPiece.Visibility = Visibility.Collapsed;
            Players[0].Pawns[4].UsrPiece.Visibility = Visibility.Collapsed;

            // Remove for player 1
            Players[1].King.UsrPiece.Visibility = Visibility.Collapsed;
            Players[1].Advisors[0].UsrPiece.Visibility = Visibility.Collapsed;
            Players[1].Advisors[1].UsrPiece.Visibility = Visibility.Collapsed;
            Players[1].Ministers[0].UsrPiece.Visibility = Visibility.Collapsed;
            Players[1].Ministers[1].UsrPiece.Visibility = Visibility.Collapsed;
            Players[1].Rooks[0].UsrPiece.Visibility = Visibility.Collapsed;
            Players[1].Rooks[1].UsrPiece.Visibility = Visibility.Collapsed;
            Players[1].Cannons[0].UsrPiece.Visibility = Visibility.Collapsed;
            Players[1].Cannons[1].UsrPiece.Visibility = Visibility.Collapsed;
            Players[1].Knights[0].UsrPiece.Visibility = Visibility.Collapsed;
            Players[1].Knights[1].UsrPiece.Visibility = Visibility.Collapsed;
            Players[1].Pawns[0].UsrPiece.Visibility = Visibility.Collapsed;
            Players[1].Pawns[1].UsrPiece.Visibility = Visibility.Collapsed;
            Players[1].Pawns[2].UsrPiece.Visibility = Visibility.Collapsed;
            Players[1].Pawns[3].UsrPiece.Visibility = Visibility.Collapsed;
            Players[1].Pawns[4].UsrPiece.Visibility = Visibility.Collapsed;
        }

        public static void TakeTurn()
        {
            if (Turn == 1) Turn = -1;
            else
                Turn = 1;
            if (Game.Turn == 1)
            {
                Game.Players[0].King.IsLock = false;
                Game.Players[0].Advisors[0].IsLock = false;
                Game.Players[0].Advisors[1].IsLock = false;
                Game.Players[0].Ministers[0].IsLock = false;
                Game.Players[0].Ministers[1].IsLock = false;
                Game.Players[0].Rooks[0].IsLock = false;
                Game.Players[0].Rooks[1].IsLock = false;
                Game.Players[0].Cannons[0].IsLock = false;
                Game.Players[0].Cannons[1].IsLock = false;
                Game.Players[0].Knights[0].IsLock = false;
                Game.Players[0].Knights[1].IsLock = false;
                Game.Players[0].Pawns[0].IsLock = false;
                Game.Players[0].Pawns[1].IsLock = false;
                Game.Players[0].Pawns[2].IsLock = false;
                Game.Players[0].Pawns[3].IsLock = false;
                Game.Players[0].Pawns[4].IsLock = false;

                Game.Players[1].King.IsLock = true;
                Game.Players[1].Advisors[0].IsLock = true;
                Game.Players[1].Advisors[1].IsLock = true;
                Game.Players[1].Ministers[0].IsLock = true;
                Game.Players[1].Ministers[1].IsLock = true;
                Game.Players[1].Rooks[0].IsLock = true;
                Game.Players[1].Rooks[1].IsLock = true;
                Game.Players[1].Cannons[0].IsLock = true;
                Game.Players[1].Cannons[1].IsLock = true;
                Game.Players[1].Knights[0].IsLock = true;
                Game.Players[1].Knights[1].IsLock = true;
                Game.Players[1].Pawns[0].IsLock = true;
                Game.Players[1].Pawns[1].IsLock = true;
                Game.Players[1].Pawns[2].IsLock = true;
                Game.Players[1].Pawns[3].IsLock = true;
                Game.Players[1].Pawns[4].IsLock = true;
            }
            if (Game.Turn == -1)
            {
                Game.Players[0].King.IsLock = true;
                Game.Players[0].Advisors[0].IsLock = true;
                Game.Players[0].Advisors[1].IsLock = true;
                Game.Players[0].Ministers[0].IsLock = true;
                Game.Players[0].Ministers[1].IsLock = true;
                Game.Players[0].Rooks[0].IsLock = true;
                Game.Players[0].Rooks[1].IsLock = true;
                Game.Players[0].Cannons[0].IsLock = true;
                Game.Players[0].Cannons[1].IsLock = true;
                Game.Players[0].Knights[0].IsLock = true;
                Game.Players[0].Knights[1].IsLock = true;
                Game.Players[0].Pawns[0].IsLock = true;
                Game.Players[0].Pawns[1].IsLock = true;
                Game.Players[0].Pawns[2].IsLock = true;
                Game.Players[0].Pawns[3].IsLock = true;
                Game.Players[0].Pawns[4].IsLock = true;

                Game.Players[1].King.IsLock = false;
                Game.Players[1].Advisors[0].IsLock = false;
                Game.Players[1].Advisors[1].IsLock = false;
                Game.Players[1].Ministers[0].IsLock = false;
                Game.Players[1].Ministers[1].IsLock = false;
                Game.Players[1].Rooks[0].IsLock = false;
                Game.Players[1].Rooks[1].IsLock = false;
                Game.Players[1].Cannons[0].IsLock = false;
                Game.Players[1].Cannons[1].IsLock = false;
                Game.Players[1].Knights[0].IsLock = false;
                Game.Players[1].Knights[1].IsLock = false;
                Game.Players[1].Pawns[0].IsLock = false;
                Game.Players[1].Pawns[1].IsLock = false;
                Game.Players[1].Pawns[2].IsLock = false;
                Game.Players[1].Pawns[3].IsLock = false;
                Game.Players[1].Pawns[4].IsLock = false;
            }
        }

        public static void LockBoard(bool state)
        {
            Game.Players[0].King.IsLock = state;
            Game.Players[0].Advisors[0].IsLock = state;
            Game.Players[0].Advisors[1].IsLock = state;
            Game.Players[0].Ministers[0].IsLock = state;
            Game.Players[0].Ministers[1].IsLock = state;
            Game.Players[0].Rooks[0].IsLock = state;
            Game.Players[0].Rooks[1].IsLock = state;
            Game.Players[0].Cannons[0].IsLock = state;
            Game.Players[0].Cannons[1].IsLock = state;
            Game.Players[0].Knights[0].IsLock = state;
            Game.Players[0].Knights[1].IsLock = state;
            Game.Players[0].Pawns[0].IsLock = state;
            Game.Players[0].Pawns[1].IsLock = state;
            Game.Players[0].Pawns[2].IsLock = state;
            Game.Players[0].Pawns[3].IsLock = state;
            Game.Players[0].Pawns[4].IsLock = state;

            Game.Players[1].King.IsLock = state;
            Game.Players[1].Advisors[0].IsLock = state;
            Game.Players[1].Advisors[1].IsLock = state;
            Game.Players[1].Ministers[0].IsLock = state;
            Game.Players[1].Ministers[1].IsLock = state;
            Game.Players[1].Rooks[0].IsLock = state;
            Game.Players[1].Rooks[1].IsLock = state;
            Game.Players[1].Cannons[0].IsLock = state;
            Game.Players[1].Cannons[1].IsLock = state;
            Game.Players[1].Knights[0].IsLock = state;
            Game.Players[1].Knights[1].IsLock = state;
            Game.Players[1].Pawns[0].IsLock = state;
            Game.Players[1].Pawns[1].IsLock = state;
            Game.Players[1].Pawns[2].IsLock = state;
            Game.Players[1].Pawns[3].IsLock = state;
            Game.Players[1].Pawns[4].IsLock = state;
        }

        public static void SaveLogfile(Object sender, Pieces p)
        {
            
        }

        public static void MakeEat(Pieces p)
        {

        }

        public static void FreeNode(int row, int col)
        {
            Board.Position[row, col].IsEmpty = true;
            Board.Position[row, col].Name = "";
            Board.Position[row, col].Side = "";
            Board.Position[row, col].Color = 0; // The color is 1 or -1, so return the color is 0? 
        }

        public static void RollBackNode(Pieces p, int row, int col)
        {
            Board.Position[row, col].IsEmpty = false;
            Board.Position[row, col].Name = p.PieceName;
            Board.Position[row, col].Side = p.Side;
            Board.Position[row, col].Color = p.Color; // The color is 1 or -1, so return the color is 0? 
            p.Row = row;
            p.Col = col;
        }

        public static void MakeMove(Object sender, Pieces p, int row, int col)
        {
            //if (sender.GetType() == typeof(Intelli.GUI.MainPage))
            //{
            //    p.Row = row;
            //    p.Col = col;
            //    p.DrawPiece();
            //}
            if (sender.GetType() == typeof(SquareControl2))
            {
                Board.Position[row, col].IsEmpty = false;
                Board.Position[row, col].Color = Game.PieceMarked.Color;
                Board.Position[row, col].Side = Game.PieceMarked.Side;
                Board.Position[row, col].Name = Game.PieceMarked.PieceName;
                Game.PieceMarked.Row = row;
                Game.PieceMarked.Col = col;
                //Game.PieceMarked.UsrPiece = new SquareControl(p);
                Game.PieceMarked.UsrPiece.VerticalAlignment = 0;
                Game.PieceMarked.UsrPiece.HorizontalAlignment = 0;
                Game.PieceMarked.UsrPiece.Margin = new Thickness(5 + col * 53.1, 0 + row * 54, 0, 0);
                Game.PieceMarked.UsrPiece.Visibility = Visibility.Visible;
            }
        }

        #region CheckKingUnsafe, check end game, reset count move
        public static void CheckKingUnsafe()
        {
            //String _case = "NotMakeUnsafe";
            //if (Game.IsKingSafe(Game.Players[1].King) && 
            //    !Game.IsKingSafe(Game.Players[0].King))
            //    _case = "BlackMakeRedUnsafe";
            //if (!Game.IsKingSafe(Game.Players[1].King) &&
            //    Game.IsKingSafe(Game.Players[0].King))
            //    _case = "RedMakeBlackUnsafe";
            //if (!Game.IsKingSafe(Players[1].King) &&
            //    !Game.IsKingSafe(Players[0].King))
            //    _case = "NotMakeUnsafe";
            //switch (_case)
            //{
            //    case "NotMakeUnsafe":
            //        break;
            //    case "RedMakeBlackUnsafe":
            //        break;
            //    case "BlackMakeRedUnsafe":
            //        break;
            //    default:
            //        break;
            //}
        }

        /// <summary>
        /// If the opponent of param color make king of color unsafe?
        /// </summary>
        /// <param name="color"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static Boolean IsKingSafe(int color, int row, int col)
        {
            bool result = false;
            bool r1 = false,
                r2 = false, c1 = false, c2 = false, k1 = false, k2 = false,
                p1 = false, p2 = false, p3 = false, p4 = false, p5 = false;
            int p = 1; // Get opponent of param color
            if (color == -1)
                p = 0;

            if (Players[p].Rooks[0].IsAlive)
                if (Players[p].Rooks[0].IsLegalMove(row, col)) r1 = true;
            if (Players[p].Rooks[1].IsAlive)
                if (Players[p].Rooks[1].IsLegalMove(row, col)) r2 = true;
            if (Players[p].Cannons[0].IsAlive)
                if (Players[p].Cannons[0].IsLegalMove(row, col)) c1 = true;
            if (Players[p].Cannons[1].IsAlive)
                if (Players[p].Cannons[1].IsLegalMove(row, col)) c2 = true;
            if (Players[p].Knights[0].IsAlive)
                if (Players[p].Knights[0].IsLegalMove(row, col)) k1 = true;
            if (Players[p].Knights[1].IsAlive)
                if (Players[p].Knights[1].IsLegalMove(row, col)) k2 = true;
            // Unexpand position of pawns
            if (p == 1)
            {
                if (Players[p].Pawns[0].IsAlive && (Players[p].Pawns[0].Col >= 2 && Players[p].Pawns[0].Col <= 6 && Players[p].Pawns[0].Row <= 3))
                    if (Players[p].Pawns[0].IsLegalMove(row, col)) p1 = true;
                if (Players[p].Pawns[1].IsAlive && (Players[p].Pawns[1].Col >= 2 && Players[p].Pawns[1].Col <= 6 && Players[p].Pawns[1].Row <= 3))
                    if (Players[p].Pawns[1].IsLegalMove(row, col)) p2 = true;
                if (Players[p].Pawns[2].IsAlive && (Players[p].Pawns[2].Col >= 2 && Players[p].Pawns[2].Col <= 6 && Players[p].Pawns[2].Row <= 3))
                    if (Players[p].Pawns[2].IsLegalMove(row, col)) p3 = true;
                if (Players[p].Pawns[3].IsAlive && (Players[p].Pawns[3].Col >= 2 && Players[p].Pawns[3].Col <= 6 && Players[p].Pawns[3].Row <= 3))
                    if (Players[p].Pawns[3].IsLegalMove(row, col)) p4 = true;
                if (Players[p].Pawns[4].IsAlive && (Players[p].Pawns[4].Col >= 2 && Players[p].Pawns[4].Col <= 6 && Players[p].Pawns[4].Row <= 3))
                    if (Players[p].Pawns[4].IsLegalMove(row, col)) p5 = true;
            }
            if (p == 0)
            {
                if (Players[p].Pawns[0].IsAlive && (Players[p].Pawns[0].Col >= 2 && Players[p].Pawns[0].Col <= 6 && Players[p].Pawns[0].Row >= 6))
                    if (Players[p].Pawns[0].IsLegalMove(row, col)) p1 = true;
                if (Players[p].Pawns[1].IsAlive && (Players[p].Pawns[1].Col >= 2 && Players[p].Pawns[1].Col <= 6 && Players[p].Pawns[1].Row >= 6))
                    if (Players[p].Pawns[1].IsLegalMove(row, col)) p2 = true;
                if (Players[p].Pawns[2].IsAlive && (Players[p].Pawns[2].Col >= 2 && Players[p].Pawns[2].Col <= 6 && Players[p].Pawns[2].Row >= 6))
                    if (Players[p].Pawns[2].IsLegalMove(row, col)) p3 = true;
                if (Players[p].Pawns[3].IsAlive && (Players[p].Pawns[3].Col >= 2 && Players[p].Pawns[3].Col <= 6 && Players[p].Pawns[3].Row >= 6))
                    if (Players[p].Pawns[3].IsLegalMove(row, col)) p4 = true;
                if (Players[p].Pawns[4].IsAlive && (Players[p].Pawns[4].Col >= 2 && Players[p].Pawns[4].Col <= 6 && Players[p].Pawns[4].Row >= 6))
                    if (Players[p].Pawns[4].IsLegalMove(row, col)) p5 = true;
            }

            if (!r1 && !r2 && !c1 && !c2 && !k1 && !k2 && !p1 && !p2 && !p3 && !p4 && !p5)
                result = true;

            return result;
        }

        /// <summary>
        /// Try to find if has any move all of the piece in each color
        /// </summary>
        /// <returns></returns>
        public static Boolean IsEndGame()
        {
            bool endGame = true;
            ResetCountMove();
            // All of piece have countmove = 0;
            int p = 0; // To get player 0 or 1
            if (Game.Turn == -1)
                p = 1;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Players[p].King.IsAlive)
                        if (Players[p].King.IsLegalMove(i, j))
                            if (Players[p].King.IsKingSafe(i, j)) Players[p].King.CountMove++;
                    if (Players[p].Advisors[0].IsAlive)
                        if (Players[p].Advisors[0].IsLegalMove(i, j))
                            if (Players[p].Advisors[0].IsKingSafe(i, j)) Players[p].Advisors[0].CountMove++;
                    if (Players[p].Advisors[1].IsAlive)
                        if (Players[p].Advisors[1].IsLegalMove(i, j))
                            if (Players[p].Advisors[1].IsKingSafe(i, j)) Players[p].Advisors[1].CountMove++;
                    if (Players[p].Ministers[0].IsAlive)
                        if (Players[p].Ministers[0].IsLegalMove(i, j))
                            if (Players[p].Ministers[0].IsKingSafe(i, j)) Players[p].Ministers[0].CountMove++;
                    if (Players[p].Ministers[1].IsAlive)
                        if (Players[p].Ministers[1].IsLegalMove(i, j))
                            if (Players[p].Ministers[1].IsKingSafe(i, j)) Players[p].Ministers[1].CountMove++;
                    if (Players[p].Rooks[0].IsAlive)
                        if (Players[p].Rooks[0].IsLegalMove(i, j))
                            if (Players[p].Rooks[0].IsKingSafe(i, j)) Players[p].Rooks[0].CountMove++;
                    if (Players[p].Rooks[1].IsAlive)
                        if (Players[p].Rooks[1].IsLegalMove(i, j))
                            if (Players[p].Rooks[1].IsKingSafe(i, j)) Players[p].Rooks[1].CountMove++;
                    if (Players[p].Cannons[0].IsAlive)
                        if (Players[p].Cannons[0].IsLegalMove(i, j))
                            if (Players[p].Cannons[0].IsKingSafe(i, j)) Players[p].Cannons[0].CountMove++;
                    if (Players[p].Cannons[1].IsAlive)
                        if (Players[p].Cannons[1].IsLegalMove(i, j))
                            if (Players[p].Cannons[1].IsKingSafe(i, j)) Players[p].Cannons[1].CountMove++;
                    if (Players[p].Knights[0].IsAlive)
                        if (Players[p].Knights[0].IsLegalMove(i, j))
                            if (Players[p].Knights[0].IsKingSafe(i, j)) Players[p].Knights[0].CountMove++;
                    if (Players[p].Knights[1].IsAlive)
                        if (Players[p].Knights[1].IsLegalMove(i, j))
                            if (Players[p].Knights[1].IsKingSafe(i, j)) Players[p].Knights[1].CountMove++;
                    if (Players[p].Pawns[0].IsAlive)
                        if (Players[p].Pawns[0].IsLegalMove(i, j))
                            if (Players[p].Pawns[0].IsKingSafe(i, j)) Players[p].Pawns[0].CountMove++;
                    if (Players[p].Pawns[1].IsAlive)
                        if (Players[p].Pawns[1].IsLegalMove(i, j))
                            if (Players[p].Pawns[1].IsKingSafe(i, j)) Players[p].Pawns[1].CountMove++;
                    if (Players[p].Pawns[2].IsAlive)
                        if (Players[p].Pawns[2].IsLegalMove(i, j))
                            if (Players[p].Pawns[2].IsKingSafe(i, j)) Players[p].Pawns[2].CountMove++;
                    if (Players[p].Pawns[3].IsAlive)
                        if (Players[p].Pawns[3].IsLegalMove(i, j))
                            if (Players[p].Pawns[3].IsKingSafe(i, j)) Players[p].Pawns[3].CountMove++;
                    if (Players[p].Pawns[4].IsAlive)
                        if (Players[p].Pawns[4].IsLegalMove(i, j))
                            if (Players[p].Pawns[4].IsKingSafe(i, j)) Players[p].Pawns[4].CountMove++;

                    if (Players[p].King.CountMove == 1 || Players[p].Advisors[0].CountMove == 1 || Players[p].Advisors[1].CountMove == 1 ||
                        Players[p].Ministers[0].CountMove == 1 || Players[p].Ministers[1].CountMove == 1 ||
                        Players[p].Rooks[0].CountMove == 1 || Players[p].Rooks[1].CountMove == 1 ||
                        Players[p].Cannons[0].CountMove == 1 || Players[p].Cannons[1].CountMove == 1 ||
                        Players[p].Knights[0].CountMove == 1 || Players[p].Knights[1].CountMove == 1 ||
                        Players[p].Pawns[0].CountMove == 1 || Players[p].Pawns[1].CountMove == 1 ||
                        Players[p].Pawns[2].CountMove == 1 || Players[p].Pawns[3].CountMove == 1 || Players[p].Pawns[4].CountMove == 1)
                    {
                        endGame = false;
                        break;
                    }
                }
            }

            if (endGame)
            {
                Winner = (p == 0) ? "Black" : "Red";
            }
            return endGame;
        }

        private static void ResetCountMove()
        {
            Game.Players[0].King.CountMove = Game.Players[0].Advisors[0].CountMove = Game.Players[0].Advisors[1].CountMove =
                Game.Players[0].Ministers[0].CountMove = Game.Players[0].Ministers[1].CountMove =
                Game.Players[0].Rooks[0].CountMove = Game.Players[0].Rooks[1].CountMove =
                Game.Players[0].Cannons[0].CountMove = Game.Players[0].Cannons[1].CountMove =
                Game.Players[0].Knights[0].CountMove = Game.Players[0].Knights[1].CountMove =
                Game.Players[0].Pawns[0].CountMove = Game.Players[0].Pawns[1].CountMove =
                Game.Players[0].Pawns[2].CountMove = Game.Players[0].Pawns[3].CountMove = Game.Players[0].Pawns[4].CountMove =

                Game.Players[1].King.CountMove = Game.Players[1].Advisors[0].CountMove = Game.Players[1].Advisors[1].CountMove =
                Game.Players[1].Ministers[0].CountMove = Game.Players[1].Ministers[1].CountMove =
                Game.Players[1].Rooks[0].CountMove = Game.Players[1].Rooks[1].CountMove =
                Game.Players[1].Cannons[0].CountMove = Game.Players[1].Cannons[1].CountMove =
                Game.Players[1].Knights[0].CountMove = Game.Players[1].Knights[1].CountMove =
                Game.Players[1].Pawns[0].CountMove = Game.Players[1].Pawns[1].CountMove =
                Game.Players[1].Pawns[2].CountMove = Game.Players[1].Pawns[3].CountMove = Game.Players[1].Pawns[4].CountMove = 0;
        } 
        #endregion

        //[Fen "rnbakabnr/9/1c5c1/p1p1p1p1p/9/9/P1P1P1P1P/1C5C1/9/RNBAKABNR w"]
        //[Fen "rnbakabnr/9/1c5c1/p1p1p1p1p/9/9/P1P1P1P1P/1C5C1/9/RNBAKABNR w"]
        //[Fen "rnbakabnr/9/1c5c1/p1p1p1p1p/9/9/P1P1P1P1P/1C5C1/9/RNBAKABNR w"]
    }
}
