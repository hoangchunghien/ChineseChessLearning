using Intelli.Core.Game;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    [TestFixture]
    public class GameStateMachineUnitTest
    {
        [Test]
        public void newGameMachineTest()
        {
            GameStateMachine machine = new GameStateMachine();
        }
    }
}
