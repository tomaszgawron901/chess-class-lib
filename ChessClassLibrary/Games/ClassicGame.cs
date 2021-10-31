using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using ChessClassLibrary.Pieces.SlowPieces;
using System.Linq;

namespace ChessClassLibrary.Games.ClassicGame
{
    /// <summary>
    /// Standard Chess Game.
    /// </summary>
    public class ClassicGame: BaseClassicGame, IGame, IClassicGame
    {
        public ClassicGame(): base() {}

        protected override bool InsufficientMatingMaterial()
        {
            return InsufficientMatingMaterial(PieceColor.White) && InsufficientMatingMaterial(PieceColor.Black);
        }

        /// <summary>
        /// Checks if there is enough Pieces with given color to play the Game.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private bool InsufficientMatingMaterial(PieceColor color)
        {
            var colorPieces = Board.Where(x => x != null && x.Color == color);
            var kingCount = colorPieces.Count(x => x.Type == PieceType.King);
            var knightCount = colorPieces.Count(x => x.Type == PieceType.Knight);
            var bishopCount = colorPieces.Count(x => x.Type == PieceType.Bishop);
            var otherCount = colorPieces.Count() - kingCount - knightCount - bishopCount;
            return (knightCount <= 1 && bishopCount == 0) || (knightCount == 1 && bishopCount <= 1) && otherCount == 0;
        }


        #region Create Board
        protected override void CreateBoard()
        {
            Board = new ClassicBoard(new IPiece[8, 8]);
            WhiteKing = CreateKing(new King(PieceColor.White, new Position(4, 0)));
            BlackKing = CreateKing(new King(PieceColor.Black, new Position(4, 7)));

            WhiteKing.AtackedPiece = BlackKing;
            BlackKing.AtackedPiece = WhiteKing;

            InsertRichRow(PieceColor.White, 0);
            InsertPawnRow(PieceColor.White, 1);
            InsertEmptyRow(2);
            InsertEmptyRow(3);
            InsertEmptyRow(4);
            InsertEmptyRow(5);
            InsertPawnRow(PieceColor.Black, 6);
            InsertRichRow(PieceColor.Black, 7);
        }

        private void InsertRichRow(PieceColor color, int row)
        {
            Board.SetPiece(CreateRook(color, new Position(0, row)));
            Board.SetPiece(CreateKnight(color, new Position(1, row)));
            Board.SetPiece(CreateBishop(color, new Position(2, row)));
            Board.SetPiece(CreateQueen(color, new Position(3, row)));
            if (color == PieceColor.White)
            {
                Board.SetPiece(WhiteKing);
            }
            else if (color == PieceColor.Black)
            {
                Board.SetPiece(BlackKing);
            }
            Board.SetPiece(CreateBishop(color, new Position(5, row)));
            Board.SetPiece(CreateKnight(color, new Position(6, row)));
            Board.SetPiece(CreateRook(color, new Position(7, row)));
        }
        #endregion
    }
}
