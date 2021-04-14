using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessClassLibrary.Games.ClassicGame;
using ChessClassLibrary.enums;

namespace ChessClassLibrary.Tests
{
    [TestClass()]
    public class BasicClassicGameTests: ClassicGame
    {
        public BasicClassicGameTests() { }


        [TestMethod()]
        public void constructor_correct()
        {
            Assert.IsNotNull(Board);
            Assert.AreEqual(CurrentPlayerColor, PieceColor.White);
            Assert.AreEqual(GameState, GameState.NotStarted);
        }

        [TestMethod]
        public void change_player_correct()
        {
            var currentColor = CurrentPlayerColor;
            SwapPlayers();
            Assert.AreNotEqual(currentColor, CurrentPlayerColor);
            currentColor = CurrentPlayerColor;
            SwapPlayers();
            Assert.AreNotEqual(currentColor, CurrentPlayerColor);
        }

        [TestMethod()]
        public void cannot_move_enemy_pieces()
        {
            Assert.IsFalse(CanPerformMove(new BoardMove(new Position(0, 6), new Position(0, 5))));
            Assert.IsFalse(CanPerformMove(new BoardMove(new Position(2, 6), new Position(2, 5))));
            Assert.IsFalse(CanPerformMove(new BoardMove(new Position(3, 6), new Position(3, 5))));
            Assert.IsFalse(CanPerformMove(new BoardMove(new Position(4, 6), new Position(4, 5))));
            SwapPlayers();
            Assert.IsFalse(CanPerformMove(new BoardMove(new Position(0, 1), new Position(0, 2))));
            Assert.IsFalse(CanPerformMove(new BoardMove(new Position(2, 1), new Position(2, 2))));
            Assert.IsFalse(CanPerformMove(new BoardMove(new Position(3, 1), new Position(3, 2))));
            Assert.IsFalse(CanPerformMove(new BoardMove(new Position(4, 1), new Position(4, 2))));
        }

        [TestMethod()]
        public void can_move_own_pieces()
        {
            Assert.IsTrue(CanPerformMove(new BoardMove(new Position(0, 1), new Position(0, 2))));
            Assert.IsTrue(CanPerformMove(new BoardMove(new Position(2, 1), new Position(2, 2))));
            Assert.IsTrue(CanPerformMove(new BoardMove(new Position(3, 1), new Position(3, 2))));
            Assert.IsTrue(CanPerformMove(new BoardMove(new Position(4, 1), new Position(4, 2))));
            SwapPlayers();
            Assert.IsTrue(CanPerformMove(new BoardMove(new Position(0, 6), new Position(0, 5))));
            Assert.IsTrue(CanPerformMove(new BoardMove(new Position(2, 6), new Position(2, 5))));
            Assert.IsTrue(CanPerformMove(new BoardMove(new Position(3, 6), new Position(3, 5))));
            Assert.IsTrue(CanPerformMove(new BoardMove(new Position(4, 6), new Position(4, 5))));
        }
    }
}