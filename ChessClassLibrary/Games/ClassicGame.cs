using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.Pieces;
using ChessClassLibrary.Pieces.FasePieces;
using ChessClassLibrary.Pieces.SlowPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Games
{
    public class ClassicGame : IGame
    {
        private ClassicBoard board;
        private PieceColor currentPlayerColor;
        public ClassicGame()
        {
            board = new ClassicBoard();
            currentPlayerColor = PieceColor.White;
        }

        #region Common Piece Rools
        public bool CanPerformMove(Move move)
        {
            Piece pickedPiece = this.board.GetPiece(move.current);
            if (pickedPiece == null || pickedPiece.Color == currentPlayerColor)
            {
                return false;
            }

            if (pickedPiece is King && CanCastle(pickedPiece as King, move.destination)) return true;

            if (pickedPiece is SlowPiece && CanMoveSlowPiece(pickedPiece as SlowPiece, move.destination)) return true;

            if (pickedPiece is FastPiece && CanMoveFastPiece(pickedPiece as FastPiece, move.destination)) return true;

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
            throw new NotImplementedException();
        }

        private bool PretendMovesAndCheckIfKingIsChecked(IEnumerable<Move> moves)
        {
            var backup = new Stack<PieceBackup>();

            foreach (Move move in moves)
            {
                Piece pieceAtStartPosition = board.GetPiece(move.current);
                Piece pieceAtDestinationPosition = board.GetPiece(move.destination);

                backup.Push(new PieceBackup(pieceAtStartPosition, move.current));
                backup.Push(new PieceBackup(pieceAtDestinationPosition, move.destination));

                board.SetPiece(null, move.current);
                board.SetPiece(pieceAtStartPosition, move.destination);
            }

            bool KingIsChecked = false;
            if (currentPlayerColor == PieceColor.White)
            {
                KingIsChecked = IsKingChecked(board.WhiteKing);
            }
            else if (currentPlayerColor == PieceColor.Black)
            {
                KingIsChecked = IsKingChecked(board.BlackKing);
            }

            while (backup.Count > 0)
            {
                var pieceBackup = backup.Pop();
                board.SetPiece(pieceBackup.piece, pieceBackup.position);
            }
            return KingIsChecked;
        }

        private bool PretendMovesAndCheckIfKingIsChecked(Move move)
        {
            return PretendMovesAndCheckIfKingIsChecked(new List<Move>() {move});
        }
        #endregion

        #region Slow Piece Rools
        private bool CanMoveSlowPiece(SlowPiece piece, Position destination)
        {
            if (!board.IsInRange(destination)) return false;
            if (piece == null) return false;

            if (canSlowMoveToPosition(piece, destination)) return true;

            if (canSlowKillAtPosition(piece, destination)) return true;

            return false;
        }

        private bool canSlowKillAtPosition(SlowPiece piece, Position destination)
        {
            var killMovement = piece.CanKillAchieve(destination);
            if (killMovement != null)
            {
                var destinationPiece = board.GetPiece(destination);
                if (destinationPiece == null && destinationPiece.Color == piece.Color) return false;

                return PretendMovesAndCheckIfKingIsChecked(new Move(piece.Position, destination));
            }
            return false;
        }

        private bool canSlowMoveToPosition(SlowPiece piece, Position destination)
        {
            var moveMovement = piece.CanMoveAchieve(destination);
            if (moveMovement != null)
            {
                var destinationPiece = board.GetPiece(destination);
                if (destinationPiece != null) return false;

                return PretendMovesAndCheckIfKingIsChecked(new Move(piece.Position, destination));
            }
            return false;
        }
        #endregion

        #region Fast Piece Rools
        private bool CanMoveFastPiece(FastPiece piece, Position destination)
        {
            if (!board.IsInRange(destination)) return false;

            if (piece == null) return false;

            if (canFastMoveToPosition(piece, destination)) return true;

            if (canFastKillAtPosition(piece, destination)) return true;

            return false;
        }

        private bool canFastKillAtPosition(FastPiece piece, Position destination)
        {
            var killMovement = piece.CanKillAchieve(destination);
            if (killMovement != null)
            {
                var destinationPiece = board.GetPiece(destination);
                if (destinationPiece == null
                    || destinationPiece.Color == piece.Color
                    || !IsPathClear(piece.Position, destination, (Position)killMovement)) return false;

                return PretendMovesAndCheckIfKingIsChecked(new Move(piece.Position, destination));
            }
            return false;
        }

        private bool canFastMoveToPosition(FastPiece piece, Position destination)
        {
            var moveMovement = piece.CanMoveAchieve(destination);
            if (moveMovement != null)
            {
                var destinationPiece = board.GetPiece(destination);
                if (destinationPiece != null
                    || !IsPathClear(piece.Position, destination, (Position)moveMovement)) return false;

                return PretendMovesAndCheckIfKingIsChecked(new Move(piece.Position, destination));
            }
            return false;
        }

        private bool IsPathClear(Position startPosition, Position destination, Position move)
        {
            for (
                Position checkedPosition = startPosition + move;
                checkedPosition != destination;
                checkedPosition += move)
            {
                if (board.GetPiece(checkedPosition) != null)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region King Rools
        private bool CanCastle(King king, Position destination)
        {
            // check if can perform slow move and castle move
            throw new NotImplementedException();
        }

        private bool IsKingChecked(King king)
        {
            // check is king is checked
            throw new NotImplementedException();
        }

        private bool IsKingCheckmated(King king)
        {
            // check is king is checkmated
            throw new NotImplementedException();
        }

        private bool IsKingStalemated(King king)
        {
            // check is king is stalemated
            throw new NotImplementedException();
        }
        #endregion

    }
}
