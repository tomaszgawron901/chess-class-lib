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
    public class QueenTests
    {
        [DataTestMethod()]
        [DataRow("White")]
        [DataRow("Black")]
        public void constructor_correct(string color)
        {
            ChessBoard board = new ChessBoard();
            Piece queen = new Queen(color, new Point(3, 4), board);
            Assert.AreEqual(queen.Color, color);
            Assert.AreEqual(queen.Name, "Queen");
            Assert.IsTrue(queen is FastPiece);
            Assert.AreEqual(queen.Position, new Point(3, 4));
            Assert.AreSame(queen, board.GetPiece(new Point(3, 4)));
            Assert.AreSame(queen.KillSet, queen.MoveSet);
            Assert.IsTrue(queen.KillSet.Length == 8);
            Assert.IsTrue(queen.KillSet.Contains(new Point(-1, 1)));
            Assert.IsTrue(queen.KillSet.Contains(new Point(0, 1)));
            Assert.IsTrue(queen.KillSet.Contains(new Point(1, 1)));
            Assert.IsTrue(queen.KillSet.Contains(new Point(-1, 0)));
            Assert.IsTrue(queen.KillSet.Contains(new Point(1, 0)));
            Assert.IsTrue(queen.KillSet.Contains(new Point(-1, -1)));
            Assert.IsTrue(queen.KillSet.Contains(new Point(0, -1)));
            Assert.IsTrue(queen.KillSet.Contains(new Point(1, -1)));
        }

        [DataTestMethod()]
        [DataRow("White", "Black")]
        [DataRow("Black", "White")]
        public void canMoveTo_correct(string color1, string color2)
        {
            ChessBoard board = new ChessBoard();
            Piece queen = new Queen(color1, new Point(3, 4), board);
            new Rook(color2, new Point(4, 3), board);
            new Rook(color2, new Point(6, 4), board);
            Assert.IsTrue(queen.canMoveTo(new Point(2, 5)));
            Assert.IsTrue(queen.canMoveTo(new Point(3, 5)));
            Assert.IsTrue(queen.canMoveTo(new Point(4, 5)));

            Assert.IsTrue(queen.canMoveTo(new Point(4, 4)));
            Assert.IsTrue(queen.canMoveTo(new Point(5, 4)));
            Assert.IsTrue(queen.canMoveTo(new Point(6, 4)));

            Assert.IsTrue(queen.canMoveTo(new Point(0, 4)));
            Assert.IsTrue(queen.canMoveTo(new Point(1, 4)));

            Assert.IsTrue(queen.canMoveTo(new Point(2, 4)));
            Assert.IsTrue(queen.canMoveTo(new Point(3, 3)));
            Assert.IsTrue(queen.canMoveTo(new Point(3, 2)));

            Assert.IsTrue(queen.canMoveTo(new Point(2, 3)));
            Assert.IsTrue(queen.canMoveTo(new Point(1, 2)));

            Assert.IsTrue(queen.canMoveTo(new Point(4, 3)));

        }

        [DataTestMethod()]
        [DataRow("White", "Black")]
        [DataRow("Black", "White")]
        public void canMoveTo_false(string color1, string color2)
        {
            ChessBoard board = new ChessBoard();
            Piece queen = new Queen(color1, new Point(3, 4), board);
            new Rook(color2, new Point(4, 3), board);
            new Rook(color1, new Point(6, 4), board);
            Assert.IsFalse(queen.canMoveTo(new Point(3, 4)));
            Assert.IsFalse(queen.canMoveTo(new Point(6, 4)));
            Assert.IsFalse(queen.canMoveTo(new Point(-1, 4)));
            Assert.IsFalse(queen.canMoveTo(new Point(7, 4)));
            Assert.IsFalse(queen.canMoveTo(new Point(5, 2)));
            Assert.IsFalse(queen.canMoveTo(new Point(2, 2)));
            Assert.IsFalse(queen.canMoveTo(new Point(1, 3)));

        }

        [DataTestMethod()]
        [DataRow(4, 3)]
        [DataRow(0, 4)]
        [DataRow(2, 5)]
        public void moveTo_corrent(int x, int y)
        {
            ChessBoard board = new ChessBoard();
            Piece queen = new Queen("White", new Point(3, 4), board);
            queen.moveTo(new Point(x, y));
            Assert.IsNull(board.GetPiece(new Point(3, 4)));
            Assert.AreSame(queen, board.GetPiece(new Point(x, y)));
        }

        [DataTestMethod()]
        [DataRow(4, 2)]
        [ExpectedException(typeof(ArgumentException))]
        public void moveTo_exception(int x, int y)
        {
            ChessBoard board = new ChessBoard();
            Piece queen = new Queen("White", new Point(3, 4), board);
            queen.moveTo(new Point(x, y));
        }
    }
}
