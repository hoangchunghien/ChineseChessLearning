using Intelli.Core.EventHandler;
using Intelli.Core.EventHandler.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intelli.Core.EventHandler;

namespace Intelli.Config
{
    public class GameConfig
    {
        public static Core.EventHandler.GameService getGameService()
        {
            return new GameCoreEventHandler();
        }
    }
}
