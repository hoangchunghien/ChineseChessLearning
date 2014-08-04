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
        
        public static string DB_PATH = Path.Combine(ApplicationData.Current.LocalFolder.Path, "CClearning.sqlite");

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void btnLearningCourses_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/Tactics.xaml", UriKind.Relative));
        }

        private void btnPractice_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/BoardPractice.xaml", UriKind.Relative));
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