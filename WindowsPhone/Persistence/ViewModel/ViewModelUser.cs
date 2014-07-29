using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Persistence.Model;
using SQLite;

namespace Persistence.ViewModel
{
    public class ViewModelUser
    {
        public ObservableCollection<User> Users { get; set; }
        public string Path { get; set; }
        public bool State { get; set; }
        public ViewModelUser(String path, bool state)
        {
            this.Path = path;
            this.State = state;
        }
        public ObservableCollection<User> GetUsers()
        {
            ObservableCollection<User> rs;// = new ObservableCollection<User>();
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                //var r = db.Table<User>().ToList<User>();
                var r = db.Query<User>("SELECT * FROM User");
                rs = new ObservableCollection<User>(r);
            }
            return rs;
        }

        public User GetUser(User user)
        {
            User rs = new User();
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                rs = db.Query<User>("SELECT * FROM User WHERE ID=? OR USERNAME=?", user.Id, user.Username).FirstOrDefault();
            }
            return rs;
        }

        public int InsertUser(User user)
        {
            int rs = -1;
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                db.RunInTransaction(() =>
                {
                    rs = db.Insert(user);
                });
            }
            return rs;
        }

        public int UpdateUser(User user)
        {
            int rs = -1;
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                var existing = db.Query<User>("SELECT * FROM User WHERE ID=?", user.Id).FirstOrDefault();
                if (existing != null)
                {
                    existing = user.GetCopy();
                    db.RunInTransaction(() =>
                    {
                        rs = db.Update(existing);
                    });
                }
            }
            return rs;
        }

        public int DeleteUser(User user)
        {
            int rs = -1;
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                var existing = db.Query<User>("SELECT * FROM User WHERE ID=?", user.Id).FirstOrDefault();
                if (existing != null)
                {
                    db.RunInTransaction(() =>
                    {
                        rs = db.Delete(existing);
                    });
                }
            }
            return rs;
        }
    }
}
