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
    public class ClassicBoard : Board, IEnumerable<Piece>
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

            public Piece Current => board.pieces[Y][X];

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
        private Piece[][] pieces;
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
            get { return pieces; }
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

        public override bool IsInRange(Position position)
        {
            return position.x >= 0 && position.x < width && position.y >= 0 && position.y < height;
        }

        public override Piece GetPiece(Position position)
        {
            return this.pieces[position.y][position.x];
        }

        /// <summary>
        /// Add piece to the board at given position and set piece position.
        /// </summary>
        public override void SetPiece(Piece piece, Position position)
        {
            if (IsInRange(position))
            {
                pieces[position.y][position.x] = piece;
                if (piece != null)
                {
                    piece.Position = position;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public override void Clear()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    pieces[y][x] = null;
                }
            }
        }

        #region Create Board
        private void  CreateBoard()
        {
            this.pieces = new Piece[][] {
                createRichRow(PieceColor.White, 0),
                createPawnRow(PieceColor.White, 1),
                new Piece[width],
                new Piece[width],
                new Piece[width],
                new Piece[width],
                createPawnRow(PieceColor.Black, 6),
                createRichRow(PieceColor.Black, 7),
            };
            whiteKing = pieces[0][4] as King;
            blackKing = pieces[7][4] as King;
        }

        private Piece[] createPawnRow(PieceColor color, int row)
        {
            var newRow = new Piece[width];
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

        private Piece[] createRichRow(PieceColor color, int row)
        {
            return new Piece[] {
            new Rook(color, new Position(0, row)),
            new Knight(color, new Position(1, row)),
            new Bishop(color, new Position(2, row)),
            new Queen(color, new Position(3, row)),
            new King(color, new Position(4, row)),
            new Bishop(color, new Position(5, row)),
            new Knight(color, new Position(6, row)),
            new Rook(color, new Position(7, row)),
            };
        }
        #endregion

        #region Board Restore
        public override IEnumerable<PieceBackup> GetBoardBackup()
        {
            return this.Where(x => x != null).Select(x => new PieceBackup(x, x.Position));
        }

        public override void RestoreBoard(IEnumerable<PieceBackup> backup)
        {
            Clear();
            foreach (var pieceBackup in backup)
            {
                SetPiece(pieceBackup.piece, pieceBackup.position);
            }
        }
        #endregion

    }
}
