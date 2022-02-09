using ChessClassLibrary.Boards;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using System.Collections.Generic;

namespace ChessClassLibrary.Logic
{
    public interface IPieceTransformation : IBasePieceDecorator
    {
        bool Transformed { get; }
        IBasePieceDecorator Current { get; }
        IBasePieceDecorator After { get; }
        void Transform();
    }

    /// <summary>
    /// Base class responsible for transforming one Piece to the other.
    /// </summary>
    public abstract class BasePieceTransformation: BasePieceDecorator, IPieceTransformation
    {
        /// <summary>
        /// Determines if piece was transformed.
        /// </summary>
        public bool Transformed {get; protected set; }

        /// <summary>
        /// Current piece.
        /// </summary>
        public IBasePieceDecorator Current { get; private set; }

        /// <summary>
        /// Piece after transformation.
        /// </summary>
        public IBasePieceDecorator After { get; private set; }
        public BasePieceTransformation(IBasePieceDecorator current, IBasePieceDecorator after)
        {
            Current = current;
            After = after;
            Transformed = false;
        }

        public override IPiece Piece => Current;
        public override IBoard Board => Current.Board;

        public override IEnumerable<PieceMove> MoveSet => Current.MoveSet;

        public override PieceMove MoveModifier(PieceMove move) => move;

        public override bool ValidateNewMove(PieceMove move) => Current.ValidateNewMove(move);
        public override PieceMove GetMoveTo(Position position) => Current.GetMoveTo(position);
        public override void MoveToPosition(Position position) => Current.MoveToPosition(position);

        public void Transform()
        {
            if (!Transformed)
            {
                After.Position = Current.Position;
                Current = After;
                Transformed = true;
            }
        }
    }
}
