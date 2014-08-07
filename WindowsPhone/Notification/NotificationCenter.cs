using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification
{
    public class NotificationCenter
    {
        private static NotificationCenter instance;
        public static NotificationCenter getInstance()
        {
            if (instance == null)
            {
                instance = new NotificationCenter();
            }
            return instance;
        }

        private Dictionary<Channel, List<INotifiable>> channelSubcribers;

        private NotificationCenter()
        {
            initialize();
        }

        private void initialize()
        {
            this.channelSubcribers = new Dictionary<Channel, List<INotifiable>>();
        }

        public void registerChannel(Channel channel)
        {
            if (!this.channelSubcribers.ContainsKey(channel))
            {
                this.channelSubcribers.Add(channel, new List<INotifiable>());
            }
        }

        public void broadcastNotification(Channel channel, Object notification)
        {
            if (this.channelSubcribers.ContainsKey(channel))
            {
                foreach (INotifiable client in this.channelSubcribers[channel]) {
                    client.receiveNotification(channel, notification);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="channel"></param>
        public void subscribe(INotifiable client, Channel channel)
        {
            if (this.channelSubcribers.ContainsKey(channel) 
                && !this.channelSubcribers[channel].Contains(client))
            {
                this.channelSubcribers[channel].Add(client);
            }
        }
    }
}
