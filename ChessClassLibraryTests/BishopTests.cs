using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Tests
{
    [TestClass()]
    public class BishopTests
    {
        [DataTestMethod()]
        [DataRow("White")]
        [DataRow("Black")]
        public void constructor_correct(string color)
        {
            ChessBoard board = new ChessBoard();
            Piece bishop = new Bishop(color, new Point(0, 2), board);
            Assert.AreEqual(bishop.Color, color);
            Assert.AreEqual(bishop.Name, "Bishop");
            Assert.IsTrue(bishop is FastPiece);
            Assert.AreEqual(bishop.Position, new Point(0, 2));
            Assert.IsTrue(bishop.MoveSet.Contains(new Point(-1, 1)));
            Assert.IsTrue(bishop.MoveSet.Contains(new Point(1, 1)));
            Assert.IsTrue(bishop.MoveSet.Contains(new Point(1, -1)));
            Assert.IsTrue(bishop.MoveSet.Contains(new Point(-1, -1)));

        }

        [DataTestMethod()]
        [DataRow("White", "Black")]
        [DataRow("Black", "White")]
        public void CanMoveTo_true(string color1, string color2)
        {
            ChessBoard board = new ChessBoard();
            Piece bishop = new Bishop(color1, new Point(3, 4), board);
            new Rook(color2, new Point(4, 3), board);
            Assert.IsTrue(bishop.canMoveTo(new Point(1, 2)));
            Assert.IsTrue(bishop.canMoveTo(new Point(2, 3)));
            Assert.IsTrue(bishop.canMoveTo(new Point(4, 3)));
            Assert.IsTrue(bishop.canMoveTo(new Point(2, 5)));
            Assert.IsTrue(bishop.canMoveTo(new Point(4, 5)));
        }

        [DataTestMethod()]
        [DataRow("White", "Black")]
        [DataRow("Black", "White")]
        public void CanMoveTo_false(string color1, string color2)
        {
            ChessBoard board = new ChessBoard();
            Piece bishop = new Bishop(color1, new Point(3, 4), board);
            new Rook(color2, new Point(4, 3), board);
            new Rook(color1, new Point(2, 3), board);
            Assert.IsFalse(bishop.canMoveTo(new Point(1, 2)));
            Assert.IsFalse(bishop.canMoveTo(new Point(2, 3)));
            Assert.IsFalse(bishop.canMoveTo(new Point(5, 2)));
            Assert.IsFalse(bishop.canMoveTo(new Point(3, 4)));
            Assert.IsFalse(bishop.canMoveTo(new Point(4, 4)));
            Assert.IsFalse(bishop.canMoveTo(new Point(3, 5)));
            Assert.IsFalse(bishop.canMoveTo(new Point(7, 3)));
            Assert.IsFalse(bishop.canMoveTo(new Point(-1, 0)));
        }

        [DataTestMethod()]
        [DataRow(2, 5)]
        [DataRow(1, 2)]
        [DataRow(4, 3)]
        public void moveTo_corrent(int x, int y)
        {
            ChessBoard board = new ChessBoard();
            Piece bishop = new Bishop("White", new Point(3, 4), board);
            new Rook("Black", new Point(4, 3), board);
            bishop.moveTo(new Point(x, y));
            Assert.IsNull(board.GetPiece(new Point(3, 4)));
            Assert.AreSame(bishop, board.GetPiece(new Point(x, y)));
        }

        [DataTestMethod()]
        [DataRow(2, 4)]
        [ExpectedException(typeof(ArgumentException))]
        public void moveTo_exception(int x, int y)
        {
            ChessBoard board = new ChessBoard();
            Piece bishop = new Bishop("White", new Point(3, 4), board);
            bishop.moveTo(new Point(x, y));
        }

    }
}