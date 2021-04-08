using ChessClassLibrary.Pieces;
using ChessClassLibrary.Pieces.FasePieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.PieceRules.Classic
{
    public class ClassicGameFastPiece : PieceOnBoard
    {
        protected FastPiece piece;
        public ClassicGameFastPiece(FastPiece piece, ClassicBoard_8x8 board)
            : base(board)
        {
            this.piece = piece;
        }

        public override Piece Piece { get => piece; }

        public override bool CanKillAchieve(Position position)
        {
            var movesThatCanAchieve = this.KillSet.Where(move => piece.isInLine(position, move));
            if (movesThatCanAchieve.Count() == 0) return false;
            Position chosenMove = movesThatCanAchieve.First();

            var destinationPiece = board.GetPiece(position);
            if (destinationPiece == null
                || destinationPiece.Color == piece.Color
                || !IsPathClear(position, chosenMove)) return false;

            return PretendMovesAndCheckIfKingIsChecked(position);

        }

        public override bool CanMoveAchieve(Position position)
        {
            var movesThatCanAchieve = piece.MoveSet.Where(move => piece.isInLine(position, move));
            if (movesThatCanAchieve.Count() == 0) return false;
            Position chosenMove = movesThatCanAchieve.First();


            var destinationPiece = board.GetPiece(position);
            if (destinationPiece != null
                || !IsPathClear(position, chosenMove)) return false;

            return PretendMovesAndCheckIfKingIsChecked(position);
        }

        private bool IsPathClear(Position destination, Position move)
        {
            for (
                Position checkedPosition = Position + move;
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


        public override bool canMoveEnywhere()
        {
            return piece.MoveSet.Where(x => x != new Position(0, 0)).Any(moveMovement =>
            {
                for (
                    var checkedPosition = piece.Position + moveMovement;

                    board.IsInRange(checkedPosition) && board.GetPiece(checkedPosition) == null;

                    checkedPosition += moveMovement)
                {
                    if (CanMoveAchieve(checkedPosition))
                    {
                        return true;
                    }
                }
                return false;
            }) || piece.KillSet.Where(x => x != new Position(0, 0)).Any(moveMovement =>
            {
                for (
                    var checkedPosition = piece.Position + moveMovement;
                    board.IsInRange(checkedPosition);
                    checkedPosition += moveMovement)
                {
                    if (board.GetPiece(checkedPosition) != null)
                    {
                        return CanKillAchieve(checkedPosition);
                    }
                }
                return false;
            });

        }
    }
}
