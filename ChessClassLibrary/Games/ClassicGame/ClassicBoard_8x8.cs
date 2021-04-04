using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.Pieces;
using ChessClassLibrary.Pieces.FasePieces;
using ChessClassLibrary.Pieces.SlowPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Games.ClassicGame
{
    public class ClassicBoard_8x8 : ClassicBoard
    {
        public ClassicGameKing WhiteKing { get; protected set; }
        public ClassicGameKing BlackKing { get; protected set; }

        public ClassicBoard_8x8(): base(8, 8)
        { }

        protected override void CreateBoard()
        {
            this.Pieces = new Piece[][] {
                CreateRichRow(PieceColor.White, 0),
                CreatePawnRow(PieceColor.White, 1),
                new Piece[Width],
                new Piece[Width],
                new Piece[Width],
                new Piece[Width],
                CreatePawnRow(PieceColor.Black, 6),
                CreateRichRow(PieceColor.Black, 7),
            };
            WhiteKing = Pieces[0][4] as ClassicGameKing;
            BlackKing = Pieces[7][4] as ClassicGameKing;
        }

        private Piece[] CreatePawnRow(PieceColor color, int row)
        {
            var newRow = new Piece[Width];
            if (color == PieceColor.White)
            {
                for (int col = 0; col < Width; col++)
                {
                    newRow[col] = new WhitePawn(new Position(col, row));
                }
            }
            else
            {
                for (int col = 0; col < Width; col++)
                {
                    newRow[col] = new BlackPawn(new Position(col, row));
                }
            }
            return newRow;
        }

        private Piece[] CreateRichRow(PieceColor color, int row)
        {
            return new Piece[] {
            new Rook(color, new Position(0, row)),
            new Knight(color, new Position(1, row)),
            new Bishop(color, new Position(2, row)),
            new Queen(color, new Position(3, row)),
            new ClassicGameKing(color, new Position(4, row)),
            new Bishop(color, new Position(5, row)),
            new Knight(color, new Position(6, row)),
            new Rook(color, new Position(7, row)),
            };
        }
    }
}
