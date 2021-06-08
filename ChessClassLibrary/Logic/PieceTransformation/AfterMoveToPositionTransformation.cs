using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Logic.PieceTransformation
{
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
