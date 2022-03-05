using ChessClassLib.Helpers;
using ChessClassLib.Logic.PieceRules;
using ChessClassLib.Logic.PieceRules.PieceRuleDecorators;
using ChessClassLib.Logic.PieceRules.PieceRuleDecorators.NewMoveRules;
using ChessClassLib.Logic.PieceRules.PieceRuleDecorators.ProtectionRules;
using ChessClassLib.Logic.PieceRules.PieceRuleDecorators.TransformationRules;
using ChessClassLib.Pieces;
using ChessClassLib.Enums;
using ChessClassLib.Exceptions;
using ChessClassLib.Logic.Boards;
using ChessClassLib.Models;
using ChessClassLib.Pieces.FasePieces;
using ChessClassLib.Pieces.SlowPieces;
using hessClassLibrary.Logic.Games;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLib.Logic.Games
{
    public interface IClassicGame : IGame
    {
        ClassicBoard Board { get; }
    }

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

        protected abstract bool InsufficientMatingMaterial();

        protected abstract void CreateBoard();

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

        public bool CanPerformMove(BoardMove move)
        {
            IPiece pickedPiece = Board.GetPiece(move.Current);
            if (pickedPiece != null && pickedPiece.Color == CurrentPlayerColor)
            {
                return pickedPiece.GetMoveTo(move.Destination) != null;
            }
            return false;
        }

        public virtual bool TryPerformMove(BoardMove move)
        {
            if (CanPerformMove(move))
            {
                PerformMove(move);
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void PerformMove(BoardMove move)
        {
            if (GameState == GameState.NotStarted)
            {
                GameState = GameState.InProgress;
            }
            Board.GetPiece(move.Current).MoveToPosition(move.Destination);
            AfterMovePerformed();
        }

        public void AfterMovePerformed()
        {
            UpdateGameStatus();
            SwapPlayers();
        }

        public IEnumerable<PieceMove> GetPieceMoveSetAtPosition(Position position)
        {
            var piece = Board.GetPiece(position);
            if (piece == null) { return Enumerable.Empty<PieceMove>(); }

            return piece.MoveSet;
        }

        public PieceColor? GetWinner()
        {
            if (WhiteKingManager.IsCheckmated)
            {
                return PieceColor.Black;
            }
            if (BlackKingManager.IsCheckmated)
            {
                return PieceColor.White;
            }
            return null;
        }

        protected void InsertEmptyRow(int row)
        {
            for (int i = 0; i < Board.Width; i++)
            {
                Board.SetPiece(null, new Position(1, row));
            }
        }

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
                return CreateWhitePawn(position);
            }
            else if (color == PieceColor.Black)
            {
                return CreateBlackPawn(position);
            }
            return null;
        }

        protected IPiece CreateWhitePawn(Position position)
        {
            return new WhitePawn(position)
                .AddBasePieceRule(Board)
                .AddPieceOnBoardRule()
                .AddWhitePawnFirstMoveRule()
                .AddMoveRule()
                .AddKillRule()
                .AddOnMoveToYPositionTransformation(CreateQueen(PieceColor.White, position), Board.Height - 1)
                .AddProtectAttackRule(WhiteKingManager.Piece, BlackKingManager.Piece);
        }

        protected IPiece CreateBlackPawn(Position position)
        {
            return new BlackPawn(position)
                .AddBasePieceRule(Board)
                .AddPieceOnBoardRule()
                .AddBlackPawnFirstMoveRule()
                .AddMoveRule()
                .AddKillRule()
                .AddOnMoveToYPositionTransformation
                (CreateQueen(PieceColor.Black, position), 0)
                .AddProtectAttackRule(BlackKingManager.Piece, WhiteKingManager.Piece);
        }

        protected IPieceRule CreateStandardFastPiece(IPiece piece)
        {
            return CreateStandardPiece(piece.AddBasePieceRule(Board).AddFastPieceOnBoardRule());
        }

        protected IPieceRule CreateStandardSlowPiece(IPiece piece)
        {
            return CreateStandardPiece(piece.AddBasePieceRule(Board).AddPieceOnBoardRule());
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
