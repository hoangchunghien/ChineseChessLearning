using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Persistence.Model;
using Intelli.Config;
using Intelli.Event.Game;
using Notification;
using IntelliUI.Domain;

namespace IntelliUI.View
{
    public partial class BoardLearning : PhoneApplicationPage
    {
        Lesson lesson;
        public BoardLearning()
        {
            InitializeComponent();
            getLesson();
            this.newGame();
        }

        private void getLesson()
        {
            this.lesson = (Lesson)PhoneApplicationService.Current.State["selectedLesson"];
        }
        
            // 1. GameCoreService <-- Config
        private Intelli.Core.Services.GameCoreService gameService = GameConfig.getGameService();
        
        // 2. Two players <-- Config
        //    Config.getFirstPlayer()
        //    Config.getSecondPlayer()

        // 3. Set gameService for two players
        //    player1.setGameService(*) ...


        IntelliUI.Domain.Board board;
        private TouchItem[,] touchItems;
        private void newGame()
        {
            GameCreatedEvent createdEvent = gameService.createGame(new Intelli.Event.Game.CreateGameEvent());
            Channel channel = gameService.getBroadcastChannel();
            //NotificationCenter.getInstance().subscribe(this, channel);
            GameDetail gameDetail = createdEvent.getDetail();
           
            // Player 0 join
            gameService.requestPlayerJoinEvent(new IntelliCore.Event.Game.RequestPlayerJoinEvent(0));
            gameService.requestPlayerReadyEvent(new IntelliCore.Event.Game.RequestPlayerReadyEvent(0));

            // Player 1 join
            gameService.requestPlayerJoinEvent(new IntelliCore.Event.Game.RequestPlayerJoinEvent(1));
            gameService.requestPlayerReadyEvent(new IntelliCore.Event.Game.RequestPlayerReadyEvent(1));

            this.board = new Board();
            this.board.setBArr(gameDetail.getBoarDetail().getPieces());
            this.renderBoard(this.board.getBArr());
        }

        private void renderBoard(char[,] bArr)
        {
            //this.ContentBoard.Children.Clear();
            touchItems = new TouchItem[10, 9];
            
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    TouchItem item = new TouchItem(bArr[i, j]);
                    item.R = i;
                    item.C = j;
                    item.Width = item.Height = 48;
                    item.VerticalAlignment = 0;
                    item.HorizontalAlignment = 0;
                    item.Margin = new Thickness(5 + j * 53.1, 0 + i * 54, 0, 0);
                    item.Visibility = System.Windows.Visibility.Visible;
                    this.ContentBoard.Children.Add(item);
                    item.MouseLeftButtonDown += item_MouseLeftButtonDown;

                    touchItems[i, j] = item;
                }
            }
        }

        private void RemoveMasks()
        {
            foreach (TouchItem item in this.touchItems)
            {
                if (item.getP().Equals(' '))
                {
                    item.setVisibleForTouch(false);
                }
                else
                    item.clearMask();
            }
        }

        void item_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TouchItem item = (TouchItem)sender;

            if (!item.isVisibleForTouch())
            {
                return;
            }

            PositionDetail pDetail = new PositionDetail(item.R, item.C);
            ValidMovesEvent validMovesEvent =  gameService.requestValidMoves(new RequestValidMovesEvent(pDetail, 1));

            if (validMovesEvent.isAccepted())
            {
                this.RemoveMasks();
                item.setVisibleForTouch(true);

                List<PositionDetail> pDetails = validMovesEvent.getValidMoves();

                foreach (PositionDetail dt in pDetails)
                {
                    touchItems[dt.getRow(), dt.getCol()].setVisibleForTouch(true);
                }

            }
            else
            {
                MessageBox.Show("Not your turn");
            }

        }

        private void btnAuto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExplain_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}