using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using ChessClassLibrary.Pieces.SlowPieces;
using System.Linq;

namespace ChessClassLibrary.Games
{
    /// <summary>
    /// Knightmate Chess Game.
    /// </summary>
    public class KnightmateGame: BaseClassicGame, IGame, IClassicGame
    {
        public KnightmateGame(): base() 
        {}


        protected override bool InsufficientMatingMaterial()
        {
            return InsufficientMatingMaterial(PieceColor.White) && InsufficientMatingMaterial(PieceColor.Black);
        }

        private bool InsufficientMatingMaterial(PieceColor color)
        {
            var colorPieces = Board.Where(x => x != null && x.Color == color);
            var otherColorPieces = Board.Where(x => x != null && x.Color != color);
            var centaurCount = colorPieces.Count(x => x.Type == PieceType.Centaur);
            var rookCount = colorPieces.Count(x => x.Type == PieceType.Rook);
            var commonerCount = colorPieces.Count(x => x.Type == PieceType.Commoner);
            var bishopCount = colorPieces.Count(x => x.Type == PieceType.Bishop);
            var otherCount = colorPieces.Count() - centaurCount - commonerCount - bishopCount - rookCount;
            return (commonerCount <= 1 || rookCount <= 1 || bishopCount <= 1) && otherCount == 0 && otherColorPieces.Count() == 1;
        }


        #region Create Board
        protected override void CreateBoard()
        {
            Board = new ClassicBoard(new IPiece[8, 8]);
            WhiteKing = CreateKing(new Centaur(PieceColor.White, new Position(4, 0)));
            BlackKing = CreateKing(new Centaur(PieceColor.Black, new Position(4, 7)));

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
            Board.SetPiece(CreateCommoner(color, new Position(1, row)));
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
            Board.SetPiece(CreateCommoner(color, new Position(6, row)));
            Board.SetPiece(CreateRook(color, new Position(7, row)));
        }
        #endregion
    }
}
