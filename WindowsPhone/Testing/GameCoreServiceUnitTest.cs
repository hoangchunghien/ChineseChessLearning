using Intelli.Core.Services;
using Intelli.Core.Services.EventHandlers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    [TestFixture]
    public class GameCoreServiceUnitTest
    {
        GameCoreService gameService = new GameCoreEventHandler();

        [Test]
        public void RequestPlayerJoinEventUnitTest()
        {
            gameService.createGame(new Intelli.Event.Game.CreateGameEvent());
            gameService.requestPlayerJoinEvent(new IntelliCore.Event.Game.RequestPlayerJoinEvent(0));
        }
    }
}
