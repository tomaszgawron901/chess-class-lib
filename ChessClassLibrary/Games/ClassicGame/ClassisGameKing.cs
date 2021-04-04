using ChessClassLibrary.enums;
using ChessClassLibrary.Pieces.SlowPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Games.ClassicGame
{

    public class ClassicGameKing: King
    {
        protected static Position leftCastleMove = new Position(-2, 0);
        protected static Position rightCastleMove = new Position(2, 0);

        public Position LeftCastleMove { get => leftCastleMove; }
        public Position RightCastleMove { get => rightCastleMove; }
        public KingState State { get; set; }

        public bool IsChecked { get => State == KingState.Checked; }
        public bool IsCheckmated { get => State == KingState.Checkmated; }
        public bool IsStalemated { get => State == KingState.Stalemated; }

        public ClassicGameKing(PieceColor color, Position position)
            : base(color, position)
        {
            State = KingState.None;
        }
    }
}
