using ChessClassLib.Enums;
using ChessClassLib.Logic.Boards;
using ChessClassLib.Logic.PieceRules;
using ChessClassLib.Logic.PieceRules.PieceRuleDecorators.TransformationRules;
using ChessClassLib.Pieces;
using ChessClassLib.Pieces.FasePieces;
using ChessClassLib.Pieces.SlowPieces;
using ChessClassLibraryTests.Extensions;
using ChessClassLibraryTests.Helpers;
using hessClassLibrary.Logic.Games;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessClassLib.Logic.PieceRules.PieceRuleDecorators;

namespace ChessClassLibraryTests
{
    [TestClass]
    public class TransformationTests
    {
        [TestMethod]
        public void white_piece_to_queen_transformation_correct()
        {
            var board = new ClassicBoard(new IPiece[8, 8]);
            var whiteQueen = new Queen(PieceColor.White, "a1".ToPosition())
                .AddBasePieceRule(board)
                .AddPieceOnBoardRule();
            var whitePawn = new WhitePawn("a1".ToPosition())
                .AddBasePieceRule(board)
                .AddPieceOnBoardRule()
                .AddOnMoveToYPositionTransformation(whiteQueen, 1);
            board.SetPiece(whitePawn);

            whitePawn.MoveToPosition("a2".ToPosition());

            Assert.AreSame(whiteQueen, board.GetPiece("a2".ToPosition()));
            Assert.IsNull(board.GetPiece("a1".ToPosition()));
        }


        [TestMethod]
        public void white_piece_to_queen_transformation_in_classic_game_correct()
        {
            var game = new ClassicGame();

            #region lead the board to a specific situation
            ChessAssert.PerformMoveAndStandardCheck(game, "f2 to f4");
            ChessAssert.PerformMoveAndStandardCheck(game, "e7 to e5");
            ChessAssert.PerformMoveAndStandardCheck(game, "f4 to e5");
            ChessAssert.PerformMoveAndStandardCheck(game, "f7 to f6");
            ChessAssert.PerformMoveAndStandardCheck(game, "e5 to f6");
            ChessAssert.PerformMoveAndStandardCheck(game, "d7 to d6");
            ChessAssert.PerformMoveAndStandardCheck(game, "f6 to g7");
            ChessAssert.PerformMoveAndStandardCheck(game, "d6 to d5");
            #endregion

            game.TryPerformMove("g7 to h8".ToBoardMove());
            var piece = game.Board.GetPiece("h8".ToPosition());
            Assert.AreEqual(piece.Type, PieceType.Queen);
            Assert.AreEqual(piece.Color, PieceColor.White);
            Assert.IsTrue(piece.WasMoved);

            ChessAssert.PerformMoveAndStandardCheck(game, "d5 to d4");

            Assert.IsTrue(game.CanPerformMove("h8 to h7".ToBoardMove()));
            Assert.IsTrue(game.CanPerformMove("h8 to g8".ToBoardMove()));
            Assert.IsTrue(game.CanPerformMove("h8 to g7".ToBoardMove()));
            Assert.IsTrue(game.CanPerformMove("h8 to f6".ToBoardMove()));
            Assert.IsTrue(game.CanPerformMove("h8 to e5".ToBoardMove()));
            Assert.IsTrue(game.CanPerformMove("h8 to d4".ToBoardMove()));

            Assert.IsNull(game.Board.GetPiece("g7".ToPosition()));

            ChessAssert.PerformMoveAndStandardCheck(game, "h8 to d4");
        }
    }
}
