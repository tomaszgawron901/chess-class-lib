using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using System.Collections.Generic;

namespace ChessClassLibrary.PiecesMoveSets
{
    internal static partial class PieceMoveSets
    {
        public readonly static IEnumerable<PieceMove> BlackPawnMoveSet = new PieceMoveSet
        (
            new PieceMove(new Position(0, -1), MoveType.Move),
            new PieceMove(new Position(-1, -1), MoveType.Kill),
            new PieceMove(new Position(1, -1), MoveType.Kill)
        );

        public readonly static IEnumerable<PieceMove> WhitePawnMoveSet = new PieceMoveSet
        (
            new PieceMove(new Position(0, 1), MoveType.Move ),
            new PieceMove(new Position(-1, 1), MoveType.Kill ),
            new PieceMove(new Position(1, 1), MoveType.Kill )
        );
    }
}
