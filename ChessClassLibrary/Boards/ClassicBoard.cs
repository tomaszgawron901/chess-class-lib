using ChessClassLibrary.enums;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using ChessClassLibrary.Pieces.FasePieces;
using ChessClassLibrary.Pieces.SlowPieces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Boards
{
    public class ClassicBoard : Board, IEnumerable<IPiece>
    {
        private sealed class ClassicBoardIterator : IEnumerator<IPiece>
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

            public IPiece Current => board.Pieces[X, Y];

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

        public int Width { get; protected set; }
        public int Height { get; protected set; }
        protected IPiece[,] Pieces { get; set; }

        public ClassicBoard(IPiece[,] pieces)
        {
            this.Pieces = pieces;
            this.Height = pieces.GetLength(1);
            this.Width = pieces.GetLength(0);
        }

        public override IEnumerator<IPiece> GetEnumerator()
        {
            return new ClassicBoardIterator(this);
        }

        public override bool IsInRange(Position position)
        {
            return position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height;
        }

        public override IPiece GetPiece(Position position)
        {
            return this.Pieces[position.X, position.Y];
        }

        /// <summary>
        /// Add piece to the board at given position.
        /// </summary>
        public override void SetPiece(IPiece piece, Position position)
        {
            if (IsInRange(position))
            {
                Pieces[position.X, position.Y] = piece;
            }
            else
            {
                throw new Exception();
            }
        }

        public override void SetPiece(IPiece piece)
        {
            SetPiece(piece, piece.Position);
        }

        public override void Clear()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Pieces[x, y] = null;
                }
            }
        }
    }
}
