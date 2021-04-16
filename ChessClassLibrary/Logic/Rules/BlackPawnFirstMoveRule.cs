using ChessClassLibrary.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Logic.Rules
{
    class BlackPawnFirstMoveRule: BasePieceRule, IBasePieceDecorator, IPiece
    {
        public BlackPawnFirstMoveRule(BasePieceDecorator pieceDecorator)
            :base(pieceDecorator)
        { }

        public override PieceMove MoveModifier(PieceMove move)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateNewMove(PieceMove move)
        {
            throw new NotImplementedException();
        }
    }
}
