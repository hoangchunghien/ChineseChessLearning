using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Configuration.Engine
{
    [XmlType(TypeName = "Levels")]
    public class LevelsCfg
    {
        [XmlElement(ElementName = "Level")]
        public List<LevelCfg> levels;



    }
}
