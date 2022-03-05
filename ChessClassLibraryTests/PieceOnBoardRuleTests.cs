using ChessClassLib.Logic.Boards;
using ChessClassLib.Pieces;
using ChessClassLib.Pieces.SlowPieces;
using ChessClassLibraryTests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessClassLib.Logic.PieceRules.PieceRuleDecorators;
using ChessClassLib.Logic.PieceRules;
using System.Linq;
using ChessClassLibraryTests.Helpers;
using ChessClassLib.Models;
using ChessClassLib.Enums;

namespace ChessClassLibraryTests
{
    [TestClass]
    public class PieceOnBoardRuleTests
    {
        [TestMethod]
        public void white_pawn_no_moves_available()
        {
            var board = new ClassicBoard(new IPiece[8, 8]);
            var whitePawnOnBoard = new WhitePawn("e8".ToPosition())
                .AddBasePieceRule(board)
                .AddPieceOnBoardRule();
            board.SetPiece(whitePawnOnBoard);

            Assert.IsTrue(whitePawnOnBoard.MoveSet.Count() == 0);
        }

        [TestMethod]
        public void white_pawn_in_a7_position_moveset_correct()
        {
            var board = new ClassicBoard(new IPiece[8, 8]);
            var whitePawnOnBoard = new WhitePawn("a7".ToPosition())
                .AddBasePieceRule(board)
                .AddPieceOnBoardRule();
            board.SetPiece(whitePawnOnBoard);

            ChessAssert.MoveSetContainsOnly(whitePawnOnBoard.MoveSet, 
                new PieceMove(new Shift(0, 1), MoveType.Move),
                new PieceMove(new Shift(1, 1), MoveType.Kill));
        }

        [TestMethod]
        public void white_pawn_in_h7_position_moveset_correct()
        {
            var board = new ClassicBoard(new IPiece[8, 8]);
            var whitePawnOnBoard = new WhitePawn("h7".ToPosition())
                .AddBasePieceRule(board)
                .AddPieceOnBoardRule();
            board.SetPiece(whitePawnOnBoard);

            ChessAssert.MoveSetContainsOnly(whitePawnOnBoard.MoveSet,
                new PieceMove(new Shift(0, 1), MoveType.Move),
                new PieceMove(new Shift(-1, 1), MoveType.Kill));
        }

        [TestMethod]
        public void black_pawn_no_moves_available()
        {
            var board = new ClassicBoard(new IPiece[8, 8]);
            var whitePawnOnBoard = new BlackPawn("e1".ToPosition())
                .AddBasePieceRule(board)
                .AddPieceOnBoardRule();
            board.SetPiece(whitePawnOnBoard);

            Assert.IsTrue(whitePawnOnBoard.MoveSet.Count() == 0);
        }

        [TestMethod]
        public void black_pawn_in_a2_position_moveset_correct()
        {
            var board = new ClassicBoard(new IPiece[8, 8]);
            var whitePawnOnBoard = new BlackPawn("a2".ToPosition())
                .AddBasePieceRule(board)
                .AddPieceOnBoardRule();
            board.SetPiece(whitePawnOnBoard);

            ChessAssert.MoveSetContainsOnly(whitePawnOnBoard.MoveSet,
                new PieceMove(new Shift(0, -1), MoveType.Move),
                new PieceMove(new Shift(1, -1), MoveType.Kill));
        }

        [TestMethod]
        public void black_pawn_in_h2_position_moveset_correct()
        {
            var board = new ClassicBoard(new IPiece[8, 8]);
            var whitePawnOnBoard = new BlackPawn("h7".ToPosition())
                .AddBasePieceRule(board)
                .AddPieceOnBoardRule();
            board.SetPiece(whitePawnOnBoard);

            ChessAssert.MoveSetContainsOnly(whitePawnOnBoard.MoveSet,
                new PieceMove(new Shift(0, -1), MoveType.Move),
                new PieceMove(new Shift(-1, -1), MoveType.Kill));
        }
    }
}
