using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Persistence.Model;
using Intelli.Config;
using Intelli.Event.Game;
using Notification;
using IntelliUI.Domain;
using System.Text.RegularExpressions;

namespace IntelliUI.View
{
    public partial class BoardLearning : PhoneApplicationPage
    {
        Lesson lesson;
        String defaultFen = "rnbakabnr/9/1c5c1/p1p1p1p1p/9/9/P1P1P1P1P/1C5C1/9/RNBAKABNR w";
        String[] fens;
        String[] subFens;
        int index = 0;// For general move
        int subIndex = 0;// For move with explaination
        public BoardLearning()
        {
            InitializeComponent();
            this.prepareLesson();
            this.newGame();
        }

        private void prepareLesson()
        {
            this.lesson = (Lesson)PhoneApplicationService.Current.State["selectedLesson"];
            this.fens = lesson.Png.Split('\n');
        }


        IntelliUI.Domain.Board board;
        private TouchItem[,] touchItems;
        private void newGame()
        {
            this.board = new Board();
            this.board.setBArr(board.getBArr());
            this.renderBoard(this.defaultFen, -1);
        }

        private void renderBoard(String fen, int index)
        {
            string pattern = "([a-z|A-Z|/|0-9]*)[\\s]{1}([w|b])";
            if (!Regex.IsMatch(fen, pattern) || !fen.Contains("/"))
            {
                MessageBox.Show(fen);
                this.txbExplain.Text = fen;
                return; // End of explaination or game
            }

            this.ContentBoard.Children.Clear();
            touchItems = new TouchItem[10, 9];

            MatchCollection matches = Regex.Matches(fen, pattern);
            Match match = matches[0];
            string boardFen = match.Groups[1].Value;
            string start = match.Groups[2].Value;

            string[] boardLines = boardFen.Split('/');

            int row = 0;
            foreach (string line in boardLines)
            {
                int col = 0;
                foreach (char c in line)
                {
                    if (Char.IsNumber(c))
                    {
                        int space = int.Parse(c.ToString());
                        for (int i = 0; i < space; i++)
                            col++;
                    }
                    else
                    {
                        TouchItem item = new TouchItem(c, "intella");
                        item.R = row;
                        item.C = col;
                        item.Width = item.Height = 48;
                        item.VerticalAlignment = 0;
                        item.HorizontalAlignment = 0;
                        item.Margin = new Thickness(5 + col * 53.1, 0 + row * 54, 0, 0);
                        item.Visibility = Visibility.Visible;
                        this.ContentBoard.Children.Add(item);

                        touchItems[row, col] = item;
                        touchItems[row, col].setVisibleForTouch(false);

                        col++;
                    }

                }
                row++;
            }

        }

        private void RemoveMasks()
        {
            foreach (TouchItem item in this.touchItems)
            {
                if (item.getP().Equals(' '))
                {
                    item.setVisibleForTouch(false);
                }
                else
                    item.clearMask();
            }
        }

        private void btnAuto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExplain_Click(object sender, RoutedEventArgs e)
        {
            this.txbExplain.Text = "";
            if (subIndex > subFens.Length - 1)
                subIndex = subFens.Length - 1;
            this.renderBoard(this.subFens[subIndex], subIndex++);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (index > fens.Length - 1)
                index = fens.Length - 1;
            if (index < 0)
                index = 0;
            this.renderBoard(this.fens[index], index--);
            if (index > 0 && this.fens[index][0] == '(') index--;
            if (index == -1)
                this.renderBoard(defaultFen, -1);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (index < 0) index = 0;
            if (index > fens.Length - 1)
                index = fens.Length - 1;

            this.renderBoard(this.fens[index], index++);
            if (index < fens.Length && this.fens[index][0] == '(')
            {
                this.txbExplain.Text = "Why? Click explain to know more!";
                btnExplain.Visibility = Visibility.Visible;

                this.subFens = this.fens[index].Substring(1, this.fens[index].Length - 1).Split('-');
                index++;// Jump over explaination row

            }
            else
            {
                this.txbExplain.Text = "";
                btnExplain.Visibility = Visibility.Collapsed;
                subIndex = 0;
            }
        }


    }
}