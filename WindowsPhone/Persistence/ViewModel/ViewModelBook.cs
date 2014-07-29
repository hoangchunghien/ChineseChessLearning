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
    public class ViewModelBook : INotifyPropertyChanged
    {
        ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books
        {
            get
            {
                return this._books;
            }
            set
            {
                this._books = value;
                RaisePropertyChanged("Books");
            }
        }

        public string Path { get; set; }
        public bool State { get; set; }
        public ViewModelBook(String path, bool state)
        {
            this.Path = path;
            this.State = state;
        }
        public ObservableCollection<Book> GetBooks()
        {
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                //var r = db.Table<Book>().ToList<Book>();
                var r = db.Query<Book>("SELECT * FROM BOOK");
                //var r = (from b in db.Table<Book>() select b);
                this.Books = new ObservableCollection<Book>(r);
            }
            return this.Books;
        }

        public Book GetBook(Book book)
        {
            Book rs = new Book();
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                rs = db.Query<Book>("SELECT * FROM BOOK WHERE ID=?", book.Id).Single();
            }
            return rs;
        }

        public ObservableCollection<Book> GetBooksOfTactic(Book book)
        {
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                var r = db.Query<Book>("SELECT * FROM BOOK WHERE TACTICID=?", book.TacticID);
                this.Books = new ObservableCollection<Book>(r);
            }
            return this.Books;
        }

        public int InsertBook(Book book)
        {
            int rs = -1;
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                db.RunInTransaction(() =>
                    {
                        rs = db.Insert(book);
                    });
            }
            return rs;
        }

        public int UpdateBook(Book book)
        {
            int rs = -1;
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                var existing = db.Query<Book>("SELECT * FROM BOOK WHERE ID=?", book.Id).FirstOrDefault();
                if (existing != null)
                {
                    existing = book.GetCopy();

                    db.RunInTransaction(() =>
                        {
                            rs = db.Update(existing);
                        });
                    this.RaisePropertyChanged("Books"); // Notify changed to update view
                }
            }
            return rs;
        }

        public int DeleteBook(Book book)
        {
            int rs = -1;
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                var existing = db.Query<Book>("SELECT * FROM BOOK WHERE ID=?", book.Id).FirstOrDefault();
                if (existing != null)
                {
                    db.RunInTransaction(() =>
                        {
                            rs = db.Delete(existing);
                        });
                    this.RaisePropertyChanged("Books"); // Notify changed to update view
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
