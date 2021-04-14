﻿using ChessClassLibrary;
using ChessClassLibrary.enums;
using ChessClassLibrary.Games.ClassicGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessClassLibraryTests
{
    [TestClass]
    public class FullGameTests
    {

        private void AssertMoveCorrect(ClassicGame game, BoardMove move)
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

        [TestMethod()]
        public void checkmate_black_win_game()
        {
            var game = new ClassicGame();
            Assert.AreEqual(game.GameState, GameState.NotStarted);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            AssertMoveCorrect(game, new BoardMove(new Position(5, 1), new Position(5, 2)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            AssertMoveCorrect(game, new BoardMove(new Position(4, 6), new Position(4, 4)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            AssertMoveCorrect(game, new BoardMove(new Position(6, 1), new Position(6, 3)));
            Assert.AreEqual(game.GameState, GameState.InProgress);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.None);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);

            AssertMoveCorrect(game, new BoardMove(new Position(3, 7), new Position(7, 3)));
            Assert.AreEqual(game.GameState, GameState.Ended);
            Assert.AreEqual(game.WhiteKing.KingState, KingState.Checkmated);
            Assert.AreEqual(game.BlackKing.KingState, KingState.None);
        }
    }
}
