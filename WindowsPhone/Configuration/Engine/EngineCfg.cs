using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Configuration.Engine
{

    [XmlType(TypeName = "Engine")]
    public class EngineCfg
    {
        [XmlAttribute(AttributeName = "name")]
        public string name { get; set; }
    }

}
