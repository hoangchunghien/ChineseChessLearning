using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.XQWLight_AI;
using Engine.Domain;

namespace Engine.Implements
{
    public interface IOpponentCallback
    {
        void OnOpponentMove(ChessMove move, MoveResult result);
    }
    public class XQWLightEngine: IEngine
    {
        public static readonly String NAME = "XQWLight";
        private Engine.XQWLight_AI.XQWLight_AI m_xqwLight;

        public XQWLightEngine(int level)
        {
            this.m_xqwLight = new XQWLight_AI.XQWLight_AI();
            this.setXqlLevel(level);
            this.m_xqwLight.init_game(null, 'w');
        }

        static readonly int[] searchTimePerLevel = new int[] { 5, 10, 20, 30, 50 };
        private void setXqlLevel(int level)
        {
            this.m_xqwLight.init_engine(level);
            this.m_xqwLight.set_search_time(searchTimePerLevel[level - 1]);
            
        }
        public Domain.ChessMove getBestMove(ChessMove humanMoveAction)
        {
            String move = String.Format("{0}{1}{2}{3}",
                humanMoveAction.From.X,
                humanMoveAction.From.Y,
                humanMoveAction.To.X,
                humanMoveAction.To.Y);

            this.m_xqwLight.on_human_move(move);

            String s = this.m_xqwLight.generate_move();

            String b = this.m_xqwLight.ToString();

            return new ChessMove(int.Parse(s.Substring(0, 1)), 
                int.Parse(s.Substring(1, 1)),
                int.Parse(s.Substring(2, 1)),
                int.Parse(s.Substring(3, 1)));
        }
    }
}
