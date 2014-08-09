using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification
{
    public class NotificationCenter
    {
        private static NotificationCenter instance = null;
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

        /// <summary>
        /// Add this channel and new list<Inotifiable> to channelsubcribers if this channel doens't exist
        /// </summary>
        /// <param name="channel"></param>
        public void registerChannel(Channel channel)
        {
            if (!this.channelSubcribers.ContainsKey(channel))
            {
                this.channelSubcribers.Add(channel, new List<INotifiable>());
            }
        }

        /// <summary>
        /// Broadcast all of clients in this channel
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="notification"></param>
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
        /// Add Inotifiable (client) to this channel if client doesn't exist client
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
