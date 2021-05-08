using ChessClassLibrary;
using ChessClassLibrary.Games.ClassicGame;
using ChessClassLibrary.Models;
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
            var pieceAtCurrectPosition = game.Board.GetPiece(move.Current);
            var currentPlayer = game.CurrentPlayerColor;

            Assert.IsTrue(game.CanPerformMove(move));
            game.TryPerformMove(move);
            Assert.IsNull(game.Board.GetPiece(move.Current));
            Assert.AreSame(pieceAtCurrectPosition, game.Board.GetPiece(move.Destination));
            Assert.AreEqual(game.Board.GetPiece(move.Destination).Position, move.Destination);
            Assert.AreNotEqual(game.CurrentPlayerColor, currentPlayer);
        }
    }
}
