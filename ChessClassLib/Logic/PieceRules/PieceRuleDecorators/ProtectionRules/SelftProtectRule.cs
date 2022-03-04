namespace ChessClassLib.Logic.PieceRules.PieceRuleDecorators.ProtectionRules
{
    public static partial class IPieceRuleExtensions
    {
        public static SelftProtectRule AddSelfProtectRule(this IPieceRule innerPieceRule)
        {
            return new SelftProtectRule(innerPieceRule);
        }
    }

    public class SelftProtectRule : ProtectionRule
    {
        public SelftProtectRule(IPieceRule innerPieceRule) : base(innerPieceRule)
        {
            ProtectedPiece = this;
        }
    }
}
