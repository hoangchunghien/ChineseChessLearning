using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification
{
    public interface INotifiable
    {
        void receiveNotification(Channel channel, Object notification);
    }
}
