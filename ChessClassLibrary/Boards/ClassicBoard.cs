using ChessClassLibrary.enums;
using ChessClassLibrary.Pieces;
using ChessClassLibrary.Pieces.FasePieces;
using ChessClassLibrary.Pieces.SlowPieces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Boards
{
    public class ClassicBoard : Board
    {
        private sealed class ClassicBoardIterator : IEnumerator<Piece>
        {
            private ClassicBoard board;
            private int X;
            private int Y;
            public ClassicBoardIterator(ClassicBoard board)
            {
                this.board = board;
                X = -1;
                Y = 0;
            }

            public Piece Current => board.board[Y][X];

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                X += 1;
                if (X >= board.Width)
                {
                    X = 0;
                    Y += 1;
                }
                return (X < board.Width && Y < board.Height);
            }

            public void Reset()
            {
                X = -1;
                Y = 0;
            }
        }

        private static int width = 8;
        private static int height = 8;
        private Piece[][] board;
        private King whiteKing;
        private King blackKing;

        #region Props
        public int Width
        {
            get { return width; }
        }
        public int Height
        {
            get { return height; }
        }
        public King WhiteKing
        {
            get { return whiteKing; }
        }
        public King BlackKing
        {
            get { return blackKing; }
        }
        public Piece[][] Board
        {
            get { return board; }
        }
        #endregion

        public ClassicBoard()
        {
            CreateBoard();
        }

        public override IEnumerator<Piece> GetEnumerator()
        {
            return new ClassicBoardIterator(this);
        }

        public override bool IsInRange(Point position)
        {
            return position.X >= 0 && position.X < width && position.Y >= 0 && position.Y < height;
        }

        #region CreateBoard
        private void  CreateBoard()
        {
            this.board = new Piece[][] {
                createRichRow(PieceColor.White, 0),
                createPawnRow(PieceColor.White, 1),
                new Piece[width],
                new Piece[width],
                new Piece[width],
                new Piece[width],
                createPawnRow(PieceColor.Black, 6),
                createRichRow(PieceColor.Black, 7),
            };
            whiteKing = board[0][4] as King;
            blackKing = board[7][4] as King;
        }

        private Piece[] createPawnRow(PieceColor color, int row)
        {
            var newRow = new Piece[width];
            if (color == PieceColor.White)
            {
                for (int col = 0; col < Width; col++)
                {
                    newRow[col] = new WhitePawn(new Point(col, row));
                }
            }
            else
            {
                for (int col = 0; col < Width; col++)
                {
                    newRow[col] = new BlackPawn(new Point(col, row));
                }
            }
            return newRow;
        }

        private Piece[] createRichRow(PieceColor color, int row)
        {
            return new Piece[] {
            new Rook(color, new Point(0, row)),
            new Knight(color, new Point(1, row)),
            new Bishop(color, new Point(2, row)),
            new Queen(color, new Point(3, row)),
            new King(color, new Point(4, row)),
            new Bishop(color, new Point(5, row)),
            new Knight(color, new Point(6, row)),
            new Rook(color, new Point(7, row)),
            };
        }
        #endregion
    }
}
