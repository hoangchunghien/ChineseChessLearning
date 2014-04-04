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
using Microsoft.Phone.Controls;
using Intelli.GUI;

namespace Intelli.GUI
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Board _board;
        private Pieces _pieces;
        private Game _game;


        public MainPage()
        {
            InitializeComponent();
            //_board = new Board(this.ContentBoard, new MediaElement());
            StartGame();
        }

        private void StartGame()
        {
            this.txtResult.Text = "";
            this.RemoveUsrControlFromBoard(ContentBoard);
            this.RemoveAllPiecesFromBoard(this.ContentBoard);
            Game.NewGame();
            this.AddAllPiecesToBoard(this.ContentBoard);

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 9; j++)
                {
                    this.ContentBoard.Children.Add(Board.Position[i, j].UsrHint);
                    Board.Position[i, j].UsrHint.MouseLeftButtonDown += new MouseButtonEventHandler(UsrHint_MouseLeftButtonDown);
                }
        }

        private void UsrHint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //throw new NotImplementedException();
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 9; j++)
                {
                    if (sender.Equals(Board.Position[i, j].UsrHint)) // sender as SquareControl2 is the user hint
                    {
                        if (Game.Marked)
                        {
                            switch (Board.Position[i, j].IsEmpty)
                            {
                                case true: // Make piece move to the empty position
                                    Game.Marked = false;
                                    // Game log
                                    Game.FreeNode(Game.PieceMarked.Row, Game.PieceMarked.Col);
                                    // Move piece to new position
                                    Game.MakeMove(sender, Game.PieceMarked, i, j);
                                    // Media element

                                    //Game.CheckKingUnsafe();

                                    Game.TakeTurn();

                                    EndgameNotify();
                                    Board.ResetUsrHint();

                                    break;
                                case false: // Make piece move to eat opponent's piece
                                    //Game.PieceMarked.UsrPiece = new SquareControl(Game.PieceMarked.PieceName, Game.PieceMarked.Color);
                                    int p = 0;
                                    if (Game.PieceMarked.Color == 1)
                                        p = 1; // ?et for opponent's color

                                    Pieces tmpPiece = new Pieces();
                                    // Position user click (the userControl hint) will be Piece of user selected before
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

                                    // Unselect piece
                                    Game.Marked = false;
                                    // Game log?

                                    // Eat piece by hide piece from the board
                                    DeleteOnePieceFromBoard(tmpPiece);
                                    Game.FreeNode(Game.PieceMarked.Row, Game.PieceMarked.Col);
                                    Game.MakeMove(sender, Game.PieceMarked, i, j);
                                    //Game.CheckKingUnsafe();

                                    Game.TakeTurn();

                                    EndgameNotify();

                                    Board.ResetUsrHint();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
        }

        private void EndgameNotify()
        {
            if (Game.IsEndGame())
                this.txtResult.Text = Game.Winner + " win!";
        }

        #region Board Control: Add, Remove, Delete UsrControl on Board
        private void AddAllPiecesToBoard(Grid grdBoard)
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 9; j++)
                {
                    if (Board.Position[i, j].Color == 1)
                    {
                        if (Board.Position[i, j].Name == "king")
                            grdBoard.Children.Add(Game.Players[0].King.UsrPiece);
                        else if (Board.Position[i, j].Name == "advisor")
                        {
                            if (Board.Position[i, j].Side == "left")
                                grdBoard.Children.Add(Game.Players[0].Advisors[0].UsrPiece);
                            if (Board.Position[i, j].Side == "right")
                                grdBoard.Children.Add(Game.Players[0].Advisors[1].UsrPiece);
                        }
                        else if (Board.Position[i, j].Name == "minister")
                        {
                            if (Board.Position[i, j].Side == "left")
                                grdBoard.Children.Add(Game.Players[0].Ministers[0].UsrPiece);
                            if (Board.Position[i, j].Side == "right")
                                grdBoard.Children.Add(Game.Players[0].Ministers[1].UsrPiece);
                        }
                        else if (Board.Position[i, j].Name == "rook")
                        {
                            if (Board.Position[i, j].Side == "left")
                                grdBoard.Children.Add(Game.Players[0].Rooks[0].UsrPiece);
                            if (Board.Position[i, j].Side == "right")
                                grdBoard.Children.Add(Game.Players[0].Rooks[1].UsrPiece);
                        }
                        else if (Board.Position[i, j].Name == "cannon")
                        {
                            if (Board.Position[i, j].Side == "left")
                                grdBoard.Children.Add(Game.Players[0].Cannons[0].UsrPiece);
                            if (Board.Position[i, j].Side == "right")
                                grdBoard.Children.Add(Game.Players[0].Cannons[1].UsrPiece);
                        }
                        else if (Board.Position[i, j].Name == "knight")
                        {
                            if (Board.Position[i, j].Side == "left")
                                grdBoard.Children.Add(Game.Players[0].Knights[0].UsrPiece);
                            if (Board.Position[i, j].Side == "right")
                                grdBoard.Children.Add(Game.Players[0].Knights[1].UsrPiece);
                        }
                        else if (Board.Position[i, j].Name == "pawn")
                        {
                            if (Board.Position[i, j].Side == "0")
                                grdBoard.Children.Add(Game.Players[0].Pawns[0].UsrPiece);
                            if (Board.Position[i, j].Side == "1")
                                grdBoard.Children.Add(Game.Players[0].Pawns[1].UsrPiece);
                            if (Board.Position[i, j].Side == "2")
                                grdBoard.Children.Add(Game.Players[0].Pawns[2].UsrPiece);
                            if (Board.Position[i, j].Side == "3")
                                grdBoard.Children.Add(Game.Players[0].Pawns[3].UsrPiece);
                            if (Board.Position[i, j].Side == "4")
                                grdBoard.Children.Add(Game.Players[0].Pawns[4].UsrPiece);
                        }
                    }
                    if (Board.Position[i, j].Color == -1)
                    {
                        if (Board.Position[i, j].Name == "king")
                            grdBoard.Children.Add(Game.Players[1].King.UsrPiece);
                        else if (Board.Position[i, j].Name == "advisor")
                        {
                            if (Board.Position[i, j].Side == "left")
                                grdBoard.Children.Add(Game.Players[1].Advisors[0].UsrPiece);
                            if (Board.Position[i, j].Side == "right")
                                grdBoard.Children.Add(Game.Players[1].Advisors[1].UsrPiece);
                        }
                        else if (Board.Position[i, j].Name == "minister")
                        {
                            if (Board.Position[i, j].Side == "left")
                                grdBoard.Children.Add(Game.Players[1].Ministers[0].UsrPiece);
                            if (Board.Position[i, j].Side == "right")
                                grdBoard.Children.Add(Game.Players[1].Ministers[1].UsrPiece);
                        }
                        else if (Board.Position[i, j].Name == "rook")
                        {
                            if (Board.Position[i, j].Side == "left")
                                grdBoard.Children.Add(Game.Players[1].Rooks[0].UsrPiece);
                            if (Board.Position[i, j].Side == "right")
                                grdBoard.Children.Add(Game.Players[1].Rooks[1].UsrPiece);
                        }
                        else if (Board.Position[i, j].Name == "cannon")
                        {
                            if (Board.Position[i, j].Side == "left")
                                grdBoard.Children.Add(Game.Players[1].Cannons[0].UsrPiece);
                            if (Board.Position[i, j].Side == "right")
                                grdBoard.Children.Add(Game.Players[1].Cannons[1].UsrPiece);
                        }
                        else if (Board.Position[i, j].Name == "knight")
                        {
                            if (Board.Position[i, j].Side == "left")
                                grdBoard.Children.Add(Game.Players[1].Knights[0].UsrPiece);
                            if (Board.Position[i, j].Side == "right")
                                grdBoard.Children.Add(Game.Players[1].Knights[1].UsrPiece);
                        }
                        else if (Board.Position[i, j].Name == "pawn")
                        {
                            if (Board.Position[i, j].Side == "0")
                                grdBoard.Children.Add(Game.Players[1].Pawns[0].UsrPiece);
                            if (Board.Position[i, j].Side == "1")
                                grdBoard.Children.Add(Game.Players[1].Pawns[1].UsrPiece);
                            if (Board.Position[i, j].Side == "2")
                                grdBoard.Children.Add(Game.Players[1].Pawns[2].UsrPiece);
                            if (Board.Position[i, j].Side == "3")
                                grdBoard.Children.Add(Game.Players[1].Pawns[3].UsrPiece);
                            if (Board.Position[i, j].Side == "4")
                                grdBoard.Children.Add(Game.Players[1].Pawns[4].UsrPiece);
                        }
                    }
                }
        }

        private void RemoveAllPiecesFromBoard(Grid grdBoard)
        {
            // Remove for player 0
            grdBoard.Children.Remove(Game.Players[0].King.UsrPiece);
            grdBoard.Children.Remove(Game.Players[0].Advisors[0].UsrPiece);
            grdBoard.Children.Remove(Game.Players[0].Advisors[1].UsrPiece);
            grdBoard.Children.Remove(Game.Players[0].Ministers[0].UsrPiece);
            grdBoard.Children.Remove(Game.Players[0].Ministers[1].UsrPiece);
            grdBoard.Children.Remove(Game.Players[0].Rooks[0].UsrPiece);
            grdBoard.Children.Remove(Game.Players[0].Rooks[1].UsrPiece);
            grdBoard.Children.Remove(Game.Players[0].Cannons[0].UsrPiece);
            grdBoard.Children.Remove(Game.Players[0].Cannons[1].UsrPiece);
            grdBoard.Children.Remove(Game.Players[0].Knights[0].UsrPiece);
            grdBoard.Children.Remove(Game.Players[0].Knights[1].UsrPiece);
            grdBoard.Children.Remove(Game.Players[0].Pawns[0].UsrPiece);
            grdBoard.Children.Remove(Game.Players[0].Pawns[1].UsrPiece);
            grdBoard.Children.Remove(Game.Players[0].Pawns[2].UsrPiece);
            grdBoard.Children.Remove(Game.Players[0].Pawns[3].UsrPiece);
            grdBoard.Children.Remove(Game.Players[0].Pawns[4].UsrPiece);

            // Remove for player 1
            grdBoard.Children.Remove(Game.Players[1].King.UsrPiece);
            grdBoard.Children.Remove(Game.Players[1].Advisors[0].UsrPiece);
            grdBoard.Children.Remove(Game.Players[1].Advisors[1].UsrPiece);
            grdBoard.Children.Remove(Game.Players[1].Ministers[0].UsrPiece);
            grdBoard.Children.Remove(Game.Players[1].Ministers[1].UsrPiece);
            grdBoard.Children.Remove(Game.Players[1].Rooks[0].UsrPiece);
            grdBoard.Children.Remove(Game.Players[1].Rooks[1].UsrPiece);
            grdBoard.Children.Remove(Game.Players[1].Cannons[0].UsrPiece);
            grdBoard.Children.Remove(Game.Players[1].Cannons[1].UsrPiece);
            grdBoard.Children.Remove(Game.Players[1].Knights[0].UsrPiece);
            grdBoard.Children.Remove(Game.Players[1].Knights[1].UsrPiece);
            grdBoard.Children.Remove(Game.Players[1].Pawns[0].UsrPiece);
            grdBoard.Children.Remove(Game.Players[1].Pawns[1].UsrPiece);
            grdBoard.Children.Remove(Game.Players[1].Pawns[2].UsrPiece);
            grdBoard.Children.Remove(Game.Players[1].Pawns[3].UsrPiece);
            grdBoard.Children.Remove(Game.Players[1].Pawns[4].UsrPiece);
        }

        private void RemoveUsrControlFromBoard(Grid grdBoard)
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 9; j++)
                    this.ContentBoard.Children.Remove(Board.Position[i, j].UsrHint);
        }

        private void DeleteOnePieceFromBoard(Pieces diePiece)
        {
            diePiece.IsAlive = false;
            diePiece.UsrPiece.Visibility = System.Windows.Visibility.Collapsed;
        } 
        #endregion

        #region Button Events
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            this.StartGame();
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Comming soon!");
        }

        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Comming soon!");
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Comming soon!");
        }
        #endregion

       
    }
}