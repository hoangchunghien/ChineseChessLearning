using System;
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
        public void testPawnWhenNotCrossedOpponentSide()
        {
            List<char[,]> testList = new List<char[,]>();
            List<Position> inputPostion = new List<Position>();
            List<int> expectNumber = new List<int>();
            List<List<Position>> expectPositions = new List<List<Position>>();

            testList.Add(new char[,]
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

        [Test]
        public void testPawnWhenCrossedOpponentSide_1()
        {
            char[,] testCase = {
                {'r','k','m','a','g','a','m','k','r'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ','P',' ',' ',' ',' ',' '},

                {' ',' ',' ','p',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '}
            };

            Board board = new Board();
            board.deserialize(testCase);
            Piece rPawn = board.getPieces()[4, 3];
            Piece bPawn = board.getPieces()[5, 3];
            Assert.AreEqual(3, rPawn.getValidNextPositions().Count);
            Assert.AreEqual(3, bPawn.getValidNextPositions().Count);

            Assert.True(rPawn.getValidNextPositions().Contains(new Position(4, 2)));
            Assert.True(rPawn.getValidNextPositions().Contains(new Position(3, 3)));
            Assert.True(rPawn.getValidNextPositions().Contains(new Position(4, 4)));

            Assert.True(bPawn.getValidNextPositions().Contains(new Position(5, 2)));
            Assert.True(bPawn.getValidNextPositions().Contains(new Position(5, 4)));
            Assert.True(bPawn.getValidNextPositions().Contains(new Position(6, 3)));
        }

        [Test]
        public void testPawnWhenCrossedOpponentSide_11()
        {
            char[,] testCase = {
                {'r','k','m','a','g','a','m','k','r'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ','r','P','K',' ',' ',' ',' '},

                {' ',' ','R','p','k',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '}
            };

            Board board = new Board();
            board.deserialize(testCase);
            Piece rPawn = board.getPieces()[4, 3];
            Piece bPawn = board.getPieces()[5, 3];
            Assert.AreEqual(2, rPawn.getValidNextPositions().Count);
            Assert.AreEqual(2, bPawn.getValidNextPositions().Count);

            Assert.True(rPawn.getValidNextPositions().Contains(new Position(4, 2)));
            Assert.True(rPawn.getValidNextPositions().Contains(new Position(3, 3)));

            Assert.True(bPawn.getValidNextPositions().Contains(new Position(5, 2)));
            Assert.True(bPawn.getValidNextPositions().Contains(new Position(6, 3)));
        }

        [Test]
        public void testPawnWhenCrossedOpponentSide_2()
        {
            char[,] testCase = {
                {'r','k','m','a','g','a','m','k','r'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {'P',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ','p'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '}
            };

            Board board = new Board();
            board.deserialize(testCase);
            Piece rPawn = board.getPieces()[4, 0];
            Piece bPawn = board.getPieces()[6, 8];
            Assert.AreEqual(2, rPawn.getValidNextPositions().Count);
            Assert.AreEqual(2, bPawn.getValidNextPositions().Count);

            Assert.True(rPawn.getValidNextPositions().Contains(new Position(3, 0)));
            Assert.True(rPawn.getValidNextPositions().Contains(new Position(4, 1)));

            Assert.True(bPawn.getValidNextPositions().Contains(new Position(7, 8)));
            Assert.True(bPawn.getValidNextPositions().Contains(new Position(6, 7)));
        }

        [Test]
        public void testPawnWhenCrossedOpponentSide_21()
        {
            char[,] testCase = {
                {'r','k','m','a','g','a','m','k',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {'R',' ',' ',' ',' ',' ',' ',' ',' '},
                {'P',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ','r','p'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '}
            };

            Board board = new Board();
            board.deserialize(testCase);
            Piece rPawn = board.getPieces()[4, 0];
            Piece bPawn = board.getPieces()[6, 8];
            Assert.AreEqual(1, rPawn.getValidNextPositions().Count);
            Assert.AreEqual(1, bPawn.getValidNextPositions().Count);

            Assert.True(rPawn.getValidNextPositions().Contains(new Position(4, 1)));

            Assert.True(bPawn.getValidNextPositions().Contains(new Position(7, 8)));
        }

        [Test]
        public void testPawnWhenCrossedOpponentSide_3()
        {
            char[,] testCase = {
                {'r','k','m','a','g','a','m','k','P'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ','p'}
            };

            Board board = new Board();
            board.deserialize(testCase);
            Piece rPawn = board.getPieces()[0, 8];
            Piece bPawn = board.getPieces()[9, 8];
            Assert.AreEqual(1, rPawn.getValidNextPositions().Count);
            Assert.AreEqual(1, bPawn.getValidNextPositions().Count);

            Assert.True(rPawn.getValidNextPositions().Contains(new Position(0, 7)));

            Assert.True(bPawn.getValidNextPositions().Contains(new Position(9, 7)));
        }

        [Test]
        public void testPawnWhenCrossedOpponentSide_31()
        {
            char[,] testCase = {
                {'r','k','m','a','g','a','m','C','P'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ','c','p'}
            };

            Board board = new Board();
            board.deserialize(testCase);
            Piece rPawn = board.getPieces()[0, 8];
            Piece bPawn = board.getPieces()[9, 8];
            Assert.AreEqual(0, rPawn.getValidNextPositions().Count);
            Assert.AreEqual(0, bPawn.getValidNextPositions().Count);

        }

        [Test]
        public void testMaster_3()
        {
            char[,] testCase = {
                {'r','k','m',' ','g',' ','m','C','P'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ','G',' ',' ','c','p'}
            };

            Board board = new Board();
            board.deserialize(testCase);
            Piece rMaster = board.getPieces()[9, 4];
            Piece bMaster = board.getPieces()[0, 4];
            Assert.AreEqual(3, rMaster.getValidNextPositions().Count);
            Assert.AreEqual(3, bMaster.getValidNextPositions().Count);

            Assert.True(rMaster.getValidNextPositions().Contains(new Position(9, 5)));
            Assert.True(rMaster.getValidNextPositions().Contains(new Position(9, 3)));
            Assert.True(rMaster.getValidNextPositions().Contains(new Position(8, 4)));

            Assert.True(bMaster.getValidNextPositions().Contains(new Position(0, 5)));
            Assert.True(bMaster.getValidNextPositions().Contains(new Position(0, 3)));
            Assert.True(bMaster.getValidNextPositions().Contains(new Position(1, 4)));
        }

        [Test]
        public void testMaster_4()
        {
            char[,] testCase = {
                {'r','k','m',' ',' ',' ','m','C','P'},
                {' ',' ',' ',' ','g',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ','G',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ','c','p'}
            };

            Board board = new Board();
            board.deserialize(testCase);
            Piece rMaster = board.getPieces()[8, 4];
            Piece bMaster = board.getPieces()[1, 4];
            Assert.AreEqual(4, rMaster.getValidNextPositions().Count);
            Assert.AreEqual(4, bMaster.getValidNextPositions().Count);

            Assert.True(rMaster.getValidNextPositions().Contains(new Position(9, 4)));
            Assert.True(rMaster.getValidNextPositions().Contains(new Position(8, 3)));
            Assert.True(rMaster.getValidNextPositions().Contains(new Position(8, 5)));
            Assert.True(rMaster.getValidNextPositions().Contains(new Position(7, 4)));

            Assert.True(bMaster.getValidNextPositions().Contains(new Position(1, 5)));
            Assert.True(bMaster.getValidNextPositions().Contains(new Position(1, 3)));
            Assert.True(bMaster.getValidNextPositions().Contains(new Position(0, 4)));
            Assert.True(bMaster.getValidNextPositions().Contains(new Position(2, 4)));
        }

        [Test]
        public void testMaster_2()
        {
            char[,] testCase = {
                {'r','k','m',' ',' ',' ','m','C','P'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ','g',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ','G',' ',' ',' ','c','p'}
            };

            Board board = new Board();
            board.deserialize(testCase);
            Piece rMaster = board.getPieces()[9, 3];
            Piece bMaster = board.getPieces()[2, 3];
            Assert.AreEqual(2, rMaster.getValidNextPositions().Count);
            Assert.AreEqual(2, bMaster.getValidNextPositions().Count);

            Assert.True(rMaster.getValidNextPositions().Contains(new Position(9, 4)));
            Assert.True(rMaster.getValidNextPositions().Contains(new Position(8, 3)));

            Assert.True(bMaster.getValidNextPositions().Contains(new Position(1, 3)));
            Assert.True(bMaster.getValidNextPositions().Contains(new Position(2, 4)));
        }

        [Test]
        public void testMaster_1()
        {
            char[,] testCase = {
                {'r','k','m',' ',' ',' ',' ','C','P'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ','g','m',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ','R',' ',' ',' ',' ',' '},
                {' ',' ',' ','G',' ',' ',' ','c','p'}
            };

            Board board = new Board();
            board.deserialize(testCase);
            Piece rMaster = board.getPieces()[9, 3];
            Piece bMaster = board.getPieces()[2, 3];
            Assert.AreEqual(1, rMaster.getValidNextPositions().Count);
            Assert.AreEqual(1, bMaster.getValidNextPositions().Count);

            Assert.True(rMaster.getValidNextPositions().Contains(new Position(9, 4)));

            Assert.True(bMaster.getValidNextPositions().Contains(new Position(1, 3)));
        }

        [Test]
        public void testMaster_0()
        {
            char[,] testCase = {
                {'r','k','m',' ',' ',' ',' ','C','P'},
                {' ',' ',' ','c',' ',' ',' ',' ',' '},
                {' ',' ',' ','g','m',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ','R',' ',' ',' ',' ',' '},
                {' ',' ',' ','G','K',' ',' ','c','p'}
            };

            Board board = new Board();
            board.deserialize(testCase);
            Piece rMaster = board.getPieces()[9, 3];
            Piece bMaster = board.getPieces()[2, 3];
            Assert.AreEqual(0, rMaster.getValidNextPositions().Count);
            Assert.AreEqual(0, bMaster.getValidNextPositions().Count);

        }


    }

}
