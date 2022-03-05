using ChessClassLib.Pieces;
using ChessClassLib.Enums;
using ChessClassLib.Logic.Boards;
using ChessClassLib.Models;
using System.Linq;

namespace ChessClassLib.Helpers
{
    public interface IKingStateProvider
    {
        KingState KingState { get; }
        bool IsChecked { get => KingState == KingState.Checked; }
        bool IsCheckmated { get => KingState == KingState.Checkmated; }
        bool IsStalemated { get => KingState == KingState.Stalemated; }
        void UpdateState();
    }

    public class KingStateProvider: IKingStateProvider
    {
        public IPiece Piece { get; set; }
        public IBoard Board { get; set; }

        public KingState KingState { get; private set; }
        public bool IsChecked { get => KingState == KingState.Checked; }
        public bool IsCheckmated { get => KingState == KingState.Checkmated; }
        public bool IsStalemated { get => KingState == KingState.Stalemated; }

        public KingStateProvider(IPiece piece, IBoard board)
        {
            Piece = piece;
            Board = board;
        }


        public void UpdateState()
        {
            KingState = KingState.None;
            if (Board.Any(p => p != null && p.Color != Piece.Color && PieceMove.HasType(p.GetMoveTo(Piece.Position), MoveType.Kill)))
            {
                KingState = KingState.Checked;
                var aaa = Board.Where(piece => piece != null && piece.Color == Piece.Color && piece.MoveSet.Any());
                if (!Board.Any(piece => piece != null && piece.Color == Piece.Color && piece.MoveSet.Any()))
                {
                    KingState = KingState.Checkmated;
                }

            }
            else if (!Board.Any(piece => piece != null && piece.Color == Piece.Color && piece.MoveSet.Any()))
            {
                KingState = KingState.Stalemated;
            }
        }
    }
}
