using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.Games.ClassicGame;
using ChessClassLibrary.Logic;
using ChessClassLibrary.Logic.Containers;
using ChessClassLibrary.Logic.Rules;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Games
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

        public ProtectedPieceRule WhiteKing { get; protected set; }
        public ProtectedPieceRule BlackKing { get; protected set; }


        public BaseClassicGame()
        {
            CreateBoard();
            CurrentPlayerColor = PieceColor.White;
            GameState = GameState.NotStarted;
        }

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

        public void TryPerformMove(BoardMove move)
        {
            if (CanPerformMove(move))
            {
                PerformMove(move);
            }
            else
            {
                throw new Exception();
            }
        }

        public void PerformMove(BoardMove move)
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
    }
}
