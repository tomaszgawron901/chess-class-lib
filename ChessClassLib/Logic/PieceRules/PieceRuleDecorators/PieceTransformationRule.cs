using ChessClassLib.Pieces;
using ChessClassLibrary.Pieces;

namespace ChessClassLib.Logic.PieceRules.PieceRuleDecorators
{
    /// <summary>
    /// Base class responsible for transforming one Piece to the other.
    /// </summary>
    public abstract class PieceTransformationRule: PieceRuleDecorator
    {
        public IPiece TransformingTo { get; private set; }

        public PieceTransformationRule(IPieceRule innerPieceRule, IPiece transformingTo)
            :base(innerPieceRule)
        {
            TransformingTo = transformingTo;
        }

        protected virtual void Transform()
        {
            TransformingTo.Position = Position;
            TransformingTo.WasMoved = WasMoved;
            Board.SetPiece(TransformingTo);
        }
    }
}
