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
        [XmlAttribute(AttributeName = "name")]
        public string name { get; set; }

        [XmlAttribute(AttributeName = "engine")]
        public string engine { get; set; }

        [XmlAttribute(AttributeName = "selected")]
        public bool selected { get; set; }
    }
}
