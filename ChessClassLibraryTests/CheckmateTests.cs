using ChessClassLibrary.enums;
using ChessClassLibrary.Games.ClassicGame;
using ChessClassLibrary.Models;
using ChessClassLibraryTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessClassLibraryTests
{
    [TestClass]
    public class CheckmateTests
    {
        [TestMethod()]
        public void checkmate_black_win_game()
        {
            var game = new ClassicGame();
            Assert.AreEqual(game.GameState, GameState.NotStarted);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(5, 1), new Position(5, 2)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(4, 6), new Position(4, 4)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(6, 1), new Position(6, 3)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 7), new Position(7, 3)));
            Assert.AreEqual(game.GameState, GameState.Ended);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.Checkmated);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);
        }


        [TestMethod()]
        public void checkmate_white_win_game()
        {
            var game = new ClassicGame();
            Assert.AreEqual(game.GameState, GameState.NotStarted);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(4, 1), new Position(4, 3)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(5, 6), new Position(5, 4)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(5, 1), new Position(5, 3)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(6, 6), new Position(6, 4)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);


            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 0), new Position(7, 4)));
            Assert.AreEqual(game.GameState, GameState.Ended);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.Checkmated);
        }
    }
}
