using Configuration;
using Configuration.Engine;
using Engine.Implements;
using Engine.XQWLight_AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class EngineFactory
    {
        static Config config = Config.getInstance();

        public static IEngine getEngine()
        {
            LevelCfg level = config.Levels.levels.Single(l => l.selected);

            EngineCfg engine = config.Engines.engines.Single(e => e.name.Equals(level.engine));

            switch (engine.name)
            {
                case "Random":
                    return new RandomEngine();
                case "XQWLight":
                    return new XQWLightEngine(1);
                default:
                    return null;
            }
        }
    }
}
