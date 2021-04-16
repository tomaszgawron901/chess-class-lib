using ChessClassLibrary;
using ChessClassLibrary.enums;
using ChessClassLibrary.Games.ClassicGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessClassLibraryTests
{
    [TestClass]
    public class StalemateTests
    {
        [TestMethod]
        public void black_stale_mate_full_game()
        {
            var game = new ClassicGame();
            Assert.AreEqual(game.GameState, GameState.NotStarted);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(4, 1), new Position(4, 2)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(0, 6), new Position(0, 4)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(3, 0), new Position(7, 4)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(0, 7), new Position(0, 5)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(7, 4), new Position(0, 4)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(7, 6), new Position(7, 4)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(7, 1), new Position(7, 3)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(0, 5), new Position(7, 5)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(0, 4), new Position(2, 6)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(5, 6), new Position(5, 5)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(2, 6), new Position(3, 6)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.Checked);

            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(4, 7), new Position(5, 6)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(3, 6), new Position(1, 6)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(3, 7), new Position(3, 2)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(1, 6), new Position(1, 7)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(3, 2), new Position(7, 6)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(1, 7), new Position(2, 7)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(5, 6), new Position(6, 5)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            /*-----------------------------------------------------------------------------------*/
            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(2, 7), new Position(4, 5)));
            Assert.AreEqual(game.GameState, GameState.Ended);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.Stalemated);
        }
    }
}
