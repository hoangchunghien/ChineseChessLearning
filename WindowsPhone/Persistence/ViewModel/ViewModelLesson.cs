using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Persistence.Model;
using SQLite;
using System.ComponentModel;

namespace Persistence.ViewModel
{
    public class ViewModelLesson : INotifyPropertyChanged
    {
        private ObservableCollection<Lesson> _lessons;
        public ObservableCollection<Lesson> Lessons 
        {
            get
            {
                return this._lessons;
            }
            set
            {
                this._lessons = value;
                this.RaisePropertyChanged("Lessons");
            }
        }

        public string Path { get; set; }
        public bool State { get; set; }
        public ViewModelLesson(String path, bool state)
        {
            this.Path = path;
            this.State = state;
        }

        public ObservableCollection<Lesson> GetLessons()
        {
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                //var r = db.Table<Lesson>().ToList<Lesson>();
                var r = db.Query<Lesson>("SELECT * FROM Lesson");
                this.Lessons = new ObservableCollection<Lesson>(r);
            }

            return this.Lessons;
        }

        public Lesson GetLesson(Lesson lesson)
        {
            Lesson rs = new Lesson();
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                rs = db.Query<Lesson>("SELECT * FROM Lesson WHERE ID=?", lesson.Id).FirstOrDefault();
            }
            return rs;
        }

        public ObservableCollection<Lesson> GetLessonOfBooks(Lesson lesson)
        {
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                var r = db.Query<Lesson>("SELECT * FROM Lesson WHERE BOOKID=?", lesson.BookID);
                this.Lessons = new ObservableCollection<Lesson>(r);
            }

            return this.Lessons;
        }

        public int InsertLesson(Lesson lesson)
        {
            int rs = -1;
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                db.RunInTransaction(() =>
                {
                    rs = db.Insert(lesson);
                });

                this.RaisePropertyChanged("Lessons");
            }

            return rs;
        }

        public int UpdateLesson(Lesson lesson)
        {
            int rs = -1;
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                var existing = db.Query<Lesson>("SELECT * FROM Lesson WHERE ID=?", lesson.Id).FirstOrDefault();
                if (existing != null)
                {
                    existing = lesson.GetCopy();
                    db.RunInTransaction(() =>
                    {
                        rs = db.Update(existing);
                    });

                    this.RaisePropertyChanged("Lessons");
                }
            }
            return rs;
        }

        public int DeleteLesson(Lesson lesson)
        {
            int rs = -1;
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                var existing = db.Query<Lesson>("SELECT * FROM Lesson WHERE ID=?", lesson.Id).FirstOrDefault();
                if (existing != null)
                {
                    db.RunInTransaction(() =>
                    {
                        rs = db.Delete(existing);
                    });

                    this.RaisePropertyChanged("Lessons");
                }
            }

            return rs;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
