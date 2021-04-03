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

            if (pickedPiece is King)
            {
                return CanMoveKing(pickedPiece as King, move.destination);
            }
            else if (pickedPiece is SlowPiece)
            {
                return CanMoveSlowPiece(pickedPiece as SlowPiece, move.destination);
            }
            else if (pickedPiece is FastPiece)
            {
                return CanMoveFastPiece(pickedPiece as FastPiece, move.destination);
            }
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

        private bool PretendMoveAndCheckIfKingIsChecked(Piece piece, Position destination)
        {
            var backup = new List<PieceBackup>();
            backup.Add(new PieceBackup(piece, piece.Position));

            Piece pieceAtDestinationPosition = board.GetPiece(destination);
            if (pieceAtDestinationPosition != null)
            {
                backup.Add(new PieceBackup(pieceAtDestinationPosition, destination));
            }

            board.SetPiece(null, piece.Position);
            board.SetPiece(piece, destination);
            bool KingIsChecked = false;
            if (currentPlayerColor == PieceColor.White)
            {
                KingIsChecked = IsKingChecked(board.WhiteKing);
            }
            else if (currentPlayerColor == PieceColor.Black)
            {
                KingIsChecked = IsKingChecked(board.BlackKing);
            }

            backup.ForEach( x => {
                board.SetPiece(x.piece, x.position);
            });
            return KingIsChecked;
        }
        #endregion

        #region Slow Piece Rools
        private bool CanMoveSlowPiece(SlowPiece piece, Position destination)
        {
            if (!board.IsInRange(destination)) return false;

            var moveMovement = piece.CanMoveAchieve(destination);
            if (moveMovement != null)
            {
                var destinationPiece = board.GetPiece(destination);
                if (destinationPiece == null)
                {
                    return PretendMoveAndCheckIfKingIsChecked(piece, destination);
                }
            }

            var killMovement = piece.CanKillAchieve(destination);
            if (killMovement != null)
            {
                var destinationPiece = board.GetPiece(destination);
                if (destinationPiece != null && destinationPiece.Color != piece.Color)
                {
                    return PretendMoveAndCheckIfKingIsChecked(piece, destination);
                }
            }
            return false;
        }
        #endregion

        #region Fast Piece Rools
        private bool CanMoveFastPiece(FastPiece piece, Position destination)
        {
            if (!board.IsInRange(destination)) return false;

            var moveMovement = piece.CanMoveAchieve(destination);
            if (moveMovement != null)
            {
                var destinationPiece = board.GetPiece(destination);
                if (destinationPiece == null && IsPathClear(piece.Position, destination, (Position)moveMovement))
                {
                    return PretendMoveAndCheckIfKingIsChecked(piece, destination);
                }
            }

            var killMovement = piece.CanKillAchieve(destination);
            if (killMovement != null)
            {
                var destinationPiece = board.GetPiece(destination);
                if (destinationPiece != null && destinationPiece.Color != piece.Color && IsPathClear(piece.Position, destination, (Position)moveMovement))
                {
                    return PretendMoveAndCheckIfKingIsChecked(piece, destination);
                }
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
        private bool CanMoveKing(King king, Position destination)
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
