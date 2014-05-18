using System;
using System.Collections.Generic;
using NUnit.Framework;
using Intelli.Core.Game;
using Intelli.Core.Game.Board;
using Intelli.Core.Game.Board.Events;

namespace Testing
{
    /// <summary>
    /// Summary description for TestBoard
    /// </summary>
    [TestFixture]
    public class TestBoardMachine
    {
        BoardStateMachine boardMachine;

        [SetUp]
        public void Init()
        {
            boardMachine = new BoardStateMachine();
        }

        [Test]
        public void TestBoardInitializingState()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void TestConsumeEvent()
        {
            IEvent e = new BoardMoveEvent(new Position(0, 0), new Position(1, 0));
            boardMachine.consumeEvent(e);
            Assert.AreEqual(BoardReadyState.NAME, boardMachine.getCurrentState().getStateName());

            IEvent invalidEvent = new BoardReadyEvent();
            boardMachine.consumeEvent(invalidEvent);
        }
    }
}
