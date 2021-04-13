using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessClassLibrary.Pieces;
using ChessClassLibrary.Pieces.SlowPieces;
using ChessClassLibrary.enums;
using ChessClassLibrary.Games.ClassicGame;

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
            for (int x = 0; x < this.board.Width; x++)
            {
                Assert.IsNull(board.GetPiece(new Position(x, row)));
            }
        }

        [DataTestMethod()]
        [DataRow(1, PieceColor.White)]
        [DataRow(6, PieceColor.Black)]
        public void correct_pawn_row_create(int row, PieceColor color)
        {
            for (int x = 0; x < this.board.Width; x++)
            {
                var piece = board.GetPiece(new Position(x, row));
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
            for (int x = 0; x < this.board.Width; x++)
            {
                var piece = board.GetPiece(new Position(x, row));
                Assert.IsNotNull(piece);
                Assert.AreEqual(piece.Color, color);
                Assert.AreEqual(piece.Position, new Position(x, row));
            }

            Assert.AreEqual(board.GetPiece(new Position(0, row)).Type, PieceType.Rook);
            Assert.AreEqual(board.GetPiece(new Position(1, row)).Type, PieceType.Knight);
            Assert.AreEqual(board.GetPiece(new Position(2, row)).Type, PieceType.Bishop);
            Assert.AreEqual(board.GetPiece(new Position(3, row)).Type, PieceType.Queen);
            Assert.AreEqual(board.GetPiece(new Position(4, row)).Type, PieceType.King);
            Assert.AreEqual(board.GetPiece(new Position(5, row)).Type, PieceType.Bishop);
            Assert.AreEqual(board.GetPiece(new Position(6, row)).Type, PieceType.Knight);
            Assert.AreEqual(board.GetPiece(new Position(7, row)).Type, PieceType.Rook);
        }

        [TestMethod()]
        public void game_kings_Are_equal_board_kings()
        {
            Assert.AreSame(WhiteKing, board.GetPiece(new Position(4, 0)));
            Assert.AreSame(BlackKing, board.GetPiece(new Position(4, 7)));
        }

    }
}