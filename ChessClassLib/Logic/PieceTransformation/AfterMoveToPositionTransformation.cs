using ChessClassLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Logic.PieceTransformation
{
    /// <summary>
    /// Rule responsible for transforming Piece after moving to given Position.
    /// </summary>
    public class AfterMoveToPositionTransformation: BasePieceTransformation, IPieceTransformation
    {
        public IEnumerable<Position> Positions { get; private set; }
        public AfterMoveToPositionTransformation(IBasePieceDecorator current, IBasePieceDecorator after, IEnumerable<Position> positions)
            :base(current, after)
        {
            this.Positions = positions;
        }

        public new void MoveToPosition(Position position)
        {
            base.MoveToPosition(position);
            if (!Transformed && Positions.Any(x => x.Equals(position)))
            {
                Transform();
            }
        }
    }
}
