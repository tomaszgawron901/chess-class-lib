using ChessClassLib.Models;
using ChessClassLibraryTests.Extensions;
using hessClassLibrary.Logic.Games;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibraryTests.Helpers
{
    public static class ChessAssert
    {
        public static void PerformMoveAndStandardCheck(ClassicGame game, string move)
        {
            PerformMoveAndStandardCheck(game, move.ToBoardMove());
        }
        public static void PerformMoveAndStandardCheck(ClassicGame game, BoardMove move)
        {
            var pieceAtCurrectPosition = game.Board.GetPiece(move.Current);
            var currentPlayer = game.CurrentPlayerColor;

            Assert.IsTrue(game.CanPerformMove(move));
            game.TryPerformMove(move);
            Assert.IsTrue(pieceAtCurrectPosition.WasMoved);
            Assert.IsNull(game.Board.GetPiece(move.Current));
            Assert.AreSame(pieceAtCurrectPosition, game.Board.GetPiece(move.Destination));
            Assert.AreEqual(game.Board.GetPiece(move.Destination).Position, move.Destination);
            Assert.AreNotEqual(game.CurrentPlayerColor, currentPlayer);
        }

        public static void MoveSetContainsMove(IEnumerable<PieceMove> moveSet, PieceMove move)
        {
            Assert.IsNotNull(move);
            Assert.IsTrue(moveSet.Any(move => move.Equals(move)));
        }

        public static void MoveSetContainsOnly(IEnumerable<PieceMove> moveSet, params PieceMove[] moves) {
            Assert.AreEqual(moveSet.Count(), moves.Length);
            Assert.IsTrue(moveSet.All(x => moves.Any(y => y.Equals(x))));
            Assert.IsTrue(moves.All(x => moveSet.Any(y => y.Equals(x))));
        }

        public static void MoveSetDoesNotContainMoves(IEnumerable<PieceMove> moveSet, params PieceMove[] moves)
        {
            Assert.IsFalse(moveSet.Any(x => moves.Any(y => y.Equals(x))));
        }
    }
}
