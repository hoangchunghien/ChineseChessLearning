using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intelli;
using NUnit.Framework;
using Intelli.Core.Game.Board;
using Intelli.Core.Game.Board.Pieces;
using System.Collections.Generic;

namespace Testing
{
    [TestFixture]
    public class PiecesUnitTest
    {
        [Test]
        public void testPawnGetValidNextPostitions()
        {
            List<char[,]> testList = new List<char[,]>();
            List<Position> inputPostion = new List<Position>();
            List<int> expectNumber = new List<int>();
            List<List<Position>> expectPositions = new List<List<Position>>();

            testList.Add( new char[,]
            {
                {'r','k','m','a','g','a','m','k','r'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ','p',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '}
            });
            inputPostion.Add(new Position(3, 1));
            expectNumber.Add(1);
            List<Position> expect1 = new List<Position>();
            expect1.Add(new Position(4, 1));
            expectPositions.Add(expect1);

            testList.Add(new char[,]
            {
                {'r','k','m','a','g','a','m','k','r'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ','p',' ',' ',' ',' ',' ',' ',' '},

                {' ','p',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '}
            });
            inputPostion.Add(new Position(5, 1));
            expectNumber.Add(2);
            List<Position> expect2 = new List<Position>();
            expect2.Add(new Position(6, 1));
            expect2.Add(new Position(5, 0));
            expect2.Add(new Position(5, 2));
            expectPositions.Add(expect2);

            int i = 0;
            foreach (var arr in testList)
            {
                
                Console.WriteLine("Test case " + i);
                Board board = new Board();
                board.deserialize(arr);
                int r = inputPostion.ToArray()[i].getRow();
                int c = inputPostion.ToArray()[i].getCol();
                Piece p = board.getPieces()[r, c];
                NUnit.Framework.Assert.AreEqual(expectNumber.ToArray()[i++], p.getValidNextPositions().Count);
                
            }
        }
    }
}
