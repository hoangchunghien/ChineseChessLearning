using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Persistence.Model;

namespace Persistence.Model
{
    public class HelperDB
    {
        public string Path { get; set; }
        public bool State { get; set; }
        public HelperDB(string path, bool state)
        {
            this.Path = path;
        }

        public void CreateAllTables()
        {
            using (var db = new SQLiteConnection(this.Path, this.State))
            {
                if (!ExistsTable<Book>())
                    db.CreateTable<Book>();
                if (!ExistsTable<Lesson>())
                    db.CreateTable<Lesson>();
                if (!ExistsTable<Tactic>())
                    db.CreateTable<Tactic>();
                if (!ExistsTable<User>())
                    db.CreateTable<User>();
            }
        }

        private bool ExistsTable<T>()
        {
            //            var connStr = new SQLiteConnectionString("contacts", false);

            using (var conn = new SQLiteConnection(this.Path, this.State))
            {
                var command = new SQLiteCommand(conn)
                {

                    CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='" + typeof(T).Name + "'"
                };

                return (command.ExecuteScalar<int>() > 0);
            }
        }
    }
}
