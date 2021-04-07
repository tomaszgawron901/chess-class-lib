using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.Pieces;
using ChessClassLibrary.Pieces.FasePieces;
using ChessClassLibrary.Pieces.SlowPieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Games.ClassicGame
{
    public class ClassicGame : IGame
    {
        private ClassicBoard_8x8 board;
        private PieceColor currentPlayerColor;
        public GameState GameState { get; private set; }

        public ClassicGame()
        {
            board = new ClassicBoard_8x8();
            currentPlayerColor = PieceColor.White;
            GameState = GameState.NotStarted;
        }

        #region Game status
        public void UpdateGameStatus()
        {
            UpdateKingState(board.WhiteKing);
            UpdateKingState(board.BlackKing);
            if (board.WhiteKing.IsChecked 
                || board.WhiteKing.IsCheckmated 
                || board.WhiteKing.IsStalemated 
                || board.BlackKing.IsChecked 
                || board.BlackKing.IsCheckmated 
                || board.BlackKing.IsStalemated
                || InsufficientMatingMaterial())
            {
                GameState = GameState.Ended;
            }
        }

        public void SwapPlayers()
        {
            if (currentPlayerColor == PieceColor.White)
            {
                currentPlayerColor = PieceColor.Black;
            } else
            {
                currentPlayerColor = PieceColor.White;
            }
        }
        #endregion


        #region Move manager
        public bool CanPerformMove(Move move)
        {
            Piece pickedPiece = this.board.GetPiece(move.current);
            if (pickedPiece == null || pickedPiece.Color != currentPlayerColor)
            {
                return false;
            }

            if (pickedPiece is ClassicGameKing && CanCastle(pickedPiece as ClassicGameKing, move.destination)) return true;

            if (CanMovePiece(pickedPiece, move.destination)) return true; ;

            return false;
        }

        public void TryPerformMove(Move move)
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

        public void PerformMove(Move move)
        {
            Piece pickedPiece = this.board.GetPiece(move.current);

            if (pickedPiece is ClassicGameKing)
            {
                if (IsLeftCastleMove(move))
                {
                    DoLeftCastle(pickedPiece as ClassicGameKing);
                }
                else if (IsRightCastleMove(move))
                {
                    DoRightCastle(pickedPiece as ClassicGameKing);
                }
            }
            else
            {
                MovePieceToPosition(pickedPiece, move.destination);
            }

            AfterMovePerformed();
        }

        public void AfterMovePerformed()
        {
            UpdateGameStatus();
            SwapPlayers();
        }
        #endregion Move manager


        #region canMovePiece
        private bool CanMovePiece(Piece piece, Position destination)
        {
            if (piece is SlowPiece) return CanMovePiece(piece as SlowPiece, destination);

            if (piece is FastPiece) return CanMovePiece(piece as FastPiece, destination);

            return false;
        }


        #endregion


        #region King Rules

        #endregion King Rules

    }
}
