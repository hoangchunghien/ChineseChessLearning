using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Event.Game
{
    public class GameStateDetail
    {
        public static readonly int TURN_RED = 0;
        public static readonly int TURN_BLACK = 1;

        public static readonly int STATE_GAMEOVER = 0;
        public static readonly int STATE_PLAYING = 1;

        private char[,] board;

        private int turn;

        private int state;

        public GameStateDetail(char[,] board)
        {
            this.board = board;
        }

        public int getTurn { get { return this.turn; } }

        public int setTurn { set { this.turn = value; } }

        public int getState { get { return this.state; } }

        public int setState { set { this.state = value; } }

        public char[,] getBoard { get { return this.board; } }
    }
}
