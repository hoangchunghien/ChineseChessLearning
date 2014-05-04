using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliCore.Core.Game.Player.Exceptions
{
    public class PlayerJoinException : Exception
    {
        public PlayerJoinException()
            : base()
        {

        }

        public PlayerJoinException(String message)
            : base(message)
        {

        }
    }
}
