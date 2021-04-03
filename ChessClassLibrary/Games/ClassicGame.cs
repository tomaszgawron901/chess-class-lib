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

            if (CanMovePiece(pickedPiece, move.destination)) return true;;

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

        #region canMoveToPosition
        private bool canMoveToPosition(Piece piece, Position destination)
        {
            if (piece is SlowPiece) return canMoveToPosition(piece as SlowPiece, destination);

            if (piece is FastPiece) return canMoveToPosition(piece as FastPiece, destination);

            return false;
        }

        private bool canMoveToPosition(SlowPiece piece, Position destination)
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

        private bool canMoveToPosition(FastPiece piece, Position destination)
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
        #endregion

        #region canKillAtPosition
        private bool canKillAtPosition(Piece piece, Position destination)
        {
            if (piece is SlowPiece) return canKillAtPosition(piece as SlowPiece, destination);

            if (piece is FastPiece) return canKillAtPosition(piece as FastPiece, destination);

            return false;
        }

        private bool canKillAtPosition(SlowPiece piece, Position destination)
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

        private bool canKillAtPosition(FastPiece piece, Position destination)
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
        #endregion

        #region canMovePiece
        private bool CanMovePiece(Piece piece, Position destination)
        {
            if (piece is SlowPiece) return CanMovePiece(piece as SlowPiece, destination);

            if (piece is FastPiece) return CanMovePiece(piece as FastPiece, destination);

            return false;
        }

        private bool CanMovePiece(SlowPiece piece, Position destination)
        {
            if (!board.IsInRange(destination)) return false;
            if (piece == null) return false;

            if (canMoveToPosition(piece, destination)) return true;

            if (canKillAtPosition(piece, destination)) return true;

            return false;
        }

        private bool CanMovePiece(FastPiece piece, Position destination)
        {
            if (!board.IsInRange(destination)) return false;

            if (piece == null) return false;

            if (canMoveToPosition(piece, destination)) return true;

            if (canKillAtPosition(piece, destination)) return true;

            return false;
        }
        #endregion

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


        #region King Rools
        private bool CanCastle(King king, Position destination)
        {
            if (king.wasMoved) return false;

            if (king.Color == PieceColor.White)
            {
                if (destination == new Position(2, 0))
                {
                    // right white castle
                }
                else if (destination == new Position(6, 0))
                {
                    // left white castle
                }
            }
            else if (king.Color == PieceColor.Black)
            {
                if (destination == new Position(2, 7))
                {
                    // right black castle
                }
                else if (destination == new Position(6, 7))
                {
                    // left black castle
                }
            }
            return false;
        }

        private bool IsKingChecked(King king)
        {
            return board
                .Where(x => x.Color != king.Color)
                .Any(x => canKillAtPosition(x, king.Position));
        }

        private bool IsKingCheckmated(King king)
        {
            return IsKingChecked(king) && !canProtectFromDeath(king);
        }

        private bool IsKingStalemated(King king)
        {
            return !IsKingChecked(king) && (!canProtectFromDeath(king) || notEnoughtPieces());
        }

        private bool canProtectFromDeath(King king)
        {
            throw new NotImplementedException();
        }

        private bool notEnoughtPieces()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
