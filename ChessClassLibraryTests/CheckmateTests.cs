using ChessClassLibrary.Enums;
using ChessClassLibrary.Models;
using ChessClassLibraryTests.Helpers;
using hessClassLibrary.Logic.Games;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(5, 1), new Position(5, 2)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(4, 6), new Position(4, 4)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(6, 1), new Position(6, 3)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 7), new Position(7, 3)));
            Assert.IsTrue(game.Board.Where(p => p != null && p.Color == PieceColor.White).All(p => p.MoveSet.Count() == 0));
            Assert.AreEqual(game.GameState, GameState.Ended);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.Checkmated);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);
        }


        [TestMethod()]
        public void checkmate_white_win_game()
        {
            var game = new ClassicGame();
            Assert.AreEqual(game.GameState, GameState.NotStarted);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(4, 1), new Position(4, 3)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(5, 6), new Position(5, 4)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(5, 1), new Position(5, 3)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(6, 6), new Position(6, 4)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.None);


            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 0), new Position(7, 4)));
            Assert.IsTrue(game.Board.Where(p => p != null && p.Color == PieceColor.Black).All(p => p.MoveSet.Count() == 0));
            Assert.AreEqual(game.GameState, GameState.Ended);
            Assert.AreEqual(game.WhiteKingManager.KingState, KingState.None);
            Assert.AreEqual(game.BlackKingManager.KingState, KingState.Checkmated);
        }
    }
}
