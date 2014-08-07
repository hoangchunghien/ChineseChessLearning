using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Configuration.Sound
{
    [XmlType(TypeName = "Sounds")]
    public class SoundsCfg
    {
        [XmlAttribute(AttributeName="playSound")]
        public bool playSound { get; set; }

        [XmlElement(ElementName = "Sound")]
        public List<SoundCfg> sounds { get; set; }

        public SoundCfg getBackgroundSound()
        {
            foreach (SoundCfg sound in sounds)
            {
                if (sound.name.Equals(SoundCfg.BACKGROUND))
                    return sound;
            }
            return null;
        }

        public SoundCfg getMoveSound()
        {
            foreach (SoundCfg sound in sounds)
            {
                if (sound.name.Equals(SoundCfg.MOVE))
                    return sound;
            }
            return null;
        }

        public SoundCfg getChessSound()
        {
            foreach (SoundCfg sound in sounds)
            {
                if (sound.name.Equals(SoundCfg.CHESS))
                    return sound;
            }
            return null;
        }
        public SoundCfg getGameoverSound()
        {
            foreach (SoundCfg sound in sounds)
            {
                if (sound.name.Equals(SoundCfg.GAMEOVER))
                    return sound;
            }
            return null;
        }
    }
}
