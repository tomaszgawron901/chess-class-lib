using ChessClassLibrary.Games.ClassicGame;
using ChessClassLibrary.Models;
using ChessClassLibraryTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessClassLibraryTests
{
    [TestClass]
    public class CastleTests
    {
        [TestMethod()]
        public void white_left_castle_correct()
        {
            var game = new ClassicGame();

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(1, 0), new Position(0, 2)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(2, 6), new Position(2, 4)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(2, 1), new Position(2, 3)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 6), new Position(3, 4)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 1), new Position(3, 3)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(1, 6), new Position(1, 4)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(2, 0), new Position(3, 1)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(0, 6), new Position(0, 4)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 0), new Position(2, 1)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(4, 6), new Position(4, 4)));
            /*-----------------------------------------------------------------------------------*/
            var castleMove = new BoardMove(new Position(4, 0), new Position(2, 0));
            var leftRook = game.Board.GetPiece(new Position(0, 0));
            ChessAssert.PerformMoveAndStandardCheck(game, castleMove);
            
            Assert.IsNull(game.Board.GetPiece(new Position(0, 0)));

            Assert.AreSame(leftRook, game.Board.GetPiece(new Position(3, 0)));
            Assert.AreEqual(leftRook.Position, new Position(3, 0));
        }

        [TestMethod()]
        public void white_right_castle_correct()
        {
            var game = new ClassicGame();

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(6, 0), new Position(7, 2)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(5, 6), new Position(5, 4)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(6, 1), new Position(6, 2)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(6, 6), new Position(6, 4)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(5, 0), new Position(6, 1)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(7, 6), new Position(7, 4)));
            /*-----------------------------------------------------------------------------------*/
            var castleMove = new BoardMove(new Position(4, 0), new Position(6, 0));
            var rightRook = game.Board.GetPiece(new Position(7, 0));
            ChessAssert.PerformMoveAndStandardCheck(game, castleMove);

            Assert.IsNull(game.Board.GetPiece(new Position(7, 0)));

            Assert.AreSame(rightRook, game.Board.GetPiece(new Position(5, 0)));
            Assert.AreEqual(rightRook.Position, new Position(5, 0));
        }




        [TestMethod()]
        public void black_left_castle_correct()
        {
            var game = new ClassicGame();

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(0, 1), new Position(0, 3)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 6), new Position(3, 4)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(1, 1), new Position(1, 3)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 7), new Position(3, 5)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(2, 1), new Position(2, 3)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(2, 7), new Position(3, 6)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 1), new Position(3, 3)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(1, 7), new Position(2, 5)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(4, 1), new Position(4, 3)));
            /*-----------------------------------------------------------------------------------*/
            var castleMove = new BoardMove(new Position(4, 7), new Position(2, 7));
            var leftRook = game.Board.GetPiece(new Position(0, 7));
            ChessAssert.PerformMoveAndStandardCheck(game, castleMove);

            Assert.IsNull(game.Board.GetPiece(new Position(0, 7)));

            Assert.AreSame(leftRook, game.Board.GetPiece(new Position(3, 7)));
            Assert.AreEqual(leftRook.Position, new Position(3, 7));
        }


        [TestMethod()]
        public void black_right_castle_correct()
        {
            var game = new ClassicGame();

            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(0, 1), new Position(0, 3)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(6, 7), new Position(7, 5)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(1, 1), new Position(1, 3)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(6, 6), new Position(6, 5)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(2, 1), new Position(2, 3)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(5, 7), new Position(6, 6)));
            ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(3, 1), new Position(3, 3)));
            /*-----------------------------------------------------------------------------------*/
            var castleMove = new BoardMove(new Position(4, 7), new Position(6, 7));
            var rightRook = game.Board.GetPiece(new Position(7, 7));
            ChessAssert.PerformMoveAndStandardCheck(game, castleMove);

            Assert.IsNull(game.Board.GetPiece(new Position(7, 7)));

            Assert.AreSame(rightRook, game.Board.GetPiece(new Position(5, 7)));
            Assert.AreEqual(rightRook.Position, new Position(5, 7));
        }

    }
}
