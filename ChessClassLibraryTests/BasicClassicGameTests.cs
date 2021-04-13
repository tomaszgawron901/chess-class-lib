using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessClassLibrary.Games.ClassicGame;
using ChessClassLibrary.enums;

namespace ChessClassLibrary.Tests
{
    [TestClass()]
    public class BasicClassicGameTests: ClassicGame
    {
        public BasicClassicGameTests() { }


        [TestMethod()]
        public void constructor_correct()
        {
            Assert.IsNotNull(board);
            Assert.AreEqual(currentPlayerColor, PieceColor.White);
            Assert.AreEqual(GameState, GameState.NotStarted);
        }

        [TestMethod]
        public void change_player_correct()
        {
            var currentColor = currentPlayerColor;
            SwapPlayers();
            Assert.AreNotEqual(currentColor, currentPlayerColor);
            currentColor = currentPlayerColor;
            SwapPlayers();
            Assert.AreNotEqual(currentColor, currentPlayerColor);
        }

        [TestMethod()]
        public void cannot_move_enemy_pieces()
        {
            Assert.IsFalse(CanPerformMove(new BoardMove(new Position(0, 6), new Position(0, 5))));
            Assert.IsFalse(CanPerformMove(new BoardMove(new Position(2, 6), new Position(2, 5))));
            Assert.IsFalse(CanPerformMove(new BoardMove(new Position(3, 6), new Position(3, 5))));
            Assert.IsFalse(CanPerformMove(new BoardMove(new Position(4, 6), new Position(4, 5))));
            SwapPlayers();
            Assert.IsFalse(CanPerformMove(new BoardMove(new Position(0, 1), new Position(0, 2))));
            Assert.IsFalse(CanPerformMove(new BoardMove(new Position(2, 1), new Position(2, 2))));
            Assert.IsFalse(CanPerformMove(new BoardMove(new Position(3, 1), new Position(3, 2))));
            Assert.IsFalse(CanPerformMove(new BoardMove(new Position(4, 1), new Position(4, 2))));
        }

        [TestMethod()]
        public void can_move_own_pieces()
        {
            Assert.IsTrue(CanPerformMove(new BoardMove(new Position(0, 1), new Position(0, 2))));
            Assert.IsTrue(CanPerformMove(new BoardMove(new Position(2, 1), new Position(2, 2))));
            Assert.IsTrue(CanPerformMove(new BoardMove(new Position(3, 1), new Position(3, 2))));
            Assert.IsTrue(CanPerformMove(new BoardMove(new Position(4, 1), new Position(4, 2))));
            SwapPlayers();
            Assert.IsTrue(CanPerformMove(new BoardMove(new Position(0, 6), new Position(0, 5))));
            Assert.IsTrue(CanPerformMove(new BoardMove(new Position(2, 6), new Position(2, 5))));
            Assert.IsTrue(CanPerformMove(new BoardMove(new Position(3, 6), new Position(3, 5))));
            Assert.IsTrue(CanPerformMove(new BoardMove(new Position(4, 6), new Position(4, 5))));
        }

        //    [TestMethod]
        //    public void update_state()
        //    {
        //        Game game = new Game();
        //        game.UpdateState();
        //        Assert.AreEqual(game.GameState, game.Board.GetState());
        //        game.Board.GetPiece(3, 1).moveTo(3, 2);
        //        Assert.AreEqual(game.GameState, game.Board.GetState());
        //    }

        //    [TestMethod()]
        //    public void example_game()
        //    {
        //        Game game = new Game();
        //        Assert.AreEqual(game.GameState, GameStates.inProgress);
        //        Assert.AreEqual(game.PlayerTurn, Players.WhitePlayer);
        //        game.Board.GetPiece(5, 1).moveTo(5, 2);
        //        Assert.AreEqual(game.GameState, GameStates.inProgress);
        //        Assert.AreEqual(game.PlayerTurn, Players.BlackPlayer);
        //        game.Board.GetPiece(4, 6).moveTo(4, 4);
        //        Assert.AreEqual(game.GameState, GameStates.inProgress);
        //        Assert.AreEqual(game.PlayerTurn, Players.WhitePlayer);
        //        game.Board.GetPiece(6, 1).moveTo(6, 3);
        //        Assert.AreEqual(game.GameState, GameStates.inProgress);
        //        Assert.AreEqual(game.PlayerTurn, Players.BlackPlayer);
        //        game.Board.GetPiece(3, 7).moveTo(7, 3);
        //        Assert.AreEqual(game.GameState, GameStates.blackWin);
        //        Assert.AreEqual(game.PlayerTurn, Players.WhitePlayer);
        //    }
        //}

        //[TestClass()]
        //public class BoardManagerTests
        //{
        //    [TestMethod()]
        //    public void constructor()
        //    {
        //        BoardManager bm = new Game().Board;
        //        Assert.AreEqual(bm.BoardHeight, 8);
        //        Assert.AreEqual(bm.BoardWidth, 8);
        //    }

        //    [TestMethod()]
        //    public void getPiece()
        //    {
        //        BoardManager bm = new Game().Board;
        //        ChessBoard cb = new ChessBoard();
        //        for(int x = 0; x < 8; x++)
        //        {
        //            for(int y= 0; y<8; y++)
        //            {
        //                if (cb.GetPiece(new Position(x, y)) is null)
        //                    Assert.IsTrue(bm.GetPiece(x, y) is EmptyPieceManager);
        //                else
        //                    Assert.IsTrue(bm.GetPiece(x, y) is PieceManager);
        //            }
        //        }
        //    }

        //    [TestMethod()]
        //    public void foreach_correct()
        //    {
        //        BoardManager bm = new Game().Board;
        //        ChessBoard cb = new ChessBoard();
        //        int capacity = 0;
        //        foreach(var pm in bm)
        //        {
        //            capacity += 1;
        //            if(pm is EmptyPieceManager)
        //            {
        //                Assert.AreEqual(pm.Owner, Players.None);
        //                Assert.AreEqual(pm.Type, PieceTypes.Empty);
        //                Assert.IsNull(cb.GetPiece(new Position(pm.PieceXPosition, pm.PieceYPosition)));
        //            }
        //            else
        //            {
        //                Piece piece = cb.GetPiece(new Position(pm.PieceXPosition, pm.PieceYPosition));
        //                switch (pm.Owner)
        //                {
        //                    case Players.WhitePlayer:
        //                        Assert.IsTrue(piece.Color == "White");
        //                        break;
        //                    case Players.BlackPlayer:
        //                        Assert.IsTrue(piece.Color == "Black");
        //                        break;
        //                }
        //                switch (pm.Type)
        //                {
        //                    case PieceTypes.Pawn:
        //                        Assert.IsTrue(piece.Name == "Pawn");
        //                        break;
        //                    case PieceTypes.Rook:
        //                        Assert.IsTrue(piece.Name == "Rook");
        //                        break;
        //                    case PieceTypes.Knight:
        //                        Assert.IsTrue(piece.Name == "Knight");
        //                        break;
        //                    case PieceTypes.Bishop:
        //                        Assert.IsTrue(piece.Name == "Bishop");
        //                        break;
        //                    case PieceTypes.Queen:
        //                        Assert.IsTrue(piece.Name == "Queen");
        //                        break;
        //                    case PieceTypes.King:
        //                        Assert.IsTrue(piece.Name == "King");
        //                        break;
        //                }
        //            }
        //        }
        //        Assert.AreEqual(capacity, bm.BoardHeight * bm.BoardWidth);
        //    }
        //}

        //[TestClass()]
        //public class PieceManagerTests
        //{
        //    [TestMethod]
        //    public void constructor_Empty()
        //    {
        //        EmptyPieceManager pm = new EmptyPieceManager(0, 0, new Game());
        //        Assert.IsTrue(pm.Owner == Players.None);
        //        Assert.IsTrue(pm.Type == PieceTypes.Empty);
        //    }

        //    [DataTestMethod()]
        //    [DataRow(2, 2)]
        //    [DataRow(0, 7)]
        //    [DataRow(7, 0)]
        //    public void position_Correct(int x, int y)
        //    {
        //        IPieceManager pm = new EmptyPieceManager(x, y, new Game());
        //        Assert.AreEqual(pm.PieceXPosition, x);
        //        Assert.AreEqual(pm.PieceYPosition, y);
        //        pm = new PieceManager(new WhitePawn(new Position(x, y), new ChessBoard()), new Game());
        //        Assert.AreEqual(pm.PieceXPosition, x);
        //        Assert.AreEqual(pm.PieceYPosition, y);
        //    }

        //    [TestMethod()]
        //    public void constructor_whitePawn()
        //    {
        //        Piece piece = new WhitePawn(new Position(0, 0), new ChessBoard());
        //        PieceManager pm = new PieceManager(piece, new Game());
        //        Assert.AreEqual(pm.Owner, Players.WhitePlayer);
        //        Assert.AreEqual(pm.Type, PieceTypes.Pawn);
        //    }

        //    [TestMethod()]
        //    public void constructor_whiteRook()
        //    {
        //        Piece piece = new Rook("White", new Position(0, 0), new ChessBoard());
        //        PieceManager pm = new PieceManager(piece, new Game());
        //        Assert.AreEqual(pm.Owner, Players.WhitePlayer);
        //        Assert.AreEqual(pm.Type, PieceTypes.Rook);
        //    }

        //    [TestMethod()]
        //    public void constructor_whiteKnight()
        //    {
        //        Piece piece = new Knight("White", new Position(0, 0), new ChessBoard());
        //        PieceManager pm = new PieceManager(piece, new Game());
        //        Assert.AreEqual(pm.Owner, Players.WhitePlayer);
        //        Assert.AreEqual(pm.Type, PieceTypes.Knight);
        //    }

        //    [TestMethod()]
        //    public void constructor_whiteBishop()
        //    {
        //        Piece piece = new Bishop("White", new Position(0, 0), new ChessBoard());
        //        PieceManager pm = new PieceManager(piece, new Game());
        //        Assert.AreEqual(pm.Owner, Players.WhitePlayer);
        //        Assert.AreEqual(pm.Type, PieceTypes.Bishop);
        //    }

        //    [TestMethod()]
        //    public void constructor_whiteQueen()
        //    {
        //        Piece piece = new Queen("White", new Position(0, 0), new ChessBoard());
        //        PieceManager pm = new PieceManager(piece, new Game());
        //        Assert.AreEqual(pm.Owner, Players.WhitePlayer);
        //        Assert.AreEqual(pm.Type, PieceTypes.Queen);
        //    }

        //    [TestMethod()]
        //    public void constructor_whiteKing()
        //    {
        //        Piece piece = new ChessBoard().GetPiece(new Position(4, 0));
        //        PieceManager pm = new PieceManager(piece, new Game());
        //        Assert.AreEqual(pm.Owner, Players.WhitePlayer);
        //        Assert.AreEqual(pm.Type, PieceTypes.King);
        //    }

        //    [TestMethod()]
        //    public void constructor_blackPawn()
        //    {
        //        Piece piece = new BlackPawn(new Position(7, 7), new ChessBoard());
        //        PieceManager pm = new PieceManager(piece, new Game());
        //        Assert.AreEqual(pm.Owner, Players.BlackPlayer);
        //        Assert.AreEqual(pm.Type, PieceTypes.Pawn);
        //    }

        //    [TestMethod()]
        //    public void constructor_blackRook()
        //    {
        //        Piece piece = new Rook("Black", new Position(0, 0), new ChessBoard());
        //        PieceManager pm = new PieceManager(piece, new Game());
        //        Assert.AreEqual(pm.Owner, Players.BlackPlayer);
        //        Assert.AreEqual(pm.Type, PieceTypes.Rook);
        //    }

        //    [TestMethod()]
        //    public void constructor_blackKnight()
        //    {
        //        Piece piece = new Knight("Black", new Position(0, 0), new ChessBoard());
        //        PieceManager pm = new PieceManager(piece, new Game());
        //        Assert.AreEqual(pm.Owner, Players.BlackPlayer);
        //        Assert.AreEqual(pm.Type, PieceTypes.Knight);
        //    }

        //    [TestMethod()]
        //    public void constructor_blackBishop()
        //    {
        //        Piece piece = new Bishop("Black", new Position(0, 0), new ChessBoard());
        //        PieceManager pm = new PieceManager(piece, new Game());
        //        Assert.AreEqual(pm.Owner, Players.BlackPlayer);
        //        Assert.AreEqual(pm.Type, PieceTypes.Bishop);
        //    }

        //    [TestMethod()]
        //    public void constructor_blackQueen()
        //    {
        //        Piece piece = new Queen("Black", new Position(0, 0), new ChessBoard());
        //        PieceManager pm = new PieceManager(piece, new Game());
        //        Assert.AreEqual(pm.Owner, Players.BlackPlayer);
        //        Assert.AreEqual(pm.Type, PieceTypes.Queen);
        //    }

        //    [TestMethod()]
        //    public void constructor_blackKing()
        //    {
        //        Piece piece = new ChessBoard().GetPiece(new Position(4, 7));
        //        PieceManager pm = new PieceManager(piece, new Game());
        //        Assert.AreEqual(pm.Owner, Players.BlackPlayer);
        //        Assert.AreEqual(pm.Type, PieceTypes.King);
        //    }

        //    [TestMethod()]
        //    public void canMoveTo_true()
        //    {
        //        Game game = new Game();
        //        Assert.IsTrue(game.Board.GetPiece(5, 1).canMoveTo(5, 2));
        //        Assert.IsTrue(game.Board.GetPiece(5, 1).canMoveTo(5, 3));
        //        game.Board.GetPiece(5, 1).moveTo(5, 3);
        //        Assert.IsTrue(game.Board.GetPiece(6, 7).canMoveTo(5, 5));
        //        Assert.IsTrue(game.Board.GetPiece(6, 7).canMoveTo(7, 5));
        //    }

        //    [TestMethod()]
        //    public void canMoveTo_false()
        //    {
        //        Game game = new Game();
        //        Assert.IsFalse(game.Board.GetPiece(5, 1).canMoveTo(6, 2));
        //        Assert.IsFalse(game.Board.GetPiece(5, 1).canMoveTo(6, 1));
        //        Assert.IsFalse(game.Board.GetPiece(5, 6).canMoveTo(6, 6));
        //        game.Board.GetPiece(5, 1).moveTo(5, 2);
        //        Assert.IsFalse(game.Board.GetPiece(5, 2).canMoveTo(5, 3));
        //    }

        //    [TestMethod()]
        //    public void moveTo_corrcet()
        //    {
        //        Game game = new Game();
        //        IPieceManager piece = game.Board.GetPiece(5, 1);
        //        piece.moveTo(5, 2);
        //        Assert.IsTrue(game.Board.GetPiece(5, 1) is EmptyPieceManager);
        //        Assert.AreEqual(piece.Owner, game.Board.GetPiece(5, 2).Owner);
        //        Assert.AreEqual(piece.Type, game.Board.GetPiece(5, 2).Type);
        //    }

        //    [TestMethod()]
        //    [ExpectedException(typeof(Exception))]
        //    public void moveTo_exception_enotherPlayerTurn()
        //    {
        //        Game game = new Game();
        //        game.Board.GetPiece(6, 6).moveTo(6, 5);
        //    }

        //    [TestMethod()]
        //    [ExpectedException(typeof(Exception))]
        //    public void moveTo_exception_wrong_position()
        //    {
        //        Game game = new Game();
        //        game.Board.GetPiece(6, 1).moveTo(5, 2);
        //    }

        //    [DataTestMethod()]
        //    [DataRow(3, 3)]
        //    [DataRow(0, 7)]
        //    [DataRow(7, 0)]
        //    public void get_piece_position_correct(int x, int y)
        //    {
        //        PieceManager pm = new PieceManager(new WhitePawn(new Position(x, y), new ChessBoard()), new Game());
        //        Assert.AreEqual(pm.PieceXPosition, x);
        //        Assert.AreEqual(pm.PieceYPosition, y);
        //    }
    }
}