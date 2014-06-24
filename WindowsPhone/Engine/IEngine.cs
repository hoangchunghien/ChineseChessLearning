using Engine.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public interface IEngine
    {
        String getBestMove(String humanMoveAction);//String board is a fen anotation
        //[Fen "rnbakabnr/9/1c5c1/p1p1p1p1p/9/9/P1P1P1P1P/1C5C1/9/RNBAKABNR w"]

        void setStartupBoard(String fenBoard);

        void setLevel(int level);
    }
}
