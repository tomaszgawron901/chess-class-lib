using ChessClassLibrary.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary
{
    public class PieceBackup
    {
        public Piece piece;
        public Position position;

        public PieceBackup(Piece piece, Position position)
        {
            this.piece = piece;
            this.position = position;
        }
    }
}
