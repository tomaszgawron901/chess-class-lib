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
    /// <summary>
    /// Rectangular Board.
    /// </summary>
    public class ClassicBoard : IBoard
    {
        /// <summary>
        /// Class responsible for enumerating through the Board Pieces.
        /// </summary>
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

        public IEnumerator<IPiece> GetEnumerator()
        {
            return new ClassicBoardIterator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Checks if given position is in range of the Board.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool IsInRange(Position position)
        {
            return position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height;
        }

        /// <summary>
        /// Gets Piece at given Position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public IPiece GetPiece(Position position)
        {
            return this.Pieces[position.X, position.Y];
        }

        /// <summary>
        /// Add Piece to the Board at the given Position.
        /// </summary>
        public void SetPiece(IPiece piece, Position position)
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

        /// <summary>
        /// Add Piece to the Board at its Position.
        /// </summary>
        /// <param name="piece"></param>
        public void SetPiece(IPiece piece)
        {
            SetPiece(piece, piece.Position);
        }

        /// <summary>
        /// Clears Board by assigning null all its fields.
        /// </summary>
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
