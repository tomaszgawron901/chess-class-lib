using ChessClassLibrary.enums;
using ChessClassLibrary.Pieces;
using ChessClassLibrary.Pieces.SlowPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Games.ClassicGame
{
    public class ClassicGameSlowPiece : SlowPieceOnClassicBoard
    {
        protected SlowPiece piece;
        public override Piece Piece { get => piece; }
        public ClassicGameSlowPiece(SlowPiece piece, ClassicBoard_8x8 board)
            :base(board)
        {
            this.piece = piece;
        }

        public override bool CanKillAchieve(Position position)
        {
            if (base.CanKillAchieve(position))
            {
                var destinationPiece = board.GetPiece(position);
                if (destinationPiece == null && destinationPiece.Color == this.Color) return false;

                return PretendMovesAndCheckIfKingIsChecked(position);
            }
            return false;
        }

        public override bool CanMoveAchieve(Position position)
        {
            if (base.CanMoveAchieve(position))
            {
                var destinationPiece = board.GetPiece(position);
                if (destinationPiece != null) return false;

                return PretendMovesAndCheckIfKingIsChecked(position);
            }
            return false;
        }

        public override bool canMoveEnywhere()
        {
            return this.MoveSet.Any(moveMovement => CanMoveAchieve(this.Position + moveMovement))
                || this.KillSet.Any(killMovement => CanKillAchieve(this.Position + killMovement));
        }
    }
}
