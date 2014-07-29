using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Model
{
    public class User : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement, MaxLength(8)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Level { get; set; }
        public int Elo { get; set; }
        public int CountStars { get; set; }
        public int CountLessons { get; set; }
        public bool Active { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public User GetCopy()
        {
            User copy = (User)this.MemberwiseClone();
            return copy;
        }
    }
}
