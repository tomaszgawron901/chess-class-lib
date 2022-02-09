using ChessClassLibrary.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Models
{
    public class PieceMove
    {
        /// <summary>
        /// Available Piece MoveTypes.
        /// </summary>
        public IEnumerable<MoveType> MoveTypes { get; set; }
        /// <summary>
        /// Vector by with Piece will be shifted.
        /// </summary>
        public Position Shift { get; set; }

        public PieceMove(Position shift, params MoveType[] moveTypes)
        {
            this.MoveTypes = moveTypes;
            this.Shift = shift;
        }
    }
}
