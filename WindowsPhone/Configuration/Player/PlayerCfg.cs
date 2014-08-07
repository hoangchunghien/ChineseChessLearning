using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Configuration.Player
{
    [XmlType(TypeName = "Player")]
    public class PlayerCfg
    {
        public static readonly String TYPE_HUMAN = "human";
        public static readonly String TYPE_COMPUTER = "computer";
        public static readonly String TYPE_ONLINE = "online";

        [XmlAttribute(AttributeName = "id")]
        public int id { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public String name { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public String type { get; set; }

        [XmlAttribute(AttributeName = "color")]
        public String color { get; set; }

    }
}
