using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Configuration.Player
{
    [XmlType(TypeName = "Players")]
    public class PlayersCfg
    {

        [XmlAttribute(AttributeName = "playerFirstId")]
        public int playerFirstId { get; set; }

        [XmlAttribute(AttributeName = "playerOnlineId")]
        public int playerOnlineId { get; set; }

        [XmlAttribute(AttributeName = "autoViewer")]
        public bool autoViewer { get; set; }

        [XmlElement(ElementName = "Player")]
        public List<PlayerCfg> players { get; set; }



    }
}
