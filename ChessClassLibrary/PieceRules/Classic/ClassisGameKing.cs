using ChessClassLibrary.enums;
using ChessClassLibrary.Pieces;
using ChessClassLibrary.Pieces.FasePieces;
using ChessClassLibrary.Pieces.SlowPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.PieceRules.Classic
{

    public class ClassicGameKing: ClassicGameSlowPiece
    {
        protected static Position leftCastleMove = new Position(-2, 0);
        protected static Position rightCastleMove = new Position(2, 0);

        public Position LeftCastleMove { get => leftCastleMove; }
        public Position RightCastleMove { get => rightCastleMove; }
        public KingState State { get; set; }

        public bool IsChecked { get => State == KingState.Checked; }
        public bool IsCheckmated { get => State == KingState.Checkmated; }
        public bool IsStalemated { get => State == KingState.Stalemated; }

        public ClassicGameKing(King king, ClassicBoard_8x8 board)
            : base(king, board)
        {
            State = KingState.None;
        }


        #region Castle

        private bool CanCastle(Position destination)
        {
            if (IsChecked) return false;
            if (WasMoved) return false;

            if (destination == Position + RightCastleMove)
            {
                return CanRightCastle();
            }
            else if (destination ==Position + LeftCastleMove)
            {
                return CanLeftCastle();
            }
            return false;
        }
        private bool CanRightCastle()
        {
            var rookPosition = new Position(7, Position.y);
            IPiece rightRook = board.GetPiece(rookPosition);
            if (rightRook is Rook && !rightRook.WasMoved && rightRook.Color == this.Color)
            {
                foreach (var checkedPosition in new Position[] { new Position(5, this.Position.y), new Position(6, this.Position.y) })
                {
                    if (board.GetPiece(checkedPosition) != null) return false;

                    if (CanAnyKillAtPosition(board.Where(x => x.Color != this.Color), checkedPosition)) return false;
                }
                return true;
            }
            return false;
        }
        private bool CanLeftCastle()
        {
            var rookPosition = new Position(0, this.Position.y);
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



        private bool CheckIfChecked()
        {
            return this.board.Where(x => x != null && x.Color != this.Color).Any(x => x.CanKillAchieve(this.Position));
        }

        private bool CheckIfCheckmated()
        {
            return this.IsChecked && !AnyPieceCanMoveAnywhere(this.Color);
        }

        private bool CheckIfStalemated()
        {
            return !this.IsChecked && (!AnyPieceCanMoveAnywhere(this.Color) || this.board.InsufficientMatingMaterial());
        }

        private bool AnyPieceCanMoveAnywhere(PieceColor color)
        {
            return board.Where(p => p.Color == color).Any(canMoveAnywhere);
        }

        private void UpdateKingState(ClassicGameKing king)
        {
            if (CheckIfChecked()) king.State = KingState.Checked;
            if (CheckIfCheckmated()) king.State = KingState.Checkmated;
            if (CheckIfStalemated()) king.State = KingState.Stalemated;
        }
    }
}
