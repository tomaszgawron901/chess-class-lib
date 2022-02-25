using ChessClassLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessClassLib.Models
{
    public class PieceMove: IEquatable<PieceMove>
    {
        public IEnumerable<MoveType> MoveTypes { get; }

        public Shift Shift { get; }

        public PieceMove(Shift shift, params MoveType[] moveTypes)
        {
            this.MoveTypes = moveTypes;
            this.Shift = shift;
        }

        public bool HasType(MoveType moveType) => MoveTypes.Any(mt => mt == moveType);

        public override string ToString()
        {
            var st = new StringBuilder();
            st.Append(Shift.ToString());
            foreach (var moveType in MoveTypes)
            {
                st.Append(' ');
                st.Append(moveType.ToString());
            }
            return st.ToString();
        }

        public static bool HasType(PieceMove pieceMove, MoveType moveType)
        {
            return pieceMove == null ? false : pieceMove.HasType(moveType);
        }

        public override bool Equals(object obj)
        {
            return obj is PieceMove && Equals((PieceMove)obj);
        }

        public bool Equals(PieceMove other)
        {
            return this.Shift.Equals(other.Shift) && this.MoveTypes.Count() == other.MoveTypes.Count() && MoveTypes.All(mt => other.MoveTypes.Contains(mt));
        }
    }
}
