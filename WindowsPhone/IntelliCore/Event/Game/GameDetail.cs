using Intelli.Core.Game;
using Intelli.Core.Game.Board.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intelli.Event.Game
{
    public class GameDetail
    {
        private BoardDetail boardDetail;
        private PlayerDetail[] playerDetails;

        public static GameDetail fromGameStateMachine(GameStateMachine gameMachine)
        {
            GameDetail detail = new GameDetail();

            // Convert BoardStateMachine to BoardDetail   
            char[,] cPieces = gameMachine.getBoardMachine().getBoard().serialize();
            BoardDetail boardDetail = new BoardDetail(cPieces);

            // Convert from PlayerStateMachine to PlayerDetail
            PlayerDetail[] playerDetails = new PlayerDetail[2]; // STUB
            playerDetails[0] = new PlayerDetail(gameMachine.getBoardMachine().getPieces(Color.BLACK));
            playerDetails[1] = new PlayerDetail(gameMachine.getBoardMachine().getPieces(Color.RED));

            // Game detail
            detail.boardDetail = boardDetail;
            detail.playerDetails = playerDetails;

            return detail;
        }

        public BoardDetail getBoarDetail()
        {
            return this.boardDetail;
        }
    }
}
