using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.Exceptions;
using ChessClassLibrary.Logic;
using ChessClassLibrary.Logic.Containers;
using ChessClassLibrary.Logic.PieceTransformation;
using ChessClassLibrary.Logic.Rules;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using ChessClassLibrary.Pieces.FasePieces;
using ChessClassLibrary.Pieces.SlowPieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Games
{
    public interface IClassicGame : IGame
    {
        ClassicBoard Board { get; }
    }

    /// <summary>
    /// Base Game with two kings on Rectangular Board.
    /// </summary>
    public abstract class BaseClassicGame : IClassicGame
    {
        public ClassicBoard Board { get; protected set; }
        public PieceColor CurrentPlayerColor { get; protected set; }
        public GameState GameState { get; protected set; }

        public ProtectedPieceRule WhiteKing { get; protected set; }
        public ProtectedPieceRule BlackKing { get; protected set; }


        public BaseClassicGame()
        {
            CreateBoard();
            CurrentPlayerColor = PieceColor.White;
            GameState = GameState.NotStarted;
        }

        /// <summary>
        /// Updates the Game status.
        /// </summary>
        public void UpdateGameStatus()
        {
            WhiteKing.UpdateState();
            BlackKing.UpdateState();
            if (
                WhiteKing.IsCheckmated
                || WhiteKing.IsStalemated
                || BlackKing.IsCheckmated
                || BlackKing.IsStalemated
                || InsufficientMatingMaterial())
            {
                GameState = GameState.Ended;
            }
        }

        /// <summary>
        /// Checks if there are enough Pieces to play the Game. 
        /// </summary>
        /// <returns></returns>
        protected abstract bool InsufficientMatingMaterial();

        /// <summary>
        /// Creates Board and populates it with Pieces.
        /// </summary>
        protected abstract void CreateBoard();

        /// <summary>
        /// Swap players.
        /// </summary>
        public void SwapPlayers()
        {
            if (CurrentPlayerColor == PieceColor.White)
            {
                CurrentPlayerColor = PieceColor.Black;
            }
            else
            {
                CurrentPlayerColor = PieceColor.White;
            }
        }

        /// <summary>
        /// Check if BoardMove can be performed.
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public bool CanPerformMove(BoardMove move)
        {
            IPiece pickedPiece = Board.GetPiece(move.Current);
            if (pickedPiece != null && pickedPiece.Color == CurrentPlayerColor)
            {
                return pickedPiece.GetMoveTo(move.Destination) != null;
            }
            return false;
        }

        /// <summary>
        /// Tries to perform BoardMoves. If move cannot be performed throws CouldNotPerformMoveException.
        /// </summary>
        /// <param name="move"></param>
        public virtual void TryPerformMove(BoardMove move)
        {
            if (CanPerformMove(move))
            {
                PerformMove(move);
            }
            else
            {
                throw new CouldNotPerformMoveException();
            }
        }

        /// <summary>
        /// Performs BoardMove.
        /// </summary>
        /// <param name="move"></param>
        public virtual void PerformMove(BoardMove move)
        {
            if (GameState == GameState.NotStarted)
            {
                GameState = GameState.InProgress;
            }
            Board.GetPiece(move.Current).MoveToPosition(move.Destination);
            AfterMovePerformed();
        }

        /// <summary>
        /// Method called after move performed. Updates game status and swaps players.
        /// </summary>
        public void AfterMovePerformed()
        {
            UpdateGameStatus();
            SwapPlayers();
        }

        /// <summary>
        /// Gets available moves of Piece at given Position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public IEnumerable<PieceMove> GetPieceMoveSetAtPosition(Position position)
        {
            var piece = Board.GetPiece(position);
            if (piece == null) { return Enumerable.Empty<PieceMove>(); }

            return piece.MoveSet;
        }

        /// <summary>
        /// Gets winner color or null if game still in progress or ended by stalemate.
        /// </summary>
        /// <returns></returns>
        public PieceColor? GetWinner()
        {
            if (this.WhiteKing.IsCheckmated)
            {
                return PieceColor.Black;
            }
            if (this.BlackKing.IsCheckmated)
            {
                return PieceColor.White;
            }
            return null;
        }


        /// <summary>
        /// Clears row at given index.
        /// </summary>
        /// <param name="row"></param>
        protected void InsertEmptyRow(int row)
        {
            for (int i = 0; i < Board.Width; i++)
            {
                Board.SetPiece(null, new Position(1, row));
            }
        }

        /// <summary>
        /// Fill row with given index with Pawns with given PieceColor.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="row"></param>
        protected void InsertPawnRow(PieceColor color, int row)
        {
            for (int i = 0; i < Board.Width; i++)
            {
                Board.SetPiece(CreatePawn(color, new Position(i, row)));
            }
        }


        #region Piece Creator Helpers

        protected BasePieceDecorator CreateSlowPiece(IPiece piece)
        {
            return new KillRule(new MoveRule(new PieceOnBoard(piece, Board)));
        }

        protected BasePieceDecorator CreateFastPiece(IPiece piece)
        {
            return new KillRule(new MoveRule(new FastPieceOnBoard(piece, Board)));
        }

        #region Protector
        protected BasePieceDecorator CreateWhiteProtector(BasePieceDecorator piece)
        {
            return new ProtectAttackRule(piece, WhiteKing, BlackKing);
        }

        protected BasePieceDecorator CreateBlackProtector(BasePieceDecorator piece)
        {
            return new ProtectAttackRule(piece, BlackKing, WhiteKing);
        }

        protected BasePieceDecorator CreateProtector(BasePieceDecorator piece)
        {
            if (piece.Color == PieceColor.White)
            {
                return this.CreateWhiteProtector(piece);
            }
            else if (piece.Color == PieceColor.Black)
            {
                return this.CreateBlackProtector(piece);
            }
            return piece;
        }
        #endregion Protector

        #endregion

        #region Piece Creators
        protected BasePieceDecorator CreatePawn(PieceColor color, Position position)
        {
            if (color == PieceColor.White)
            {
                var currentPiece = new WhitePawnFirstMoveRule(new PieceOnBoard(new WhitePawn(position), Board));
                var afterPiece = new FastPieceOnBoard(new Queen(PieceColor.White, position), Board);
                IEnumerable<Position> positions = Enumerable.Range(0, Board.Width).Select(x => new Position(x, Board.Height - 1));

                return CreateProtector(new KillRule(new MoveRule(new AfterMoveToPositionTransformation(currentPiece, afterPiece, positions))));
            }
            else if (color == PieceColor.Black)
            {
                var currentPiece = new BlackPawnFirstMoveRule(new PieceOnBoard(new BlackPawn(position), Board));
                var afterPiece = new FastPieceOnBoard(new Queen(PieceColor.Black, position), Board);
                IEnumerable<Position> positions = Enumerable.Range(0, Board.Width).Select(x => new Position(x, 0));

                return CreateProtector(new KillRule(new MoveRule(new AfterMoveToPositionTransformation(currentPiece, afterPiece, positions))));
            }
            throw new Exception();
        }

        protected BasePieceDecorator CreateRook(PieceColor color, Position position)
        {
            return CreateProtector(CreateFastPiece(new Rook(color, position)));
        }

        protected BasePieceDecorator CreateBishop(PieceColor color, Position position)
        {
            return CreateProtector(CreateFastPiece(new Bishop(color, position)));
        }

        protected BasePieceDecorator CreateQueen(PieceColor color, Position position)
        {
            return CreateProtector(CreateFastPiece(new Queen(color, position)));
        }

        protected BasePieceDecorator CreateKnight(PieceColor color, Position position)
        {
            return CreateProtector(CreateSlowPiece(new Knight(color, position)));
        }

        protected ProtectedPieceRule CreateKing(IPiece piece)
        {
            return new CastleRule(new ProtectedPieceRule(new KillRule(new MoveRule(new PieceOnBoard(piece, Board)))));
        }

        protected BasePieceDecorator CreateCommoner(PieceColor color, Position position)
        {
            return CreateProtector(CreateSlowPiece(new Commoner(color, position)));
        }
        #endregion
    }
}
