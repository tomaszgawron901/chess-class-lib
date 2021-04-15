using ChessClassLibrary;
using ChessClassLibrary.Games.ClassicGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessClassLibraryTests
{
    public static class ChessAssert
    {
        public static void IsMoveCorrect(ClassicGame game, BoardMove move)
        {
            var pieceAtCurrectPosition = game.Board.GetPiece(move.current);
            var currentPlayer = game.CurrentPlayerColor;

            Assert.IsTrue(game.CanPerformMove(move));
            game.TryPerformMove(move);
            Assert.IsNull(game.Board.GetPiece(move.current));
            Assert.AreSame(pieceAtCurrectPosition, game.Board.GetPiece(move.destination));
            Assert.AreEqual(game.Board.GetPiece(move.destination).Position, move.destination);
            Assert.AreNotEqual(game.CurrentPlayerColor, currentPlayer);
        }
    }
}
