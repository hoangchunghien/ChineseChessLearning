using Intelli.Core.Services;
using Intelli.Core.Services.EventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Config
{
    public class GameConfig
    {
        public static Intelli.Core.Services.GameCoreService getGameService()
        {
            return new GameCoreEventHandler();
        }

    }
}
