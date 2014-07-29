using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Model;
using SQLite;

namespace Persistence.ViewModel
{
    public class ViewModelTactic
    {
        public ObservableCollection<Tactic> Tactics { get; set; }
        public string Path { get; set; }
        public bool State { get; set; }
        public ViewModelTactic(String path, bool state)
        {
            this.Path = path;
            this.State = state;
        }
        public ObservableCollection<Tactic> GetTactics()
        {
            ObservableCollection<Tactic> rs;//= new ObservableCollection<Tactic>();
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                //var r = db.Table<Tactic>().ToList<Tactic>();
                var r = db.Query<Tactic>("SELECT * FROM Tactic");
                rs = new ObservableCollection<Tactic>(r);
            }

            return rs;
        }

        public Tactic GetTactic(Tactic tactic)
        {
            Tactic rs = new Tactic();
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                rs = db.Query<Tactic>("SELECT * FROM Tactic WHERE ID=?", tactic.Id).FirstOrDefault();
            }
            return rs;
        }

        public int InsertTactic(Tactic tactic)
        {
            int rs = -1;
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                db.RunInTransaction(() =>
                {
                    rs = db.Insert(tactic);
                });
            }
            return rs;
        }

        public int UpdateTactic(Tactic tactic)
        {
            int rs = -1;
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                var existing = db.Query<Tactic>("SELECT * FROM Tactic WHERE ID=?", tactic.Id).FirstOrDefault();
                if (existing != null)
                {
                    existing = tactic.GetCopy();
                    db.RunInTransaction(() =>
                    {
                        rs = db.Update(existing);
                    });
                }
            }
            return rs;
        }

        public int DeleteTactic(Tactic tactic)
        {
            int rs = -1;
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                var existing = db.Query<Tactic>("SELECT * FROM Tactic WHERE ID=?", tactic.Id).FirstOrDefault();
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
