using Configuration.Engine;
using Configuration.Player;
using Configuration.Sound;
using Intelli.Core.Services;
using Intelli.Core.Services.EventHandlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Configuration
{
    [XmlRoot(ElementName = "Configuration")]
    public class Config
    {
        public static readonly string CONFIG_PATH = "config.xml";

        private static Config instanceSingleton = null;

        public static Config getInstance()
        {
            if (instanceSingleton == null)
            {
                instanceSingleton = Deserialize(CONFIG_PATH);
            }
            return instanceSingleton;
        }

        [XmlElement(ElementName = "Engines")]
        public EnginesCfg Engines { get; set; }

        [XmlElement(ElementName = "Levels")]
        public LevelsCfg Levels { get; set; }

        [XmlElement(ElementName="Players")]
        public PlayersCfg Players { get; set; }

        [XmlElement(ElementName = "Sounds")]
        public SoundsCfg Sounds { get; set; }

        public static GameCoreService getGameService()
        {
            return new GameCoreEventHandler();
        }

        public static void Serialize(string file, Config c)
        {
            System.Xml.Serialization.XmlSerializer xs
               = new System.Xml.Serialization.XmlSerializer(c.GetType());
            StreamWriter writer = File.CreateText(file);
            xs.Serialize(writer, c);
            writer.Flush();
            writer.Close();
        }

        public static Config Deserialize(string file)
        {
            System.Xml.Serialization.XmlSerializer xs
               = new System.Xml.Serialization.XmlSerializer(
                  typeof(Config));
            StreamReader reader = File.OpenText(file);
            Config c = (Config)xs.Deserialize(reader);
            reader.Close();
            return c;
        }
    }
}
