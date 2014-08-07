using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Configuration.Engine
{
    [XmlType(TypeName = "Level")]
    public class LevelCfg
    {
        public static readonly String EASY = "easy";
        public static readonly String NORMAL = "normal";
        public static readonly String HARD = "hard";
        public static readonly String EXPERT = "expert";

        [XmlAttribute(AttributeName = "name")]
        public string name { get; set; }

        [XmlAttribute(AttributeName = "depth")]
        public int depth { get; set; }

        [XmlAttribute(AttributeName = "engine")]
        public string engine { get; set; }

        [XmlAttribute(AttributeName = "selected")]
        public bool selected { get; set; }
    }
}
