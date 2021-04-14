using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessClassLibrary.enums;
using ChessClassLibrary.Games.ClassicGame;
using ChessClassLibrary.Logic;

namespace ChessClassLibrary.Tests
{
    [TestClass()]
    public class ClassicGameBoardTests: ClassicGame
    {
        public ClassicGameBoardTests(): base(){}

        [DataTestMethod()]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        public void correct_empy_row_create(int row)
        {
            for (int x = 0; x < this.Board.Width; x++)
            {
                Assert.IsNull(Board.GetPiece(new Position(x, row)));
            }
        }

        [DataTestMethod()]
        [DataRow(1, PieceColor.White)]
        [DataRow(6, PieceColor.Black)]
        public void correct_pawn_row_create(int row, PieceColor color)
        {
            for (int x = 0; x < this.Board.Width; x++)
            {
                var piece = Board.GetPiece(new Position(x, row));
                Assert.IsNotNull(piece);
                Assert.AreEqual(piece.Color, color);
                Assert.AreEqual(piece.Position, new Position(x, row));
                Assert.AreEqual(piece.Type, PieceType.Pawn);
            }
        }

        [DataTestMethod()]
        [DataRow(0, PieceColor.White)]
        [DataRow(7, PieceColor.Black)]
        public void correct_rith_row_create(int row, PieceColor color)
        {
            for (int x = 0; x < this.Board.Width; x++)
            {
                var piece = Board.GetPiece(new Position(x, row));
                Assert.IsNotNull(piece);
                Assert.AreEqual(piece.Color, color);
                Assert.AreEqual(piece.Position, new Position(x, row));
            }

            Assert.AreEqual(Board.GetPiece(new Position(0, row)).Type, PieceType.Rook);
            Assert.AreEqual(Board.GetPiece(new Position(1, row)).Type, PieceType.Knight);
            Assert.AreEqual(Board.GetPiece(new Position(2, row)).Type, PieceType.Bishop);
            Assert.AreEqual(Board.GetPiece(new Position(3, row)).Type, PieceType.Queen);
            Assert.AreEqual(Board.GetPiece(new Position(4, row)).Type, PieceType.King);
            Assert.AreEqual(Board.GetPiece(new Position(5, row)).Type, PieceType.Bishop);
            Assert.AreEqual(Board.GetPiece(new Position(6, row)).Type, PieceType.Knight);
            Assert.AreEqual(Board.GetPiece(new Position(7, row)).Type, PieceType.Rook);
        }

        [TestMethod()]
        public void game_kings_Are_equal_board_kings()
        {
            Assert.AreSame(WhiteKing, Board.GetPiece(new Position(4, 0)));
            Assert.AreSame(BlackKing, Board.GetPiece(new Position(4, 7)));
        }

        [TestMethod]
        public void all_contains_board()
        {
            foreach (var piece in Board)
            {
                if (piece !=null && piece is BasePieceDecorator)
                {
                    Assert.IsNotNull((piece as BasePieceDecorator).Board);
                    Assert.AreSame((piece as BasePieceDecorator).Board, Board);
                }
            }
        }

    }
}