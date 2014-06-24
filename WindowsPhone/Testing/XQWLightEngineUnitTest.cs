using Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Testing
{
    [TestFixture]
    class XQWLightEngineUnitTest
    {
        IEngine XQWLight = new Engine.Implements.XQWLightEngine(4);

        [Test]
        // TODO nameing testcase
        public void generateMoveUnitTest()
        {
            String test = "[Fen \"rnbakabnr/9/1c5c1/p1p1C1p1p/9/5C3/P1P1P1P1P/9/9/RNBAKABNR w\"]";

            XQWLight.setStartupBoard(test);
            String moveAction = "5545";
            String pos = XQWLight.getBestMove(moveAction);
           
        }

        [Test]
        public void generateMoveUnitTest2()
        {
            String test = "[Fen \"rnbakabnr/9/1c5c1/p1p1C1p1p/9/5C3/P1P1P1P1P/9/9/RNBAKABNR w\"]";

            XQWLight.setStartupBoard(test);
            String moveAction = "5535";
            String pos = XQWLight.getBestMove(moveAction);

        }
    }
}
