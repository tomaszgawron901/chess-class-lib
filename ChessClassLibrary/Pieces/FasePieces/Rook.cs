﻿using ChessClassLibrary.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Pieces.FasePieces
{
    public class Rook : FastPiece
    {
        protected new static Point[] moveSet = new Point[] {
                new Point(0, 1),
                new Point(1, 0),
                new Point(0, -1),
                new Point(-1, 0)
            };
        protected new static Point[] killSet = new Point[] {
                new Point(0, 1),
                new Point(1, 0),
                new Point(0, -1),
                new Point(-1, 0)
            };
        public Rook(PieceColor color, Point position) :
            base(color, PieceType.Rook, position){}
    }
}
