﻿using ChessClassLibrary.enums;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Pieces.SlowPieces
{
    public abstract class Pawn : Piece
    {
        public Pawn(PieceColor color, Position position) :
            base(color, PieceType.Pawn, position)
        { }

        public abstract IEnumerable<PieceMove> FirstMoveSet { get; }
    }

    public class WhitePawn : Pawn
    {
        protected readonly static PieceMove[] firstMoveSet = new PieceMove[]
        {
            new PieceMove(new Position(0, 2), MoveType.Move ),
            new PieceMove(new Position(0, 1), MoveType.Move ),
            new PieceMove(new Position(-1, 1), MoveType.Kill ),
            new PieceMove(new Position(1, 1), MoveType.Kill ),
        };

        protected readonly static PieceMove[] moveSet = new PieceMove[] 
        { 
            new PieceMove(new Position(0, 1), MoveType.Move ),
            new PieceMove(new Position(-1, 1), MoveType.Kill ),
            new PieceMove(new Position(1, 1), MoveType.Kill ),
        };


        public WhitePawn(Position position) :
            base(PieceColor.White, position)
        { }

        public override IEnumerable<PieceMove> FirstMoveSet => firstMoveSet.Select(x => new PieceMove(x.Shift, x.MoveTypes.Select(y => y).ToArray()));
        public override IEnumerable<PieceMove> MoveSet
        {
            get
            {
                if (this.WasMoved)
                {
                    return moveSet.Select(x => new PieceMove(x.Shift, x.MoveTypes.Select(y => y).ToArray()));
                }
                else
                {
                    return FirstMoveSet;
                }
            }
        }
    }

    public class BlackPawn : Pawn
    {
        protected readonly static PieceMove[] firstMoveSet = new PieceMove[]
{
            new PieceMove(new Position(0, -2), MoveType.Move ),
            new PieceMove(new Position(0, -1), MoveType.Move ),
            new PieceMove(new Position(-1, -1), MoveType.Kill ),
            new PieceMove(new Position(1, -1), MoveType.Kill ),
};

        protected readonly static PieceMove[] moveSet = new PieceMove[]
        {
            new PieceMove(new Position(0, -1), MoveType.Move ),
            new PieceMove(new Position(-1, -1), MoveType.Kill ),
            new PieceMove(new Position(1, -1), MoveType.Kill ),
        };


        public BlackPawn(Position position) :
            base(PieceColor.Black, position)
        { }

        public override IEnumerable<PieceMove> FirstMoveSet => firstMoveSet.Select(x => new PieceMove(x.Shift, x.MoveTypes.Select(y => y).ToArray()));
        public override IEnumerable<PieceMove> MoveSet
        {
            get
            {
                if (this.WasMoved)
                {
                    return moveSet.Select(x => new PieceMove(x.Shift, x.MoveTypes.Select(y => y).ToArray()));
                }
                else
                {
                    return FirstMoveSet;
                }
            }
        }
    }
}
