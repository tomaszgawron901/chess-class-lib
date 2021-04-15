using ChessClassLibrary;
using ChessClassLibrary.Games.ClassicGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessClassLibraryTests
{
    [TestClass]
    public class PieceCastleTests
    {
        [TestMethod()]
        public void white_left_castle_correct()
        {
            var game = new ClassicGame();

            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(1, 0), new Position(0, 2)));
            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(2, 6), new Position(2, 4)));
            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(2, 1), new Position(2, 3)));
            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(3, 6), new Position(3, 4)));
            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(3, 1), new Position(3, 3)));
            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(1, 6), new Position(1, 4)));
            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(2, 0), new Position(3, 1)));
            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(0, 6), new Position(0, 4)));
            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(3, 0), new Position(2, 1)));
            ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(4, 6), new Position(4, 4)));
            /*-----------------------------------------------------------------------------------*/
            var castleMove = new BoardMove(new Position(4, 0), new Position(2, 0));
            var leftRook = game.Board.GetPiece(new Position(0, 0));
            ChessAssert.IsMoveCorrect(game, castleMove);
            
            Assert.IsNull(game.Board.GetPiece(new Position(0, 0)));

            Assert.AreSame(leftRook, game.Board.GetPiece(new Position(3, 0)));
            Assert.AreEqual(leftRook.Position, new Position(3, 0));

        }
    }
}
