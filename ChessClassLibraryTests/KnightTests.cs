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
    public class KnightTests
    {
        [DataTestMethod()]
        [DataRow("White")]
        [DataRow("Black")]
        public void constructor_correct(string color)
        {
            ChessBoard board = new ChessBoard();
            Piece knight = new Knight(color, new Point(0, 2), board);
            Assert.AreEqual(knight.Color, color);
            Assert.AreEqual(knight.Name, "Knight");
            Assert.IsTrue(knight is SlowPiece);
            Assert.AreEqual(knight.Position, new Point(0, 2));
            Assert.IsTrue(knight.MoveSet.Contains(new Point(-1, 2)));
            Assert.IsTrue(knight.MoveSet.Contains(new Point(1, 2)));

            Assert.IsTrue(knight.MoveSet.Contains(new Point(2, 1)));
            Assert.IsTrue(knight.MoveSet.Contains(new Point(2, -1)));

            Assert.IsTrue(knight.MoveSet.Contains(new Point(-2, 1)));
            Assert.IsTrue(knight.MoveSet.Contains(new Point(-2, -1)));

            Assert.IsTrue(knight.MoveSet.Contains(new Point(-1, -2)));
            Assert.IsTrue(knight.MoveSet.Contains(new Point(1, -2)));
        }

        [DataTestMethod()]
        [DataRow("White", "Black")]
        [DataRow("Black", "White")]
        public void CanMoveTo_true(string color1, string color2)
        {
            ChessBoard board = new ChessBoard();
            Piece knight = new Knight(color1, new Point(2, 3), board);
            new Rook(color2, new Point(3, 5), board);
            Assert.IsTrue(knight.canMoveTo(new Point(3, 5)));
            Assert.IsTrue(knight.canMoveTo(new Point(4, 4)));
            Assert.IsTrue(knight.canMoveTo(new Point(4, 2)));
        }

        [DataTestMethod()]
        [DataRow("White")]
        [DataRow("Black")]
        public void CanMoveTo_false(string color)
        {
            ChessBoard board = new ChessBoard();
            Piece knight = new Knight(color, new Point(0, 3), board);
            new Rook(color, new Point(1, 5), board);
            Assert.IsFalse(knight.canMoveTo(new Point(1, 5)));
            Assert.IsFalse(knight.canMoveTo(new Point(0, 3)));
            Assert.IsFalse(knight.canMoveTo(new Point(-1, 5)));
        }

    }
}