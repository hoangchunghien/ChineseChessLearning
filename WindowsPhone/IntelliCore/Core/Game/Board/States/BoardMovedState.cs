﻿using Intelli.Core.Game.Board.Events;
using Intelli.Core.Game.Board.Notifies;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelli.Core.Game.Board
{
    public class BoardMovedState : IState
    {
        public static readonly String NAME = "BoardMovedState";
        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        private BoardStateMachine boardMachine;
        private Dictionary<String, IState> transitionableStates = new Dictionary<string, IState>();

        public BoardMovedState(BoardStateMachine board)
        {
            this.boardMachine = board;
        }

        public string getName()
        {
            return "board_moved_state";
        }

        public void run(IEvent e)
        {
            ReadyNotify notify = new ReadyNotify();
            this.boardMachine.fireStateChangedNotification(notify);
            LOG.Info(NAME);
            this.boardMachine.consumeEvent(new BoardReadyEvent());
        }


        public Dictionary<string, IState> getTransitionableState()
        {
            return this.transitionableStates;
        }
    }
}
