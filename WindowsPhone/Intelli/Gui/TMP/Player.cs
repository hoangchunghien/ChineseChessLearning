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

namespace Intelli.GUI
{
    public class Player
    {
        //public enum Player
        //{ 
        //    Human_Human,
        //    Human_Computer,
        //    Computer_Computer,
        //}

        public int Color { get; set; }
        public King King { get; set; }
        public Advisor[] Advisors { get; set; }
        public Minister[] Ministers { get; set; }
        public Rook[] Rooks { get; set; }
        public Cannon[] Cannons { get; set; }
        public Knight[] Knights { get; set; }
        public Pawn[] Pawns { get; set; }

        public Player(int color, int side) // side == 1 or -1
        {
            Color = color;
            King = new King();
            Advisors = new Advisor[2];
            Ministers = new Minister[2];
            Rooks = new Rook[2];
            Cannons = new Cannon[2];
            Knights = new Knight[2];
            Pawns = new Pawn[5];

            Advisors[0] = new Advisor();
            Advisors[1] = new Advisor();
            Ministers[0] = new Minister();
            Ministers[1] = new Minister();
            Rooks[0] = new Rook();
            Rooks[1] = new Rook();
            Cannons[0] = new Cannon();
            Cannons[1] = new Cannon();
            Knights[0] = new Knight();
            Knights[1] = new Knight();
            Pawns[0] = new Pawn();
            Pawns[1] = new Pawn();
            Pawns[2] = new Pawn();
            Pawns[3] = new Pawn();
            Pawns[4] = new Pawn();

            //InitializePlayer(color, side);
            InitializePlayer(color);
        }
        private void InitializePlayer(int color)
        {
            if (color == 1)
            {
                King.InitializePiece(0, 4, "king", color, true, false, "mid", 0);
                Advisors[0].InitializePiece(0, 3, "advisor", color, true, false, "left", 0);
                Advisors[1].InitializePiece(0, 5, "advisor", color, true, false, "right", 0);
                Ministers[0].InitializePiece(0, 2, "minister", color, true, false, "left", 0);
                Ministers[1].InitializePiece(0, 6, "minister", color, true, false, "right", 0);
                Rooks[0].InitializePiece(0, 0, "rook", color, true, false, "left", 0);
                Rooks[1].InitializePiece(0, 8, "rook", color, true, false, "right", 0);
                Cannons[0].InitializePiece(2, 1, "cannon", color, true, false, "left", 0);
                Cannons[1].InitializePiece(2, 7, "cannon", color, true, false, "right", 0);
                Knights[0].InitializePiece(0, 1, "knight", color, true, false, "left", 0);
                Knights[1].InitializePiece(0, 7, "knight", color, true, false, "right", 0);
                Pawns[0].InitializePiece(3, 0, "pawn", color, true, false, "0", 0);
                Pawns[1].InitializePiece(3, 2, "pawn", color, true, false, "1", 0);
                Pawns[2].InitializePiece(3, 4, "pawn", color, true, false, "2", 0);
                Pawns[3].InitializePiece(3, 6, "pawn", color, true, false, "3", 0);
                Pawns[4].InitializePiece(3, 8, "pawn", color, true, false, "4", 0);
            }
            else if (color == -1)
            {
                King.InitializePiece(9, 4, "king", color, true, true, "mid", 0);
                Advisors[0].InitializePiece(9, 3, "advisor", color, true, true, "left", 0);
                Advisors[1].InitializePiece(9, 5, "advisor", color, true, true, "right", 0);
                Ministers[0].InitializePiece(9, 2, "minister", color, true, true, "left", 0);
                Ministers[1].InitializePiece(9, 6, "minister", color, true, true, "right", 0);
                Rooks[0].InitializePiece(9, 0, "rook", color, true, true, "left", 0);
                Rooks[1].InitializePiece(9, 8, "rook", color, true, true, "right", 0);
                Cannons[0].InitializePiece(7, 1, "cannon", color, true, true, "left", 0);
                Cannons[1].InitializePiece(7, 7, "cannon", color, true, true, "right", 0);
                Knights[0].InitializePiece(9, 1, "knight", color, true, true, "left", 0);
                Knights[1].InitializePiece(9, 7, "knight", color, true, true, "right", 0);
                Pawns[0].InitializePiece(6, 0, "pawn", color, true, true, "0", 0);
                Pawns[1].InitializePiece(6, 2, "pawn", color, true, true, "1", 0);
                Pawns[2].InitializePiece(6, 4, "pawn", color, true, true, "2", 0);
                Pawns[3].InitializePiece(6, 6, "pawn", color, true, true, "3", 0);
                Pawns[4].InitializePiece(6, 8, "pawn", color, true, true, "4", 0);
            }
        }

        private void InitializePlayer(int color, int side)
        {
            Boolean isLock = true;
            if(color == 1) isLock = false;
            if (side == 1) //Side in fron of you
                side = 9;
            if (side == -1) // side overthere of you 
                side = 0;

            King.InitializePiece(side, 4, "king", color, true, isLock, "mid", 0);
            Advisors[0].InitializePiece(side, 3, "advisor", color, true, isLock, "left", 0);
            Advisors[1].InitializePiece(side, 5, "advisor", color, true, isLock, "right", 0);
            Ministers[0].InitializePiece(side, 2, "minister", color, true, isLock, "left", 0);
            Ministers[1].InitializePiece(side, 6, "minister", color, true, isLock, "right", 0);
            Rooks[0].InitializePiece(side, 8, "rook", color, true, isLock, "left", 0);
            Rooks[1].InitializePiece(side, 0, "rook", color, true, isLock, "right", 0);
            Cannons[0].InitializePiece((side == 9) ? 7 : 2, 1, "cannon", color, true, isLock, "left", 0);
            Cannons[0].InitializePiece((side == 9) ? 7 : 2, 7, "cannon", color, true, isLock, "right", 0);
            Knights[0].InitializePiece(side, 1, "knight", color, true, isLock, "left", 0);
            Knights[0].InitializePiece(side, 7, "knight", color, true, isLock, "right", 0);
            Pawns[0].InitializePiece((side == 9) ? 6 : 3, 0, "pawn", color, true, isLock, "0", 0);
            Pawns[1].InitializePiece((side == 9) ? 6 : 3, 2, "pawn", color, true, isLock, "1", 0);
            Pawns[2].InitializePiece((side == 9) ? 6 : 3, 4, "pawn", color, true, isLock, "2", 0);
            Pawns[3].InitializePiece((side == 9) ? 6 : 3, 6, "pawn", color, true, isLock, "3", 0);
            Pawns[4].InitializePiece((side == 9) ? 6 : 3, 8, "pawn", color, true, isLock, "4", 0);
        }
    }
}
