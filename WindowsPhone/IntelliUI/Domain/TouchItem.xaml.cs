using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using IntelliUI.Factory;

namespace IntelliUI.Domain
{
    public partial class TouchItem : UserControl
    {
        private char p;

        private Boolean visibleForTouch = true;
        public int R { get; set; }
        public int C { get; set; }

        public TouchItem()
        {
            InitializeComponent();
        }

        public TouchItem(char ch)
        {
            InitializeComponent();
            this.p = ch;
            //--rnbakabnr/9/1c5c1/p1p1p1p1p/9/9/P1P1P1P1P/1C5C1/9/RNBAKABNR w

            if (ch == 'G')
                imgPiece.Source = ImageFactory.getImage('G');
            if (ch == 'A')
                imgPiece.Source = ImageFactory.getImage('A');
            if (ch == 'M')
                imgPiece.Source = ImageFactory.getImage('M');
            if (ch == 'R')
                imgPiece.Source = ImageFactory.getImage('R');
            if (ch == 'C')
                imgPiece.Source = ImageFactory.getImage('C');
            if (ch == 'K')
                imgPiece.Source = ImageFactory.getImage('K');
            if (ch == 'P')
                imgPiece.Source = ImageFactory.getImage('P');
            if (ch == 'g')
                imgPiece.Source = ImageFactory.getImage('g');
            if (ch == 'a')
                imgPiece.Source = ImageFactory.getImage('a');
            if (ch == 'm')
                imgPiece.Source = ImageFactory.getImage('m');
            if (ch == 'r')
                imgPiece.Source = ImageFactory.getImage('r');
            if (ch == 'c')
                imgPiece.Source = ImageFactory.getImage('c');
            if (ch == 'k')
                imgPiece.Source = ImageFactory.getImage('k');
            if (ch == 'p')
                imgPiece.Source = ImageFactory.getImage('p');

        }

        public TouchItem(char ch, string type)
        {
            InitializeComponent();
            this.p = ch;
            //--rnbakabnr/9/1c5c1/p1p1p1p1p/9/9/P1P1P1P1P/1C5C1/9/RNBAKABNR w

            if (ch == 'K')
                imgPiece.Source = ImageFactory.getImage('G');
            if (ch == 'A')
                imgPiece.Source = ImageFactory.getImage('A');
            if (ch == 'B')
                imgPiece.Source = ImageFactory.getImage('M');
            if (ch == 'R')
                imgPiece.Source = ImageFactory.getImage('R');
            if (ch == 'C')
                imgPiece.Source = ImageFactory.getImage('C');
            if (ch == 'N')
                imgPiece.Source = ImageFactory.getImage('K');
            if (ch == 'P')
                imgPiece.Source = ImageFactory.getImage('P');
            if (ch == 'k')
                imgPiece.Source = ImageFactory.getImage('g');
            if (ch == 'a')
                imgPiece.Source = ImageFactory.getImage('a');
            if (ch == 'b')
                imgPiece.Source = ImageFactory.getImage('m');
            if (ch == 'r')
                imgPiece.Source = ImageFactory.getImage('r');
            if (ch == 'c')
                imgPiece.Source = ImageFactory.getImage('c');
            if (ch == 'n')
                imgPiece.Source = ImageFactory.getImage('k');
            if (ch == 'p')
                imgPiece.Source = ImageFactory.getImage('p');

        }

        public void clearMask()
        {
            this.imgMask.Source = ImageFactory.getImage(' ');
            
        }

        public void showMask()
        {
            this.imgMask.Source = ImageFactory.getImage('-');
            this.Visibility = System.Windows.Visibility.Visible;
        }

        public Boolean isVisibleForTouch()
        {
            return this.visibleForTouch;
        }

        public void setVisibleForTouch(bool flag)
        {
            this.visibleForTouch = flag;
            if (flag)
            {
                this.showMask();
            }
            else
                this.clearMask();
        }

        public char getP()
        {
            return this.p;
        }

    }
}
