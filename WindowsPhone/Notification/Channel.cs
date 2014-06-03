using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification
{
    public class Channel
    {
        private static int numChannel = 0;

        private String name;

        public Channel()
        {
            initialize();
            this.name = "Channel_" + numChannel;
        }

        public Channel(String name)
        {
            initialize();
            this.name = name;
        }

        private void initialize()
        {
            numChannel++;
        }

        public String getName()
        {
            return this.name;
        }

        public void setName(String name)
        {
            this.name = name;
        }
    }
}
