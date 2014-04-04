using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace Intelli.GUI
{
    public partial class SquareControl2 : UserControl
    {
        public static Pieces pieceMark; // For selected Piece
        public static Game game; // For current game

        public SquareControl2()
        {
            InitializeComponent();

            // Show the position that piece can move into
            imgMask.Source = new BitmapImage(new Uri("/Intelli;component/GUI/PNG/mask.png", UriKind.Relative));
            

            this.MouseEnter += new MouseEventHandler(SquareControl2_MouseEnter);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(SquareControl2_MouseLeftButtonDown);
            this.MouseLeave += new MouseEventHandler(SquareControl2_MouseLeave);
        }

        void SquareControl2_MouseLeave(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void SquareControl2_MouseEnter(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void SquareControl2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("incontrol");
          
        }
    }
}
