using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLib.Logic.PieceRules.PieceRuleDecorators
{
    public abstract class ProtectionRule : PieceRuleDecorator
    {
        public IPiece ProtectedPiece { get; set; }
        public ProtectionRule(IPieceRule innerPieceRule)
            : base(innerPieceRule)
        {}

        public override IEnumerable<PieceMove> MoveSet { get => InnerPiece.MoveSet.Where(isProtectedPieceSafeAfterMove); }

        /// <summary>
        /// Checks if protected Piece couldn't be killed or attacked Piece could be killed after performing PieceMove.
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        protected virtual bool isProtectedPieceSafeAfterMove(PieceMove move)
        {
            var destinationPostion = Position + move.Shift;
            IPiece pieceAtDestinationPosition = Board.GetPiece(destinationPostion);
            IPiece currentPiece = Board.GetPiece(Position);

            var backup = new Stack<PieceBackup>();
            backup.Push(new PieceBackup(currentPiece, Position));
            backup.Push(new PieceBackup(pieceAtDestinationPosition, destinationPostion));


            Board.SetPiece(null, Position);
            currentPiece.Position = destinationPostion;
            Board.SetPiece(currentPiece);

            bool KingIsChecked = Board
                .Where(piece => piece != null && piece.Color != this.Color)
                .Select(piece => piece.GetMoveTo(ProtectedPiece.Position))
                .Any(m => m != null && m.MoveTypes.Contains(MoveType.Kill));

            while (backup.Count > 0)
            {
                var pieceBackup = backup.Pop();
                if (pieceBackup.piece != null)
                {
                    pieceBackup.piece.Position = pieceBackup.position;
                }
                Board.SetPiece(pieceBackup.piece, pieceBackup.position);
            }
            return !KingIsChecked;
        }

        public override PieceMove ConstrainMove(PieceMove move)
        {
            if (isProtectedPieceSafeAfterMove(move))
            {
                return move;
            }
            return null;
        }

        public override bool ValidateMove(PieceMove move)
        {
            return InnerPieceRule.ValidateMove(move) && isProtectedPieceSafeAfterMove(move);
        }
    }
}
