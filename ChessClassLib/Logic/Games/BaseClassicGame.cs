using ChessClassLib.Helpers;
using ChessClassLib.Logic.PieceRules;
using ChessClassLib.Logic.PieceRules.BasePieceRules;
using ChessClassLib.Logic.PieceRules.PieceRuleDecorators;
using ChessClassLib.Logic.PieceRules.PieceRuleDecorators.NewMoveRules;
using ChessClassLib.Logic.PieceRules.PieceRuleDecorators.ProtectionRules;
using ChessClassLib.Logic.PieceRules.PieceRuleDecorators.TransformationRules;
using ChessClassLib.Pieces;
using ChessClassLibrary.Enums;
using ChessClassLibrary.Exceptions;
using ChessClassLibrary.Logic.Boards;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using ChessClassLibrary.Pieces.FasePieces;
using ChessClassLibrary.Pieces.SlowPieces;
using hessClassLibrary.Logic.Games;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Logic.Games
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

        public KingStateProvider WhiteKingManager { get; protected set; }
        public KingStateProvider BlackKingManager { get; protected set; }

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
            WhiteKingManager.UpdateState();
            BlackKingManager.UpdateState();
            if (
                WhiteKingManager.IsCheckmated
                || WhiteKingManager.IsStalemated
                || BlackKingManager.IsCheckmated
                || BlackKingManager.IsStalemated
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
            if (this.WhiteKingManager.IsCheckmated)
            {
                return PieceColor.Black;
            }
            if (this.BlackKingManager.IsCheckmated)
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

        #region Piece Creators
        protected IPiece CreatePawn(PieceColor color, Position position)
        {
            if (color == PieceColor.White)
            {
                return this.CreateWhitePawn(position);
            }
            else if (color == PieceColor.Black)
            {
                return this.CreateBlackPawn(position);
            }
            return null;
        }

        protected IPiece CreateWhitePawn(Position position)
        {
            return new WhitePawn(position)
                .AddPieceOnBoard(Board)
                .AddWhitePawnFirstMoveRule()
                .AddMoveRule()
                .AddKillRule()
                .AddOnMoveToYPositionTransformation(this.CreateQueen(PieceColor.White, position), Board.Height - 1)
                .AddProtectAttackRule(WhiteKingManager.Piece, BlackKingManager.Piece);
        }

        protected IPiece CreateBlackPawn(Position position)
        {
            return new BlackPawn(position)
                .AddPieceOnBoard(Board)
                .AddBlackPawnFirstMoveRule()
                .AddMoveRule()
                .AddKillRule()
                .AddOnMoveToYPositionTransformation(this.CreateQueen(PieceColor.Black, position), 0)
                .AddProtectAttackRule(BlackKingManager.Piece, WhiteKingManager.Piece);
        }

        protected IPieceRule CreateStandardFastPiece(IPiece piece)
        {
            return CreateStandardPiece(piece.AddFastPieceOnBoard(Board));
        }

        protected IPieceRule CreateStandardSlowPiece(IPiece piece)
        {
            return CreateStandardPiece(piece.AddPieceOnBoard(Board));
        }

        private IPieceRule CreateStandardPiece(IPieceRule piece)
        {
            piece = piece
                .AddMoveRule()
                .AddKillRule();

            if (piece.Color == PieceColor.White)
            {
                piece = piece.AddProtectAttackRule(WhiteKingManager.Piece, BlackKingManager.Piece);
            }
            else if (piece.Color == PieceColor.Black)
            {
                piece = piece.AddProtectAttackRule(BlackKingManager.Piece, WhiteKingManager.Piece);
            }

            return piece;
        }

        protected IPieceRule CreateRook(PieceColor color, Position position)
        {
            return CreateStandardFastPiece(new Rook(color, position));
        }

        protected IPieceRule CreateBishop(PieceColor color, Position position)
        {
            return CreateStandardFastPiece(new Bishop(color, position));
        }

        protected IPieceRule CreateQueen(PieceColor color, Position position)
        {
            return CreateStandardFastPiece(new Queen(color, position));
        }

        protected IPieceRule CreateKnight(PieceColor color, Position position)
        {
            return CreateStandardSlowPiece(new Knight(color, position));
        }

        protected IPieceRule CreateCommoner(PieceColor color, Position position)
        {
            return CreateStandardSlowPiece(new Commoner(color, position));
        }
        #endregion
    }
}
