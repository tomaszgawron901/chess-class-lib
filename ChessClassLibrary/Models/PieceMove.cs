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
        public MoveType[] MoveTypes { get; set; }
        public Position Shift { get; set; }

        public PieceMove(Position shift, params MoveType[] moveTypes)
        {
            this.MoveTypes = moveTypes;
            this.Shift = shift;
        }
    }
}
