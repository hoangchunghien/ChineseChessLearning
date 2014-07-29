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

namespace IntelliUI.View
{
    public partial class Lessons : PhoneApplicationPage
    {
        ViewModelLesson viewModelLesson;
        public Lessons()
        {
            InitializeComponent();
            viewModelLesson = new ViewModelLesson(MainPage.DB_PATH, false);
            this.DataContext = viewModelLesson;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Book book = (Book)PhoneApplicationService.Current.State["selectedBook"];
            tblTitle.Text = book.Name;
            viewModelLesson.GetLessonOfBooks(new Lesson() { BookID = book.Id });
//            insert into Lesson (BookID, Name, Png, CountStarsPassed, CountStarsRequire, CountPractises, IsCompleted)
//values (2, 'Binh Phong Ma', '123456', 0, 5, 0, 0)

        }

        private void AllLessons_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var selected = AllLessons.SelectedValue as Lesson;
            PhoneApplicationService.Current.State["selectedLesson"] = selected;
            NavigationService.Navigate(new Uri("/View/Boardlearning.xaml", UriKind.Relative));
        }
    }
}