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


        [Test]
        public void testAdvisor_0()
        {
            char[,] testCase = {
                {'r','k','m',' ',' ',' ',' ','C','P'},
                {' ',' ',' ','c','g',' ',' ',' ',' '},
                {' ',' ',' ','a','m',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ','R','G',' ',' ',' ',' '},
                {' ',' ',' ','A','K','A',' ','c','p'}
            };

            Board board = new Board();
            board.deserialize(testCase);
            Piece rAdvisor = board.getPieces()[9, 3];
            Piece bAdvisor = board.getPieces()[2, 3];
            Assert.AreEqual(0, rAdvisor.getValidNextPositions().Count);
            Assert.AreEqual(0, bAdvisor.getValidNextPositions().Count);

        }

        [Test]
        public void testAdvisor_1()
        {
            char[,] testCase = {
                {'r','k','m',' ',' ',' ',' ','C','P'},
                {' ',' ',' ','c',' ',' ',' ',' ',' '},
                {' ',' ',' ','a','m',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ','R','r',' ',' ',' ',' '},
                {' ',' ',' ','A','K','A',' ','c','p'}
            };

            Board board = new Board();
            board.deserialize(testCase);
            Piece rAdvisor = board.getPieces()[9, 3];
            Piece bAdvisor = board.getPieces()[2, 3];
            Assert.AreEqual(1, rAdvisor.getValidNextPositions().Count);
            Assert.AreEqual(1, bAdvisor.getValidNextPositions().Count);

            Assert.True(rAdvisor.getValidNextPositions().Contains(new Position(8, 4)));

            Assert.True(bAdvisor.getValidNextPositions().Contains(new Position(1, 4)));

        }

        [Test]
        public void testAdvisor_2()
        {
            char[,] testCase = {
                {'r','k','m',' ',' ',' ',' ','C','P'},
                {' ',' ',' ','c','a',' ',' ',' ',' '},
                {' ',' ',' ','g','m','c',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ','K',' ',' ',' '},
                {' ',' ',' ','R','A',' ',' ',' ',' '},
                {' ',' ',' ',' ','K','G',' ','c','p'}
            };

            Board board = new Board();
            board.deserialize(testCase);
            Piece rAdvisor = board.getPieces()[8, 4];
            Piece bAdvisor = board.getPieces()[1, 4];
            Assert.AreEqual(2, rAdvisor.getValidNextPositions().Count);
            Assert.AreEqual(2, bAdvisor.getValidNextPositions().Count);

            Assert.True(rAdvisor.getValidNextPositions().Contains(new Position(9, 3)));
            Assert.True(rAdvisor.getValidNextPositions().Contains(new Position(7, 3)));

            Assert.True(bAdvisor.getValidNextPositions().Contains(new Position(0, 3)));
            Assert.True(bAdvisor.getValidNextPositions().Contains(new Position(0, 5)));

        }

        [Test]
        public void testAdvisor_3()
        {
            char[,] testCase = {
                {'r','k','m',' ',' ',' ',' ','C','P'},
                {' ',' ',' ','c','a',' ',' ',' ',' '},
                {' ',' ',' ','g','m',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ','K',' ',' ',' '},
                {' ',' ',' ','R','A',' ',' ',' ',' '},
                {' ',' ',' ',' ','K',' ',' ','c','p'}
            };

            Board board = new Board();
            board.deserialize(testCase);
            Piece rAdvisor = board.getPieces()[8, 4];
            Piece bAdvisor = board.getPieces()[1, 4];
            Assert.AreEqual(3, rAdvisor.getValidNextPositions().Count);
            Assert.AreEqual(3, bAdvisor.getValidNextPositions().Count);

            Assert.True(rAdvisor.getValidNextPositions().Contains(new Position(9, 3)));
            Assert.True(rAdvisor.getValidNextPositions().Contains(new Position(7, 3)));
            Assert.True(rAdvisor.getValidNextPositions().Contains(new Position(9, 5)));

            Assert.True(bAdvisor.getValidNextPositions().Contains(new Position(0, 3)));
            Assert.True(bAdvisor.getValidNextPositions().Contains(new Position(0, 5)));
            Assert.True(bAdvisor.getValidNextPositions().Contains(new Position(2, 5)));

        }

        [Test]
        public void testAdvisor_4()
        {
            char[,] testCase = {
                {'r','k','m',' ',' ',' ',' ','C','P'},
                {' ',' ',' ','c','a',' ',' ',' ',' '},
                {' ',' ',' ',' ','m',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ','R','A',' ',' ',' ',' '},
                {' ',' ',' ',' ','K',' ',' ','c','p'}
            };

            Board board = new Board();
            board.deserialize(testCase);
            Piece rAdvisor = board.getPieces()[8, 4];
            Piece bAdvisor = board.getPieces()[1, 4];
            Assert.AreEqual(4, rAdvisor.getValidNextPositions().Count);
            Assert.AreEqual(4, bAdvisor.getValidNextPositions().Count);

            Assert.True(rAdvisor.getValidNextPositions().Contains(new Position(9, 3)));
            Assert.True(rAdvisor.getValidNextPositions().Contains(new Position(7, 3)));
            Assert.True(rAdvisor.getValidNextPositions().Contains(new Position(9, 5)));
            Assert.True(rAdvisor.getValidNextPositions().Contains(new Position(7, 5)));

            Assert.True(bAdvisor.getValidNextPositions().Contains(new Position(0, 3)));
            Assert.True(bAdvisor.getValidNextPositions().Contains(new Position(0, 5)));
            Assert.True(bAdvisor.getValidNextPositions().Contains(new Position(2, 5)));
            Assert.True(bAdvisor.getValidNextPositions().Contains(new Position(2, 3)));

        }

        [Test]
        public void testMinister_0()
        {
            char[,] testCase = {
                {' ','k',' ','a','g','a',' ','k',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {'m',' ',' ',' ','m',' ',' ',' ','m'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {'M',' ',' ',' ','M',' ',' ',' ','M'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ','K',' ','A','G','A',' ','K',' '}
            };

            Board board = new Board();
            board.deserialize(testCase);

            // 20, 70
            Piece rMinister = board.getPieces()[7, 0];
            Piece bMinister = board.getPieces()[2, 0];
            Piece rMinister1 = board.getPieces()[7, 8];
            Piece bMinister1 = board.getPieces()[2, 8];
            Assert.AreEqual(2, rMinister.getValidNextPositions().Count);
            Assert.AreEqual(2, bMinister.getValidNextPositions().Count);
            Assert.AreEqual(2, rMinister1.getValidNextPositions().Count);
            Assert.AreEqual(2, bMinister1.getValidNextPositions().Count);

            // 24, 74
            rMinister = board.getPieces()[7, 4];
            bMinister = board.getPieces()[2, 4];
            Assert.AreEqual(4, rMinister.getValidNextPositions().Count);
            Assert.AreEqual(4, bMinister.getValidNextPositions().Count);
        }

        [Test]
        public void testMinister_1()
        {
            char[,] testCase = {
                {' ','k','R','a','g','a','R','k',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {'m',' ',' ',' ','m',' ',' ',' ','m'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ','R',' ',' ',' ','R',' ',' '},

                {' ',' ','r',' ',' ',' ','r',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {'M',' ',' ',' ','M',' ',' ',' ','M'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ','K','r','A','G','A','r','K',' '}
            };

            Board board = new Board();
            board.deserialize(testCase);

            // 20, 70
            Piece rMinister = board.getPieces()[7, 0];
            Piece bMinister = board.getPieces()[2, 0];
            Piece rMinister1 = board.getPieces()[7, 8];
            Piece bMinister1 = board.getPieces()[2, 8];
            Assert.AreEqual(2, rMinister.getValidNextPositions().Count);
            Assert.AreEqual(2, bMinister.getValidNextPositions().Count);
            Assert.AreEqual(2, rMinister1.getValidNextPositions().Count);
            Assert.AreEqual(2, bMinister1.getValidNextPositions().Count);

            // 24, 74
            rMinister = board.getPieces()[7, 4];
            bMinister = board.getPieces()[2, 4];
            Assert.AreEqual(4, rMinister.getValidNextPositions().Count);
            Assert.AreEqual(4, bMinister.getValidNextPositions().Count);
        }

        [Test]
        public void testMinister_2()
        {
            char[,] testCase = {
                {' ','k','r','a','g','a','r','k',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {'m',' ',' ',' ','m',' ',' ',' ','m'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ','r',' ',' ',' ','r',' ',' '},

                {' ',' ','R',' ',' ',' ','R',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {'M',' ',' ',' ','M',' ',' ',' ','M'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ','K','R','A','G','A','R','K',' '}
            };

            Board board = new Board();
            board.deserialize(testCase);

            // 20, 70
            Piece rMinister = board.getPieces()[7, 0];
            Piece bMinister = board.getPieces()[2, 0];
            Piece rMinister1 = board.getPieces()[7, 8];
            Piece bMinister1 = board.getPieces()[2, 8];
            Assert.AreEqual(0, rMinister.getValidNextPositions().Count);
            Assert.AreEqual(0, bMinister.getValidNextPositions().Count);
            Assert.AreEqual(0, rMinister1.getValidNextPositions().Count);
            Assert.AreEqual(0, bMinister1.getValidNextPositions().Count);

            // 24, 74
            rMinister = board.getPieces()[7, 4];
            bMinister = board.getPieces()[2, 4];
            Assert.AreEqual(0, rMinister.getValidNextPositions().Count);
            Assert.AreEqual(0, bMinister.getValidNextPositions().Count);
        }

        [Test]
        public void testMinister_3()
        {
            char[,] testCase = {
                {' ','k',' ','a','g','a',' ','k',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {'m',' ',' ',' ','m',' ',' ',' ','m'},
                {' ','r',' ','r',' ','r',' ','r',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ','r',' ','r',' ','r',' ','r',' '},
                {'M',' ',' ',' ','M',' ',' ',' ','M'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ','K',' ','A','G','A',' ','K',' '}
            };

            Board board = new Board();
            board.deserialize(testCase);

            // 20, 70
            Piece rMinister = board.getPieces()[7, 0];
            Piece bMinister = board.getPieces()[2, 0];
            Piece rMinister1 = board.getPieces()[7, 8];
            Piece bMinister1 = board.getPieces()[2, 8];
            Assert.AreEqual(1, rMinister.getValidNextPositions().Count);
            Assert.AreEqual(1, bMinister.getValidNextPositions().Count);
            Assert.AreEqual(1, rMinister1.getValidNextPositions().Count);
            Assert.AreEqual(1, bMinister1.getValidNextPositions().Count);

            // 24, 74
            rMinister = board.getPieces()[7, 4];
            bMinister = board.getPieces()[2, 4];
            Assert.AreEqual(2, rMinister.getValidNextPositions().Count);
            Assert.AreEqual(2, bMinister.getValidNextPositions().Count);
        }

        [Test]
        public void testMinister_4()
        {
            char[,] testCase = {
                {' ','k',' ','a','g','a',' ','k',' '},
                {' ','r',' ','r',' ','r',' ','r',' '},
                {'m',' ',' ',' ','m',' ',' ',' ','m'},
                {' ','r',' ','r',' ','r',' ','r',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ','r',' ','r',' ','r',' ','r',' '},
                {'M',' ',' ',' ','M',' ',' ',' ','M'},
                {' ','r',' ','r',' ','r',' ','r',' '},
                {' ','K',' ','A','G','A',' ','K',' '}
            };

            Board board = new Board();
            board.deserialize(testCase);

            // 20, 70
            Piece rMinister = board.getPieces()[7, 0];
            Piece bMinister = board.getPieces()[2, 0];
            Piece rMinister1 = board.getPieces()[7, 8];
            Piece bMinister1 = board.getPieces()[2, 8];
            Assert.AreEqual(0, rMinister.getValidNextPositions().Count);
            Assert.AreEqual(0, bMinister.getValidNextPositions().Count);
            Assert.AreEqual(0, rMinister1.getValidNextPositions().Count);
            Assert.AreEqual(0, bMinister1.getValidNextPositions().Count);

            // 24, 74
            rMinister = board.getPieces()[7, 4];
            bMinister = board.getPieces()[2, 4];
            Assert.AreEqual(0, rMinister.getValidNextPositions().Count);
            Assert.AreEqual(0, bMinister.getValidNextPositions().Count);
        }

        [Test]
        public void testMinister_5()
        {
            char[,] testCase = {
                {' ','k','m','a','g','a','m','k',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {'m',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ','m',' ',' ',' ','m',' ',' '},

                {' ',' ','M',' ',' ',' ','M',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {'M',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ','K','M','A','G','A','M','K',' '}
            };

            Board board = new Board();
            board.deserialize(testCase);

            // 42, 52
            Piece rMinister = board.getPieces()[5, 2];
            Piece bMinister = board.getPieces()[4, 2];
            Piece rMinister1 = board.getPieces()[5, 6];
            Piece bMinister1 = board.getPieces()[4, 6];
            Assert.AreEqual(1, rMinister.getValidNextPositions().Count);
            Assert.AreEqual(1, bMinister.getValidNextPositions().Count);
            Assert.AreEqual(2, rMinister1.getValidNextPositions().Count);
            Assert.AreEqual(2, bMinister1.getValidNextPositions().Count);

            // 02, 06, 92, 96
            rMinister = board.getPieces()[9, 2];
            bMinister = board.getPieces()[0, 2];
            rMinister1 = board.getPieces()[9, 6];
            bMinister1 = board.getPieces()[0, 6];
            Assert.AreEqual(1, rMinister.getValidNextPositions().Count);
            Assert.AreEqual(1, bMinister.getValidNextPositions().Count);
            Assert.AreEqual(2, rMinister1.getValidNextPositions().Count);
            Assert.AreEqual(2, bMinister1.getValidNextPositions().Count);
        }

        [Test]
        public void testRook_0()
        {
            char[,] testCase = {
                {'r','k','m','a','g','a','m','k','r'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {'r',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ','m',' ','R',' ','m',' ',' '},

                {' ',' ','M',' ',' ',' ','M',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {'R','k','M','A','G','A','M','k','R'}
            };

            Board board = new Board();
            board.deserialize(testCase);

            // 42, 52
            Piece rRook = board.getPieces()[4, 4];
            Piece bRook = board.getPieces()[2, 0];
            Assert.AreEqual(12, rRook.getValidNextPositions().Count);
            Assert.AreEqual(16, bRook.getValidNextPositions().Count);

            Piece bRook1 = board.getPieces()[0, 0];
            Piece bRook2 = board.getPieces()[0, 8];
            Piece rRook1 = board.getPieces()[9, 0];
            Piece rRook2 = board.getPieces()[9, 8];
            Assert.AreEqual(1, bRook1.getValidNextPositions().Count);
            Assert.AreEqual(9, bRook2.getValidNextPositions().Count);
            Assert.AreEqual(8, rRook1.getValidNextPositions().Count);
            Assert.AreEqual(10, rRook2.getValidNextPositions().Count);

        }

        [Test]
        public void testCannon_0()
        {
            char[,] testCase = {
                {'r','k','m','a','g','a','m','k','r'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ','c',' ',' ','c',' ',' ',' ',' '},
                {'p',' ','p',' ','p',' ','p',' ','p'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {'P',' ','P',' ','P',' ','P',' ','P'},
                {' ',' ',' ',' ','C',' ',' ','C',' '},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {'R','K','M','A','G','A','M','K','R'}
            };

            Board board = new Board();
            board.deserialize(testCase);

            // 42, 52
            Piece rCannon = board.getPieces()[7, 4];
            Piece rCannon1 = board.getPieces()[7, 7];
            Piece bCannon = board.getPieces()[2, 1];
            Piece bCannon1 = board.getPieces()[2, 4];
            Assert.AreEqual(8, rCannon.getValidNextPositions().Count);
            Assert.AreEqual(10, rCannon1.getValidNextPositions().Count);
            Assert.AreEqual(10, bCannon.getValidNextPositions().Count);
            Assert.AreEqual(8, bCannon1.getValidNextPositions().Count);
        }

        [Test]
        public void testCannon_1()
        {
            char[,] testCase = {
                {'c','k','c','a','g','a','c','k','c'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {' ','r',' ',' ','r',' ',' ',' ',' '},
                {'R',' ','p',' ','p',' ','p',' ','R'},
                {' ',' ',' ',' ',' ',' ',' ',' ',' '},

                {' ',' ',' ',' ',' ',' ',' ',' ',' '},
                {'r',' ','P',' ','P',' ','P',' ','r'},
                {' ',' ',' ','C',' ',' ',' ',' ',' '},
                {' ',' ',' ','C',' ',' ',' ',' ',' '},
                {'C','K','C','C','G','A','C','K','C'}
            };

            Board board = new Board();
            board.deserialize(testCase);

            // 42, 52
            Piece rCannon = board.getPieces()[9, 0];
            Piece rCannon1 = board.getPieces()[9, 2];
            Piece rCannon2 = board.getPieces()[9, 6];
            Piece rCannon3 = board.getPieces()[9, 8];
            Piece bCannon = board.getPieces()[0, 0];
            Piece bCannon1 = board.getPieces()[0, 2];
            Piece bCannon2 = board.getPieces()[0, 6];
            Piece bCannon3 = board.getPieces()[0, 8];
            Assert.AreEqual(2, rCannon.getValidNextPositions().Count);
            Assert.AreEqual(3, rCannon1.getValidNextPositions().Count);
            Assert.AreEqual(3, rCannon2.getValidNextPositions().Count);
            Assert.AreEqual(2, rCannon3.getValidNextPositions().Count);
            Assert.AreEqual(2, bCannon.getValidNextPositions().Count);
            Assert.AreEqual(3, bCannon1.getValidNextPositions().Count);
            Assert.AreEqual(3, bCannon2.getValidNextPositions().Count);
            Assert.AreEqual(2, bCannon3.getValidNextPositions().Count);

            Piece endTest = board.getPieces()[9, 3];
            Piece endTest1 = board.getPieces()[8, 3];
            Piece endTest2 = board.getPieces()[7, 3];
            Assert.AreEqual(0, endTest.getValidNextPositions().Count);
            Assert.AreEqual(9, endTest1.getValidNextPositions().Count);
            Assert.AreEqual(14, endTest2.getValidNextPositions().Count);
        }
    }

}
