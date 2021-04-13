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
    public class WhiteKingTests
    {
    // [TestMethod()]
    //    public void constructor_Correct()
    //    {
    //        ChessBoard board = new ChessBoard();
    //        Piece king = board.WhiteKing;
    //        Assert.AreEqual(king.Color, "White");
    //        Assert.AreEqual(king.Name, "King");
    //        Assert.AreSame(king.KillSet, king.MoveSet);
    //        Assert.IsFalse(king.WasMoved);
    //        Assert.IsTrue(king is SlowPiece);
    //        Assert.IsTrue(king.KillSet.Length == 8);
    //        Assert.IsTrue(king.KillSet.Contains(new Position(-1, 1)));
    //        Assert.IsTrue(king.KillSet.Contains(new Position(0, 1)));
    //        Assert.IsTrue(king.KillSet.Contains(new Position(1, 1)));
    //        Assert.IsTrue(king.KillSet.Contains(new Position(-1, 0)));
    //        Assert.IsTrue(king.KillSet.Contains(new Position(1, 0)));
    //        Assert.IsTrue(king.KillSet.Contains(new Position(-1, -1)));
    //        Assert.IsTrue(king.KillSet.Contains(new Position(0, -1)));
    //        Assert.IsTrue(king.KillSet.Contains(new Position(1, -1)));
    //    }

    //    [TestMethod()]
    //    public void isChecked_true()
    //    {
    //        ChessBoard board = new ChessBoard();
    //        board.SetPiece(null, new Position(5, 1));
    //        new Bishop("Black", new Position(7, 3), board);
    //        Assert.IsTrue(board.WhiteKing.IsChecked());
    //    }

    //    [TestMethod()]
    //    public void isChecked_false()
    //    {
    //        ChessBoard board = new ChessBoard();
    //        Assert.IsFalse(board.WhiteKing.IsChecked());
    //    }

    //    [DataTestMethod()]
    //    [DataRow(3, 1)]
    //    [DataRow(4, 1)]
    //    [DataRow(5, 1)]
    //    public void canMoveTo_correct(int x, int y)
    //    {
    //        ChessBoard board = new ChessBoard();
    //        board.SetPiece(null, new Position(x, y));
    //        Assert.IsTrue(board.WhiteKing.canMoveTo(new Position(x, y)));
    //        new BlackPawn(new Position(x, y), board);
    //        Assert.IsTrue(board.WhiteKing.canMoveTo(new Position(x, y)));
    //    }

    //    [DataTestMethod()]
    //    [DataRow(3, 1)]
    //    [DataRow(4, 1)]
    //    [DataRow(5, 1)]
    //    [DataRow(4, 3)]
    //    [DataRow(4, -1)]
    //    public void canMoveTo_false(int x, int y)
    //    {
    //        ChessBoard board = new ChessBoard();
    //        board.SetPiece(null, new Position(4, 1));
    //        board.SetPiece(null, new Position(5, 1));
    //        new BlackPawn(new Position(3, 2), board);
    //        new Bishop("Black", new Position(7, 3), board);
    //        Assert.IsFalse(board.WhiteKing.canMoveTo(new Position(x, y)));
    //    }

    //    [DataTestMethod()]
    //    [DataRow(3, 1)]
    //    [DataRow(4, 1)]
    //    [DataRow(5, 1)]
    //    public void moveToCorrect(int x, int y)
    //    {
    //        ChessBoard board = new ChessBoard();
    //        board.SetPiece(null, new Position(3, 1));
    //        board.SetPiece(null, new Position(4, 1));
    //        new BlackPawn(new Position(5, 1), board);
    //        board.WhiteKing.moveTo(new Position(x, y));
    //        Assert.IsNull(board.GetPiece(new Position(4, 0)));
    //        Assert.AreSame(board.GetPiece(new Position(x, y)), board.WhiteKing);
    //        Assert.AreEqual(board.WhiteKing.Position, new Position(x, y));
    //    }

    //    [DataTestMethod()]
    //    [DataRow(3, 1)]
    //    [DataRow(4, 1)]
    //    [DataRow(5, 1)]
    //    [DataRow(4, 3)]
    //    [DataRow(4, -1)]
    //    [ExpectedException(typeof(ArgumentException))]
    //    public void moveTo_exception(int x, int y)
    //    {
    //        ChessBoard board = new ChessBoard();
    //        board.SetPiece(null, new Position(4, 1));
    //        board.SetPiece(null, new Position(5, 1));
    //        new BlackPawn(new Position(3, 2), board);
    //        new Bishop("Black", new Position(7, 3), board);
    //        board.WhiteKing.moveTo(new Position(x, y));
    //    }

    //    [TestMethod()]
    //    public void Left_Castle_Move_correct()
    //    {
    //        ChessBoard board = new ChessBoard();
    //        board.SetPiece(null, new Position(1, 0));
    //        board.SetPiece(null, new Position(2, 0));
    //        board.SetPiece(null, new Position(3, 0));
    //        var rook = board.GetPiece(new Position(0, 0));
    //        Assert.IsTrue(board.WhiteKing.canMoveTo(new Position(2, 0)));
    //        board.WhiteKing.moveTo(new Position(2, 0));
    //        Assert.IsNull(board.GetPiece(new Position(0, 0)));
    //        Assert.IsNull(board.GetPiece(new Position(4, 0)));
    //        Assert.AreSame(board.GetPiece(new Position(3, 0)), rook);
    //        Assert.AreEqual(rook.Position, new Position(3, 0));
    //        Assert.AreSame(board.GetPiece(new Position(2, 0)), board.WhiteKing);
    //        Assert.AreEqual(board.WhiteKing.Position, new Position(2, 0));
    //    }

    //    [TestMethod()]
    //    public void Right_Castle_Move_correct()
    //    {
    //        ChessBoard board = new ChessBoard();
    //        board.SetPiece(null, new Position(5, 0));
    //        board.SetPiece(null, new Position(6, 0));
    //        var rook = board.GetPiece(new Position(7, 0));
    //        Assert.IsTrue(board.WhiteKing.canMoveTo(new Position(6, 0)));
    //        board.WhiteKing.moveTo(new Position(6, 0));
    //        Assert.IsNull(board.GetPiece(new Position(7, 0)));
    //        Assert.IsNull(board.GetPiece(new Position(4, 0)));
    //        Assert.AreSame(board.GetPiece(new Position(5, 0)), rook);
    //        Assert.AreEqual(rook.Position, new Position(5, 0));
    //        Assert.AreSame(board.GetPiece(new Position(6, 0)), board.WhiteKing);
    //        Assert.AreEqual(board.WhiteKing.Position, new Position(6, 0));
    //    }
    //}

    //[TestClass()]
    //public class BlackKingTests
    //{
    //    [TestMethod()]
    //    public void constructor_Correct()
    //    {
    //        ChessBoard board = new ChessBoard();
    //        Piece king = board.BlackKing;
    //        Assert.AreEqual(king.Color, "Black");
    //        Assert.AreEqual(king.Name, "King");
    //        Assert.AreSame(king.KillSet, king.MoveSet);
    //        Assert.IsFalse(king.WasMoved);
    //        Assert.IsTrue(king is SlowPiece);
    //        Assert.IsTrue(king.KillSet.Length == 8);
    //        Assert.IsTrue(king.KillSet.Contains(new Position(-1, 1)));
    //        Assert.IsTrue(king.KillSet.Contains(new Position(0, 1)));
    //        Assert.IsTrue(king.KillSet.Contains(new Position(1, 1)));
    //        Assert.IsTrue(king.KillSet.Contains(new Position(-1, 0)));
    //        Assert.IsTrue(king.KillSet.Contains(new Position(1, 0)));
    //        Assert.IsTrue(king.KillSet.Contains(new Position(-1, -1)));
    //        Assert.IsTrue(king.KillSet.Contains(new Position(0, -1)));
    //        Assert.IsTrue(king.KillSet.Contains(new Position(1, -1)));
    //    }

    //    [TestMethod()]
    //    public void isChecked_true()
    //    {
    //        ChessBoard board = new ChessBoard();
    //        board.SetPiece(null, new Position(5, 6));
    //        new Bishop("White", new Position(7, 4), board);
    //        Assert.IsTrue(board.BlackKing.IsChecked());
    //    }

    //    [TestMethod()]
    //    public void isChecked_false()
    //    {
    //        ChessBoard board = new ChessBoard();
    //        Assert.IsFalse(board.BlackKing.IsChecked());
    //    }

    //    [DataTestMethod()]
    //    [DataRow(3, 6)]
    //    [DataRow(4, 6)]
    //    [DataRow(5, 6)]
    //    public void canMoveTo_correct(int x, int y)
    //    {
    //        ChessBoard board = new ChessBoard();
    //        board.SetPiece(null, new Position(x, y));
    //        Assert.IsTrue(board.BlackKing.canMoveTo(new Position(x, y)));
    //        new WhitePawn(new Position(x, y), board);
    //        Assert.IsTrue(board.BlackKing.canMoveTo(new Position(x, y)));
    //    }

    //    [DataTestMethod()]
    //    [DataRow(3, 6)]
    //    [DataRow(4, 6)]
    //    [DataRow(5, 6)]
    //    [DataRow(4, 4)]
    //    [DataRow(4, 8)]
    //    public void canMoveTo_false(int x, int y)
    //    {
    //        ChessBoard board = new ChessBoard();
    //        board.SetPiece(null, new Position(4, 6));
    //        board.SetPiece(null, new Position(5, 6));
    //        new WhitePawn(new Position(3, 5), board);
    //        new Bishop("White", new Position(7, 4), board);
    //        Assert.IsFalse(board.BlackKing.canMoveTo(new Position(x, y)));
    //    }

    //    [DataTestMethod()]
    //    [DataRow(3, 6)]
    //    [DataRow(4, 6)]
    //    [DataRow(5, 6)]
    //    public void moveToCorrect(int x, int y)
    //    {
    //        ChessBoard board = new ChessBoard();
    //        board.SetPiece(null, new Position(3, 6));
    //        board.SetPiece(null, new Position(4, 6));
    //        new WhitePawn(new Position(5, 6), board);
    //        board.BlackKing.moveTo(new Position(x, y));
    //        Assert.IsNull(board.GetPiece(new Position(4, 7)));
    //        Assert.AreSame(board.GetPiece(new Position(x, y)), board.BlackKing);
    //        Assert.AreEqual(board.BlackKing.Position, new Position(x, y));
    //    }

    //    [DataTestMethod()]
    //    [DataRow(3, 6)]
    //    [DataRow(4, 6)]
    //    [DataRow(5, 6)]
    //    [DataRow(4, 4)]
    //    [DataRow(4, 8)]
    //    [ExpectedException(typeof(ArgumentException))]
    //    public void moveTo_exception(int x, int y)
    //    {
    //        ChessBoard board = new ChessBoard();
    //        board.SetPiece(null, new Position(4, 6));
    //        board.SetPiece(null, new Position(5, 6));
    //        new WhitePawn(new Position(3, 5), board);
    //        new Bishop("White", new Position(7, 4), board);
    //        board.BlackKing.moveTo(new Position(x, y));
    //    }

    //    [TestMethod()]
    //    public void Left_Castle_Move_correct()
    //    {
    //        ChessBoard board = new ChessBoard();
    //        board.SetPiece(null, new Position(1, 7));
    //        board.SetPiece(null, new Position(2, 7));
    //        board.SetPiece(null, new Position(3, 7));
    //        var rook = board.GetPiece(new Position(0, 7));
    //        Assert.IsTrue(board.BlackKing.canMoveTo(new Position(2, 7)));
    //        board.BlackKing.moveTo(new Position(2, 7));
    //        Assert.IsNull(board.GetPiece(new Position(0, 7)));
    //        Assert.IsNull(board.GetPiece(new Position(4, 7)));
    //        Assert.AreSame(board.GetPiece(new Position(3, 7)), rook);
    //        Assert.AreEqual(rook.Position, new Position(3, 7));
    //        Assert.AreSame(board.GetPiece(new Position(2, 7)), board.BlackKing);
    //        Assert.AreEqual(board.BlackKing.Position, new Position(2, 7));
    //    }

    //    [TestMethod()]
    //    public void Right_Castle_Move_correct()
    //    {
    //        ChessBoard board = new ChessBoard();
    //        board.SetPiece(null, new Position(5, 7));
    //        board.SetPiece(null, new Position(6, 7));
    //        var rook = board.GetPiece(new Position(7, 7));
    //        Assert.IsTrue(board.BlackKing.canMoveTo(new Position(6, 7)));
    //        board.BlackKing.moveTo(new Position(6, 7));
    //        Assert.IsNull(board.GetPiece(new Position(7, 7)));
    //        Assert.IsNull(board.GetPiece(new Position(4, 7)));
    //        Assert.AreSame(board.GetPiece(new Position(5, 7)), rook);
    //        Assert.AreEqual(rook.Position, new Position(5, 7));
    //        Assert.AreSame(board.GetPiece(new Position(6, 7)), board.BlackKing);
    //        Assert.AreEqual(board.BlackKing.Position, new Position(6, 7));
    //    }
    }
}