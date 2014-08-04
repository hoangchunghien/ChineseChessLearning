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
        ObservableCollection<Book> books;

        public Tactics()
        {
            InitializeComponent();
            viewModelBook = new ViewModelBook(MainPage.DB_PATH, false);
            this.DataContext = viewModelBook;
            books = viewModelBook.GetBooks();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // Opening
            var rs = from b in this.books where b.TacticID == 1 select b;
            AllOpenings.ItemsSource = rs.ToList();
            // Mid Games
            rs = from b in this.books where b.TacticID == 2 select b;
            AllMidGames.ItemsSource = rs.ToList();
            // End Games
            rs = from b in this.books where b.TacticID == 3 select b;
            AllEndGames.ItemsSource = rs.ToList();
            // Tournaments
            rs = from b in this.books where b.TacticID == 4 select b;
            AllTournaments.ItemsSource = rs.ToList();
            //     Insert into Book (Name, TacticID, AvatarPath, CountLessonsPassed, CountLessons,
            //     CountStarsPassed, CountStarsRequire, IsCompleted)
            //values ("Test3", 1, "", 0, 20, 0, 5, 0)
        }

        private void AllOpenings_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var selected = AllOpenings.SelectedItem as Book;
            PhoneApplicationService.Current.State["selectedBook"] = selected;
            NavigationService.Navigate(new Uri("/View/Lessons.xaml", UriKind.Relative));
        }

        private void AllMidGames_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void AllEndGames_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void AllTournaments_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

    }
}