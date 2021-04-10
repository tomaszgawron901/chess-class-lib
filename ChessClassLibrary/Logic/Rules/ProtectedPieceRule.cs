using ChessClassLibrary.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Logic.Rules
{
    public class ProtectedPieceRule : ProtectAttackRule
    {
        public virtual KingState KingState { get; set; }
        public bool IsChecked { get => KingState == KingState.Checked;}
        public bool IsCheckemated { get => KingState == KingState.Checkmated; }
        public bool IsStalemated { get => KingState == KingState.Stalemated; }

        public ProtectedPieceRule(BasePieceDecorator pieceDecorator)
            :base(pieceDecorator, pieceDecorator)
        {}
    }
}
