using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Configuration.Sound
{
    [XmlType(TypeName = "Sound")]
    public class SoundCfg
    {
        public static readonly String BACKGROUND = "background";
        public static readonly String MOVE = "move";
        public static readonly String CHESS = "chess";
        public static readonly String GAMEOVER = "gameover";

        [XmlAttribute(AttributeName = "name")]
        public String name { get; set; }

        [XmlAttribute(AttributeName = "file")]
        public String file { get; set; }

        [XmlAttribute(AttributeName = "play")]
        public bool play { get; set; }

    }
}
