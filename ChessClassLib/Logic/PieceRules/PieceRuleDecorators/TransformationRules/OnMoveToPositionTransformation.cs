using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;

namespace ChessClassLib.Logic.PieceRules.PieceRuleDecorators.TransformationRules
{
    public static partial class IPieceRuleExtensions
    {
        public static OnMoveToYPositionTransformation AddOnMoveToYPositionTransformation(this IPieceRule innerPieceRule, IPiece transformingTo, int yPosition)
        {
            return new OnMoveToYPositionTransformation(innerPieceRule, transformingTo, yPosition);
        }
    }

    /// <summary>
    /// Rule responsible for transforming Piece after moving to Position with given Y coordinate.
    /// </summary>
    public class OnMoveToYPositionTransformation: PieceTransformationRule
    {
        public int YPosition { get; private set; }
        public OnMoveToYPositionTransformation(IPieceRule innerPieceRule, IPiece transformingTo, int yPosition)
            :base(innerPieceRule, transformingTo)
        {
            YPosition = yPosition;
        }

        public override void MoveToPosition(Position position)
        {
            base.MoveToPosition(position);
            if (position.Y.Equals(YPosition))
            {
                Transform();
            }
        }
    }
}
