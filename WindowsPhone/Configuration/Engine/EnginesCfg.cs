using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Configuration.Engine
{
    [XmlType(TypeName = "Engines")]
    public class EnginesCfg
    {
        [XmlElement(ElementName = "Engine")]
        public List<EngineCfg> engines;

    }
}
