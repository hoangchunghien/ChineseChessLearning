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

namespace IntelliUI
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Intelli.Core.Services.GameCoreService gameService = GameConfig.getGameService();

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            GameCreatedEvent createdEvent = gameService.createGame(new Intelli.Event.Game.CreateGameEvent());
            GameDetail detail = createdEvent.getDetail();
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
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
    }
}