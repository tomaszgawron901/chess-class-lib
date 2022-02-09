using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using System.Collections.Generic;

namespace ChessClassLibrary.PiecesMoveSets
{
    internal static partial class PieceMoveSets
    {
        public readonly static IEnumerable<PieceMove> BishopMoveSet = new PieceMoveSet
        (
            new PieceMove(new Position(-1, 1), MoveType.Move, MoveType.Kill),
            new PieceMove(new Position(1, 1), MoveType.Move, MoveType.Kill),
            new PieceMove(new Position(1, -1), MoveType.Move, MoveType.Kill),
            new PieceMove(new Position(-1, -1), MoveType.Move, MoveType.Kill)
        );
    }
}
