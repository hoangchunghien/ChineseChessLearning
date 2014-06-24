using Notification;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;


namespace Player
{
    public enum Color
    { 
        BLACK = 1,
        RED = 0,
        NONE = -1
    }

    public abstract class AbstractPlayer
    {
        Color color;
        String name;
        protected NotificationCenter nb = NotificationCenter.getInstance();

        public Color getColor()
        {
            return this.color;
        }

        public void setColor(Color color)
        {
            this.color = color;
        }

        public String getName()
        {
            return this.name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

    }

}
