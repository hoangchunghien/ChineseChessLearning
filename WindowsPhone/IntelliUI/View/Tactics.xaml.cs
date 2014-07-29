using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Persistence.ViewModel;
using Persistence.Model;
using System.Collections.ObjectModel;
using System.Windows.Data;



namespace IntelliUI.View
{
    public partial class Tactics : PhoneApplicationPage
    {
        ViewModelBook viewModelBook;

        public Tactics()
        {
            InitializeComponent();
            viewModelBook = new ViewModelBook(MainPage.DB_PATH, false);
            this.DataContext = viewModelBook;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            viewModelBook.GetBooks();

            //AllOpenings.ItemsSource = from b in viewModelBook.GetBooks() where b.Id == 1 select b;
            //     Insert into Book (Name, TacticID, AvatarPath, CountLessonsPassed, CountLessons,
            //     CountStarsPassed, CountStarsRequire, IsCompleted)
            //values ("Test3", 1, "", 0, 20, 0, 5, 0)
        }

        private void AllMidGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = AllMidGames.SelectedItem as Book;
            PhoneApplicationService.Current.State["selectedBook"] = selected;
            NavigationService.Navigate(new Uri("/View/Lessons.xaml", UriKind.Relative));
        }

    }
}