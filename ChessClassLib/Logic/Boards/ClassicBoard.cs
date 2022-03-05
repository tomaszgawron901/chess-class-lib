using ChessClassLib.Pieces;
using ChessClassLib.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using ChessClassLib.Exceptions;

namespace ChessClassLib.Logic.Boards
{
    public class ClassicBoard : IBoard
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

            public void Dispose() { }

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
            Pieces = pieces;
            Height = pieces.GetLength(1);
            Width = pieces.GetLength(0);
        }

        public IEnumerator<IPiece> GetEnumerator() => new ClassicBoardIterator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool IsInRange(Position position)
        {
            return position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height;
        }

        public IPiece GetPiece(Position position) => Pieces[position.X, position.Y];

        public void SetPiece(IPiece piece, Position position)
        {
            if (IsInRange(position)) {
                Pieces[position.X, position.Y] = piece;
            }
            else {
                throw new PositionOutsideBoardRangeException();
            }
        }

        public void SetPiece(IPiece piece) => SetPiece(piece, piece.Position);

        public void Clear()
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
