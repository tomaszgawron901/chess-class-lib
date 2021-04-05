using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary
{
    public class ClassicChessBoard
    {
        private static int width = 8;
        private static int height = 8;
        /// <summary>
        /// The width of the board.
        /// </summary>
        public int Width
        {
            get { return width; }
        }
        /// <summary>
        /// The height of the board.
        /// </summary>
        public int Height
        {
            get { return height; }
        }

        private Piece[][] board;
        private King whiteKing;
        private King blackKing;

        /// <summary>
        /// The White King Piece.
        /// </summary>
        public King WhiteKing
        {
            get { return whiteKing; }
            set
            {
                if(value != null)
                {
                    if (whiteKing != null)
                        throw new Exception("There is a White King on the board already.");
                    if (value.Color != "White")
                        throw new ArgumentException("Wrong color for a White King.");
                }
                whiteKing = value;
            }
        }

        /// <summary>
        /// The Black King Piece.
        /// </summary>
        public King BlackKing
        {
            get { return blackKing; }
            set
            {
                if(value != null)
                {
                    if (blackKing != null)
                        throw new Exception("There is a Black King on the board already.");
                    if (value.Color != "Black")
                        throw new ArgumentException("Wrong color for the Black King.");
                }
                blackKing = value;
            }
        }

        /// <summary>
        /// Returns current board.
        /// </summary>
        public Piece[][] Board
        {
            get { return board; }
        }

        public ClassicChessBoard()
        {
            create();
        }

        /// <summary>
        /// Checks whether Point position is in the range of the board.
        /// </summary>
        /// <param name="position">Point to check.</param>
        /// <returns>True when Point is in the range, otherwise false.</returns>
        public bool CoordinateIsInRange(Point position)
        {
            if (position.X < 0 || position.X >= width || position.Y < 0 || position.Y >= height)
                return false;
            return true;
        }

        /// <summary>
        /// Returns Piece from given position.
        /// </summary>
        /// <param name="position">Position on the board.</param>
        /// <returns>The Piece from given position.</returns>
        public Piece GetPiece(Point position)
        {
            if (!CoordinateIsInRange(position))
                throw new ArgumentOutOfRangeException("Given position is out of chess board range.");
            return board[position.Y][position.X];
        }

        /// <summary>
        /// Sets given Piece at given position.
        /// </summary>
        /// <param name="piece">Piece to set.</param>
        /// <param name="position">Position.</param>
        public void SetPiece(Piece piece, Point position)
        {
            if (!CoordinateIsInRange(position))
                throw new ArgumentOutOfRangeException("Given position is out of chess board range.");
            if (piece != null && piece.Position != position)
                throw new ArgumentException("Piece position does not match to given position.");
            board[position.Y][position.X] = piece;
        }

        private void create()
        {
            this.board = new Piece[][] {
                new Piece[8],
                new Piece[8],
                new Piece[8],
                new Piece[8],
                new Piece[8],
                new Piece[8],
                new Piece[8],
                new Piece[8],
            };
            createRichRow(0, "White");
            createPawnRow(1, "White");

            createPawnRow(6, "Black");
            createRichRow(7, "Black");
        }

        private void createPawnRow(int row, string color)
        {
            if (color != "White" && color != "Black")
                throw new ArgumentException("Piece can be 'White' or 'Black'.");
            if (color == "White")
            {
                for (int x = 0; x < Width; x++)
                {
                    new WhitePawn(new Point(x, row), this);
                }
            }
            else
            {
                for (int x = 0; x < Width; x++)
                {
                    new BlackPawn(new Point(x, row), this);
                }
            }
        }

        private void createRichRow(int row, string color)
        {
            if (color != "White" && color != "Black")
                throw new ArgumentException("Piece can be 'White' or 'Black'.");
            new Rook(color, new Point(0, row), this);
            new Knight(color, new Point(1, row), this);
            new Bishop(color, new Point(2, row), this);
            new Queen(color, new Point(3, row), this);
            if (color == "White")
                new WhiteKing(new Point(4, row), this);
            else
                new BlackKing(new Point(4, row), this);
            new Bishop(color, new Point(5, row), this);
            new Knight(color, new Point(6, row), this);
            new Rook(color, new Point(7, row), this);
        }

        /// <summary>
        /// Returns current state of the board.
        /// </summary>
        /// <returns></returns>
        public GameStates GetState()
        {
            if (this.WhiteKing.isCheckMated())
                return GameStates.blackWin;
            if (this.BlackKing.isCheckMated())
                return GameStates.whiteWin;
            if (this.whiteKing.isStaleMated() || this.blackKing.isStaleMated())
                return GameStates.stalemate;
            if (this.WhiteKing.IsChecked())
                return GameStates.whiteCheck;
            if (this.BlackKing.IsChecked())
                return GameStates.blackCheck;
            return GameStates.inProgress;
        }

        /// <summary>
        /// Check if there are only Kings on the Board.
        /// </summary>
        /// <returns></returns>
        public bool onlyKingsAlive()
        {
            if (WhiteKing == null || blackKing == null)
                return false;
            foreach (var row in Board)
            {
                foreach(var piece in row)
                {
                    if (piece != null && !(piece is King))
                        return false;
                }
            }
            return true;
        }
    }
}
