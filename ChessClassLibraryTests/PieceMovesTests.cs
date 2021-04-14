using ChessClassLibrary.Games.ClassicGame;
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

    }
}
