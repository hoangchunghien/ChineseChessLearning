using Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    [TestFixture]
    class XQWLightEngineUnitTest
    {
        IEngine XQWLight = new Engine.Implements.XQWLightEngine(1);
        [Test]
        public void generateMoveUnitTest()
        {
            Engine.Domain.ChessMove moveAction = new Engine.Domain.ChessMove(0, 0, 1, 0);
            Engine.Domain.ChessMove pos = XQWLight.getBestMove(moveAction);
            //0010-->1232


            Assert.True(pos.Equals("0122"));
        }
    }
}
