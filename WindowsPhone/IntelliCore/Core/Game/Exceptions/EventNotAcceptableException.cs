using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliCore.Core.Game.Exceptions
{
    public class EventNotAcceptableException : Exception
    {
        public EventNotAcceptableException()
            : base()
        {

        }

        public EventNotAcceptableException(String msg)
            : base(msg)
        {

        }
    }
}
