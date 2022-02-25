using ChessClassLibrary.Enums;
using ChessClassLibrary.Models;
using ChessClassLibraryTests.Helpers;
using hessClassLibrary.Logic.Games;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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
        public void pawns_normal_move_correct()
        {
            var game = new ClassicGame();
            for (int x = 0; x < game.Board.Width; x++)
            {
                ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(x, 1), new Position(x, 2)));
                ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(x, 6), new Position(x, 5)));
            }
        }

        [TestMethod]
        public void pawns_long_move_correct()
        {
            var game = new ClassicGame();
            for (int x = 0; x < game.Board.Width; x++)
            {
                ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(x, 1), new Position(x, 3)));
                ChessAssert.PerformMoveAndStandardCheck(game, new BoardMove(new Position(x, 6), new Position(x, 4)));
            }
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(7)]
        public void white_pawn_move_set_correct_at_the_beginning(int column)
        {
            var game = new ClassicGame();
            
            var piece = game.Board.GetPiece(new Position(column, 1));
            Assert.AreEqual(piece.MoveSet.Count(), 2);
            Assert.IsTrue(piece.MoveSet.Any(move => move.Equals(new PieceMove(new Position(0, 1), MoveType.Move))));
            Assert.IsTrue(piece.MoveSet.Any(move => move.Equals(new PieceMove(new Position(0, 2), MoveType.Move))));

            Assert.IsFalse(piece.MoveSet.Any(x => x.Shift == new Position(-1, 1)));
            Assert.IsFalse(piece.MoveSet.Any(x => x.Shift == new Position(1, 1)));
        }


        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(7)]
        public void black_pawn_move_set_correct_at_the_beginning(int column)
        {
            var game = new ClassicGame();

            var piece = game.Board.GetPiece(new Position(column, 6));
            Assert.AreEqual(piece.MoveSet.Count(), 2);
            Assert.IsTrue(piece.MoveSet.Any(move => move.Equals(new PieceMove(new Position(0, -1), MoveType.Move))));
            Assert.IsTrue(piece.MoveSet.Any(move => move.Equals(new PieceMove(new Position(0, -2), MoveType.Move))));

            Assert.IsFalse(piece.MoveSet.Any(x => x.Shift == new Position(-1, -1)));
            Assert.IsFalse(piece.MoveSet.Any(x => x.Shift == new Position(1, -1)));
        }

    }
}
