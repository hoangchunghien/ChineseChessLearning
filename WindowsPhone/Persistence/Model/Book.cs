using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Model
{
    public class Book: INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement, MaxLength(8)]
        public int Id { get; set; }

        [Indexed]
        public int TacticID { get; set; }
        public string Name { get; set; }
        public string AvatarPath { get; set; }
        public int CountLessons { get; set; } // One side cannon, two side cannon, two knights two side...// Need to changed

        private int _countLessonsPassed;
        public int CountLessonsPassed
        {
            //get;
            //set;
            get
            {
                return this._countLessonsPassed;
            }
            set
            {
                this._countLessonsPassed = value;
                this.RaisePropertyChanged("CountLessonsPassed");
            }
        }

        private int _countStarsPassed;
        public int CountStarsPassed
        {
            //get;
            //set;
            get
            {
                return this._countStarsPassed;
            }
            set
            {
                this._countStarsPassed = value;
                RaisePropertyChanged("CountStarsPassed");
            }
        }

        private int _countStarsRequire;
        public int CountStarsRequire
        {
            //get;
            //set;
            get
            {
                return this._countStarsRequire;
            }
            set
            {
                this._countStarsRequire = value;
                RaisePropertyChanged("CountStarsRequire");
            }
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            //get;
            //set;

            get
            {
                return this._isCompleted;
            }
            set
            {
                this._isCompleted = value;
                RaisePropertyChanged("IsCompleted");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Book GetCopy()
        {
            Book copy = (Book)this.MemberwiseClone();
            return copy;
        }
    }
}
