using ChessClassLibrary.Pieces.FasePieces;
using ChessClassLibrary.Pieces.SlowPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.PieceRules.Classic
{
    public class ClassicGameBlackPiece: ClassicGameSlowPiece
    {
        public ClassicGameBlackPiece(BlackPawn piece, ClassicBoard_8x8 board)
            : base(piece, board)
        { }

        public new void MoveToPosition(Position position)
        {
            base.MoveToPosition(position);
            if (this.Position.y == 0)
            {
                TransformToQueen();
            }
        }

        private void TransformToQueen()
        {
            board.SetPiece(new ClassicGameFastPiece(new Queen(Color, Position), board), Position);
        }
    }
}
