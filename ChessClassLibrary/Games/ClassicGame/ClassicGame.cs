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
        private void UpdateGameStatus()
        {
            throw new NotImplementedException();
        }

        private void SwapPlayers()
        {
            throw new NotImplementedException();
        }
        #endregion


        #region Move manager
        public bool CanPerformMove(Move move)
        {
            Piece pickedPiece = this.board.GetPiece(move.current);
            if (pickedPiece == null || pickedPiece.Color == currentPlayerColor)
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
            // TODO swap players
            throw new NotImplementedException();
        }
        #endregion Move manager


        private void MovePieceToPosition(Piece piece, Position position)
        {
            board.SetPiece(null, piece.Position);
            board.SetPiece(piece, position);
            piece.Position = position;
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

        #region Pretend move
        private bool PretendMovesAndCheckIfKingIsChecked(IEnumerable<Move> moves)
        {
            var backup = new Stack<PieceBackup>();

            foreach (Move move in moves)
            {
                Piece pieceAtStartPosition = board.GetPiece(move.current);
                Piece pieceAtDestinationPosition = board.GetPiece(move.destination);

                backup.Push(new PieceBackup(pieceAtStartPosition, move.current));
                backup.Push(new PieceBackup(pieceAtDestinationPosition, move.destination));


                MovePieceToPosition(pieceAtStartPosition, move.destination);
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
                pieceBackup.piece.Position = pieceBackup.position;
            }
            return KingIsChecked;
        }

        private bool PretendMovesAndCheckIfKingIsChecked(Move move)
        {
            return PretendMovesAndCheckIfKingIsChecked(new List<Move>() { move });
        }
        #endregion Pretend move  

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

        #region King Rules

        #region Castle
        private bool IsCastleMove(Move move)
        {
            return IsLeftCastleMove(move) || IsRightCastleMove(move);
        }

        private bool IsLeftCastleMove(Move move)
        {
            if (move.current == new Position(4, 0) && move.destination == new Position(2, 0))
            {
                return true;
            }

            if (move.current == new Position(4, 7) && move.destination == new Position(2, 7))
            {
                return true;
            }

            return false;
        }

        private bool IsRightCastleMove(Move move)
        {
            if (move.current == new Position(4, 0) && (move.destination == new Position(6, 0)))
            {
                return true;
            }

            if (move.current == new Position(4, 7) && move.destination == new Position(6, 7))
            {
                return true;
            }

            return false;
        }

        private bool CanCastle(ClassicGameKing king, Position destination)
        {
            if (king.IsChecked) return false;
            if (king.WasMoved) return false;

            if (destination == king.Position + king.RightCastleMove)
            {
                return CanRightCastle(king);
            }
            else if (destination == king.Position + king.LeftCastleMove)
            {
                return CanLeftCastle(king);
            }
            return false;
        }
        private bool CanRightCastle(ClassicGameKing king)
        {
            var rookPosition = new Position(7, king.Position.y);
            Piece rightRook = board.GetPiece(rookPosition);
            if (rightRook is Rook && !rightRook.WasMoved && rightRook.Color == king.Color)
            {
                foreach (var checkedPosition in new Position[] { new Position(5, king.Position.y), new Position(6, king.Position.y) })
                {
                    if (board.GetPiece(checkedPosition) != null) return false;

                    if (CanAnyKillAtPosition(board.Where(x => x.Color != king.Color), checkedPosition)) return false;
                }
                return true;
            }
            return false;
        }
        private bool CanLeftCastle(ClassicGameKing king)
        {
            var rookPosition = new Position(0, king.Position.y);
            Piece leftRook = board.GetPiece(rookPosition);
            if (leftRook is Rook && !leftRook.WasMoved && leftRook.Color == king.Color && board.GetPiece(new Position(1, king.Position.y)) == null)
            {
                foreach (var checkedPosition in new Position[] { new Position(2, king.Position.y), new Position(3, king.Position.y) })
                {
                    if (board.GetPiece(checkedPosition) != null) return false;

                    if (CanAnyKillAtPosition(board.Where(x => x.Color != king.Color), checkedPosition)) return false;
                }
                return true;
            }
            return false;
        }

        private void DoLeftCastle(ClassicGameKing king)
        {
            var yPosition = king.Position.y;
            MovePieceToPosition(king, new Position(2, yPosition));
            MovePieceToPosition(board.GetPiece(new Position(0, yPosition)), new Position(3, yPosition));
        }

        private void DoRightCastle(ClassicGameKing king)
        {
            var yPosition = king.Position.y;
            MovePieceToPosition(king, new Position(6, yPosition));
            MovePieceToPosition(board.GetPiece(new Position(7, yPosition)), new Position(5, yPosition));
        }
        #endregion Castle

        private bool CanAnyKillAtPosition(IEnumerable<Piece> pieces, Position position)
        {
            return pieces.Any(x => canKillAtPosition(x, position));
        }

        private bool IsKingChecked(ClassicGameKing king)
        {
            return CanAnyKillAtPosition(board.Where(x => x.Color != king.Color), king.Position);
        }

        private bool IsKingCheckmated(ClassicGameKing king)
        {
            return king.IsChecked && !canProtectFromDeath(king);
        }

        private bool IsKingStalemated(ClassicGameKing king)
        {
            return !king.IsChecked && (!canProtectFromDeath(king) || NotEnoughtPieces());
        }

        private bool canProtectFromDeath(ClassicGameKing king)
        {
            throw new NotImplementedException();
        }

        private bool NotEnoughtPieces()
        {
            throw new NotImplementedException();
        }

        private void UpdateKingState(ClassicGameKing king)
        {
            if (IsKingChecked(king)) king.State = KingState.Checked;
            if (IsKingCheckmated(king)) king.State = KingState.Checkmated;
            if (IsKingStalemated(king)) king.State = KingState.Stalemated;
        }
        #endregion King Rules

    }
}
