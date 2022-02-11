using ChessClassLibrary.enums;
using System.Collections.Generic;
using System.Text;

namespace ChessClassLibrary.Models
{
    public class PieceMove
    {
        /// <summary>
        /// Available Piece MoveTypes.
        /// </summary>
        public IEnumerable<MoveType> MoveTypes { get; }
        /// <summary>
        /// Vector by with Piece will be shifted.
        /// </summary>
        public Position Shift { get; }

        public PieceMove(Position shift, params MoveType[] moveTypes)
        {
            this.MoveTypes = moveTypes;
            this.Shift = shift;
        }

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
    }
}
