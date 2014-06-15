using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;

namespace Player
{
    public class HumanPlayer: AbstractPlayer
    {
        IUIControl uiControl;

        public HumanPlayer(IUIControl uiControl)
        {
            this.uiControl = uiControl;
        }

        public IUIControl getUIControl()
        {
            return this.uiControl;
        }

        public void setUIControl(IUIControl uiControl)
        {
            this.uiControl = uiControl;
        }
    }
}
