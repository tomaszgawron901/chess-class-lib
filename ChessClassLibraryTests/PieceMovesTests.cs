using ChessClassLibrary;
using ChessClassLibrary.Games.ClassicGame;
using ChessClassLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessClassLibraryTests
{
    [TestClass]
    public class PieceMovesTests
    {
        [TestMethod]
        public void all_piece_get_move_set_not_throwing_error_and_not_null()
        {
            var game = new ClassicGame();
            foreach (var piece in game.Board)
            {
                if (piece != null)
                {
                    Assert.IsNotNull(piece.MoveSet);
                }
            }
        }

        [TestMethod]
        public void pawns_move_correct()
        {
            var game = new ClassicGame();
            for (int x = 0; x < game.Board.Width; x++)
            {
                ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(x, 1), new Position(x, 2)));
                ChessAssert.IsMoveCorrect(game, new BoardMove(new Position(x, 6), new Position(x, 5)));
            }
        }

    }
}
