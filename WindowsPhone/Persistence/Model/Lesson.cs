﻿using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Model
{
    public class Lesson: INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement, MaxLength(8)]
        public int Id { get; set; }

        [Indexed]
        public int BookID { get; set; }
        public string Name { get; set; }
        public string Png { get; set; }

        private int _countStarsPassed;
        public int CountStarsPassed
        {
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

        private int _countPractises;
        public int CountPractises
        {
            get
            {
                return this._countPractises;
            }
            set
            {
                this._countPractises = value;
                RaisePropertyChanged("CountPractises");
            }
        }

        private bool _isCompleted;
        public bool IsCompleted 
        {
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

        public Lesson GetCopy()
        {
            Lesson copy = (Lesson)this.MemberwiseClone();
            return copy;
        }
    }
}
