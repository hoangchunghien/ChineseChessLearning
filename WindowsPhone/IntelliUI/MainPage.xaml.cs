using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using IntelliUI.Resources;
using Intelli.Core;
using Intelli.Config;
using Intelli.Event;
using Intelli.Event.Game;
using Intelli.Core.Game;
using Intelli.Core.Services.EventHandlers;
using IntelliUI.Domain;
using System.Windows.Media.Imaging;
using IntelliUI.Factory;
using Notification;
using System.IO;
using Windows.Storage;
using Persistence.ViewModel;
using System.Collections.ObjectModel;
using Persistence.Model;

namespace IntelliUI
{
    public partial class MainPage : PhoneApplicationPage, INotifiable
    {
        
        
        // 1. GameCoreService <-- Config
        private Intelli.Core.Services.GameCoreService gameService = GameConfig.getGameService();
        
        // 2. Two players <-- Config
        //    Config.getFirstPlayer()
        //    Config.getSecondPlayer()

        // 3. Set gameService for two players
        //    player1.setGameService(*) ...


        IntelliUI.Domain.Board board;
        private TouchItem[,] touchItems;
        public static string DB_PATH = Path.Combine(ApplicationData.Current.LocalFolder.Path, "CClearning.sqlite");

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            //this.newGame();
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void newGame()
        {
            GameCreatedEvent createdEvent = gameService.createGame(new Intelli.Event.Game.CreateGameEvent());
            Channel channel = gameService.getBroadcastChannel();
            NotificationCenter.getInstance().subscribe(this, channel);
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

        private void btnLearningCourses_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/Tactics.xaml", UriKind.Relative));
        }

        private void btnPractice_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnChessProblems_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTournamentsGame_Click(object sender, RoutedEventArgs e)
        {

        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        public void receiveNotification(Channel channel, object notification)
        {
            throw new NotImplementedException();
        }

       
    }
}