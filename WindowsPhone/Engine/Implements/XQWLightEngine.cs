using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.XQWLight_AI;
using Engine.Domain;
using System.Text.RegularExpressions;

namespace Engine.Implements
{
    public interface IOpponentCallback
    {
        void OnOpponentMove(ChessMove move, MoveResult result);
    }
    public class XQWLightEngine : IEngine
    {
        public static readonly String NAME = "XQWLight";
        private Engine.XQWLight_AI.XQWLight_AI m_xqwLight;
        private int level;

        public XQWLightEngine(int level)
        {
            this.level = level;
            initEngine(level, null, 'w');
        }

        public XQWLightEngine(int level, sbyte[,] board, char start)
        {
            this.level = level;
            initEngine(level, board, start);
        }

        private void initEngine(int level, sbyte[,] board, char start) {
            
            this.m_xqwLight = new XQWLight_AI.XQWLight_AI();
            this.setXqlLevel(level);
            this.m_xqwLight.init_game(board, start);
        }

        static readonly int[] searchTimePerLevel = new int[] { 5, 10, 20, 30, 50 };
        private void setXqlLevel(int level)
        {
            this.m_xqwLight.init_engine(level);
            this.m_xqwLight.set_search_time(searchTimePerLevel[level - 1]);

        }
        public String getBestMove(String humanMoveAction)
        {
            this.m_xqwLight.on_human_move(humanMoveAction);
            return this.m_xqwLight.generate_move();
        }


        public void setStartupBoard(string fenBoard)
        {
            //String test = "[Fen \"rnbakabnr/9/1c5c1/p1p1p1p1p/9/9/P1P1P1P1P/1C5C1/9/RNBAKABNR w\"]";
            sbyte[,] stBoard = new sbyte[10, 9];

            string pattern = "Fen[\\s]{1}\"([a-z|A-Z|/|0-9]*)[\\s]{1}([w|b])";
            MatchCollection matches = Regex.Matches(fenBoard, pattern);
            Console.WriteLine(matches.Count);
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
                        {
                            stBoard[row, col++] = getPiece(' ');
                        }
                    }
                    else
                    {
                        stBoard[row, col] = getPiece(c);
                        col++;
                    }

                }
                row++;
            }

            initEngine(this.level, stBoard, start[0]);

        }

        private sbyte getPiece(char c)
        {
            //rnbakabnr/9/1c5c1/p1p1p1p1p/9/9/P1P1P1P1P/1C5C1/9/RNBAKABNR w\
            switch (c)
            {
                case 'r':
                    return 20;
                case 'n':
                    return 19;
                case 'b':
                    return 18;
                case 'a':
                    return 17;
                case 'k':
                    return 16;
                case 'c':
                    return 21;
                case 'p':
                    return 22;

                case 'R':
                    return 12;
                case 'N':
                    return 11;
                case 'B':
                    return 10;
                case 'A':
                    return 9;
                case 'K':
                    return 8;
                case 'C':
                    return 13;
                case 'P':
                    return 14;
                default: 
                    return 0;
            }

        }


        public void setLevel(int level)
        {
            throw new NotImplementedException();
        }
    }
}
