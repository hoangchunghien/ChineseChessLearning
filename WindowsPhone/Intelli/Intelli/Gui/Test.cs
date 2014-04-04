using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Intelli.Gui
{
    public class Test
    {
        public void Understanding ()
        {
            Intelli.Event.Game.PositionDetail positionDetail = new Intelli.Event.Game.PositionDetail(4, 5);
            Intelli.Event.Game.RequestValidMovesEvent reqEvent = new Intelli.Event.Game.RequestValidMovesEvent(positionDetail);
            Intelli.Event.Game.ValidMovesEvent validMovesEvent = Intelli.Config.GameConfig.getGameService().requestValidMoves(reqEvent);

            validMovesEvent.getValidMoves();
        }
        
    }
}
