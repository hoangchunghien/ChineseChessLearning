using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Event.Base
{
    public class ReadEvent
    {
        protected bool accepted = true;

        public bool isAccepted()
        {
            return accepted;
        }
    }
}
