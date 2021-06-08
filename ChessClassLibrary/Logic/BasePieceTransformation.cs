using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Logic
{
    public interface IPieceTransformation : IBasePieceDecorator
    {
        bool Transformed { get; }
        IBasePieceDecorator Current { get; }
        IBasePieceDecorator After { get; }
        void Transform();
    }

    public abstract class BasePieceTransformation: BasePieceDecorator, IPieceTransformation
    {
        public bool Transformed {get; protected set; }
        public IBasePieceDecorator Current { get; private set; }
        public IBasePieceDecorator After { get; private set; }
        public BasePieceTransformation(IBasePieceDecorator current, IBasePieceDecorator after)
        {
            Current = current;
            After = after;
        }

        public override IPiece Piece => Current;
        public override Board Board => Current.Board;

        public override IEnumerable<PieceMove> MoveSet => Current.MoveSet;

        public override PieceMove MoveModifier(PieceMove move) => move;

        public override bool ValidateNewMove(PieceMove move) => Current.ValidateNewMove(move);
        public override PieceMove GetMoveTo(Position position) => Current.GetMoveTo(position);
        public override void MoveToPosition(Position position) => Current.MoveToPosition(position);

        public void Transform()
        {
            if (!Transformed)
            {
                if (Current.WasMoved)
                {
                    After.MoveToPosition(Current.Position);
                }
                this.Current = After;
            }
        }
    }
}
