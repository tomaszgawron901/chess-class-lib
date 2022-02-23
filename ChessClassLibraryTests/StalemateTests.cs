using ChessClassLibrary.enums;
using ChessClassLibrary.Games.ClassicGame;
using ChessClassLibrary.Models;
using ChessClassLibraryTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessClassLibraryTests
{
    [TestClass]
    public class StalemateTests
    {
        [TestMethod]
        public void black_stalemate_full_game()
        {
            var game = new ClassicGame();
            Assert.AreEqual(game.GameState, GameState.NotStarted);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(4, 1), new Position(4, 2)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(0, 6), new Position(0, 4)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 0), new Position(7, 4)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(0, 7), new Position(0, 5)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(7, 4), new Position(0, 4)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(7, 6), new Position(7, 4)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(7, 1), new Position(7, 3)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(0, 5), new Position(7, 5)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(0, 4), new Position(2, 6)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(5, 6), new Position(5, 5)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(2, 6), new Position(3, 6)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.Checked);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(4, 7), new Position(5, 6)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 6), new Position(1, 6)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 7), new Position(3, 2)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(1, 6), new Position(1, 7)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 2), new Position(7, 6)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(1, 7), new Position(2, 7)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(5, 6), new Position(6, 5)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            /*-----------------------------------------------------------------------------------*/
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(2, 7), new Position(4, 5)));
            Assert.AreEqual(game.GameState, GameState.Ended);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.Stalemated);
        }

        [TestMethod]
        public void white_stalemate_all_pieces_on_board_full_game()
        {
            var game = new ClassicGame();
            Assert.AreEqual(game.GameState, GameState.NotStarted);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 1), new Position(3, 3)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 6), new Position(3, 5)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 0), new Position(3, 1)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(4, 6), new Position(4, 4)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(0, 1), new Position(0, 3)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(4, 4), new Position(4, 3)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 1), new Position(5, 3)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(5, 6), new Position(5, 4)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(7, 1), new Position(7, 2)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(5, 7), new Position(4, 6)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(5, 3), new Position(7, 1)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(2, 7), new Position(4, 5)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(0, 0), new Position(0, 2)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(2, 6), new Position(2, 4)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(0, 2), new Position(6, 2)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 7), new Position(0, 4)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.Checked);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(1, 0), new Position(3, 1)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(4, 6), new Position(7, 3)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(5, 1), new Position(5, 2)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(4, 5), new Position(1, 2)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 3), new Position(3, 4)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(4, 3), new Position(4, 2)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(2, 1), new Position(2, 3)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            /*----------------------------------------------------------------------------------*/
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(5, 4), new Position(5, 3)));
            Assert.AreEqual(game.GameState, GameState.Ended);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.Stalemated);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);
        }

    }
}
