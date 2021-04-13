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
    public class RookTests
    {
        //[DataTestMethod()]
        //[DataRow("White")]
        //[DataRow("Black")]
        //public void constructor_correct(string color)
        //{
        //    ChessBoard board = new ChessBoard();
        //    Piece rook = new Rook(color, new Position(3, 4), board);
        //    Assert.AreEqual(rook.Color, color);
        //    Assert.AreEqual(rook.Name, "Rook");
        //    Assert.IsTrue(rook is FastPiece);
        //    Assert.AreEqual(rook.Position, new Position(3, 4));
        //    Assert.AreSame(rook, board.GetPiece(new Position(3, 4)));
        //    Assert.AreSame(rook.KillSet, rook.MoveSet);
        //    Assert.IsTrue(rook.KillSet.Contains(new Position(0, 1)));
        //    Assert.IsTrue(rook.KillSet.Contains(new Position(1, 0)));
        //    Assert.IsTrue(rook.KillSet.Contains(new Position(0, -1)));
        //    Assert.IsTrue(rook.KillSet.Contains(new Position(-1, 0)));
        //}

        //[DataTestMethod()]
        //[DataRow("White")]
        //[DataRow("Black")]
        //public void canMoveTo_correct(string color)
        //{
        //    ChessBoard board = new ChessBoard();
        //    Piece rook = new Rook(color, new Position(3, 4), board);
        //    Assert.IsTrue(rook.canMoveTo(new Position(3, 2)));
        //    Assert.IsTrue(rook.canMoveTo(new Position(3, 3)));
        //    Assert.IsTrue(rook.canMoveTo(new Position(3, 5)));

        //    Assert.IsTrue(rook.canMoveTo(new Position(0, 4)));
        //    Assert.IsTrue(rook.canMoveTo(new Position(1, 4)));
        //    Assert.IsTrue(rook.canMoveTo(new Position(2, 4)));
        //    Assert.IsTrue(rook.canMoveTo(new Position(4, 4)));
        //    Assert.IsTrue(rook.canMoveTo(new Position(5, 4)));
        //    Assert.IsTrue(rook.canMoveTo(new Position(6, 4)));
        //    Assert.IsTrue(rook.canMoveTo(new Position(7, 4)));
        //    if(color == "White")
        //        Assert.IsTrue(rook.canMoveTo(new Position(3, 6)));
        //    else if(color == "Black")
        //        Assert.IsTrue(rook.canMoveTo(new Position(3, 1)));
        //}

        //[DataTestMethod()]
        //[DataRow("White")]
        //[DataRow("Black")]
        //public void canMoveTo_false(string color)
        //{
        //    ChessBoard board = new ChessBoard();
        //    Piece rook = new Rook(color, new Position(3, 4), board);
        //    Assert.IsFalse(rook.canMoveTo(new Position(3, 0)));
        //    Assert.IsFalse(rook.canMoveTo(new Position(3, 7)));
        //    Assert.IsFalse(rook.canMoveTo(new Position(3, -1)));

        //    if (color == "White")
        //        new Rook("White", new Position(5, 4), board);
        //    else if (color == "Black")
        //        new Rook("Black", new Position(5, 4), board);

        //    Assert.IsFalse(rook.canMoveTo(new Position(6, 4)));
        //    Assert.IsFalse(rook.canMoveTo(new Position(7, 4)));
        //    Assert.IsFalse(rook.canMoveTo(new Position(8, 4)));

        //    Assert.IsFalse(rook.canMoveTo(new Position(3, 4)));
        //    Assert.IsFalse(rook.canMoveTo(new Position(4, 3)));
        //    Assert.IsFalse(rook.canMoveTo(new Position(4, 5)));
        //    Assert.IsFalse(rook.canMoveTo(new Position(2, 3)));
        //    Assert.IsFalse(rook.canMoveTo(new Position(2, 5)));
        //    Assert.IsFalse(rook.canMoveTo(new Position(0, 2)));
        //}

        //[DataTestMethod()]
        //[DataRow(3, 6)]
        //[DataRow(7, 4)]
        //[DataRow(2, 4)]
        //public void moveTo_corrent(int x, int y)
        //{
        //    ChessBoard board = new ChessBoard();
        //    Piece rook = new Rook("White", new Position(3, 4), board);
        //    rook.moveTo(new Position(x, y));
        //    Assert.IsNull(board.GetPiece(new Position(3, 4)));
        //    Assert.AreSame(rook, board.GetPiece(new Position(x, y)));
        //}

        //[DataTestMethod()]
        //[DataRow(2, 5)]
        //[ExpectedException(typeof(ArgumentException))]
        //public void moveTo_exception(int x, int y)
        //{
        //    ChessBoard board = new ChessBoard();
        //    Piece rook = new Rook("White", new Position(3, 4), board);
        //    rook.moveTo(new Position(x, y));
        //}
    }
}
