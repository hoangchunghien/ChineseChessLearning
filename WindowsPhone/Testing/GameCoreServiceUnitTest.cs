using Intelli.Config;
using Intelli.Core.Game;
using Intelli.Core.Game.Player.States;
using Intelli.Core.Game.States;
using Intelli.Core.Services;
using Intelli.Core.Services.EventHandlers;
using Intelli.Event.Game;
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
            gameService.createGame(new Intelli.Event.Game.GameCreateEvent(true));
            GameStateMachine machine = ((GameCoreEventHandler)gameService).getGameMachine();
            gameService.requestPlayerJoinEvent(new IntelliCore.Event.Game.RequestPlayerJoinEvent(0));
            Assert.True(machine.getPlayers()[0].getCurrentState().GetType().Equals(typeof(PlayerJoinedState)));

            gameService.requestPlayerReadyEvent(new IntelliCore.Event.Game.RequestPlayerReadyEvent(0));
            Assert.True(machine.getPlayers()[0].getCurrentState().GetType().Equals(typeof(PlayerReadyState)));

            gameService.requestPlayerJoinEvent(new IntelliCore.Event.Game.RequestPlayerJoinEvent(0));
            gameService.requestPlayerJoinEvent(new IntelliCore.Event.Game.RequestPlayerJoinEvent(1));
            Assert.True(machine.getPlayers()[1].getCurrentState().GetType().Equals(typeof(PlayerJoinedState)));
            gameService.requestPlayerReadyEvent(new IntelliCore.Event.Game.RequestPlayerReadyEvent(1));

            int f = GameConfig.getPlayFirst();
            Assert.True(machine.getPlayers()[f].getCurrentState().GetType().Equals(typeof(PlayerPlayingState)));
            Assert.True(machine.getPlayers()[1 - f].getCurrentState().GetType().Equals(typeof(PlayerWaitingState)));

            //Assert.True(machine.getCurrentState().GetType().Equals(typeof(GamePlayingState)));


            //gameService.requestPlayerMoveEvent(new IntelliCore.Event.Game.RequestPlayerMoveEvent(f, new PositionDetail(9, 8), new PositionDetail(8, 8)));
            //Assert.True(machine.getPlayers()[f].getCurrentState().GetType().Equals(typeof(PlayerWaitingState)));
            //Assert.True(machine.getPlayers()[1-f].getCurrentState().GetType().Equals(typeof(PlayerPlayingState)));
            
        }
    }
}
