using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player
{
    public class ComputerPlayer : AbstractPlayer
    {
        IEngine engine;

        public ComputerPlayer(IEngine engine)
        {
            this.engine = engine;
            //this.nb.subscribe(this, channel);
        }

        public IEngine getEngine()
        {
            return this.engine;
        }

        public void setEngine(IEngine engine)
        {
            this.engine = engine;
        }

    }
}
