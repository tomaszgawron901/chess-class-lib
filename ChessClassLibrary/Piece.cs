using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary
{
    interface IPiece
    {
        string Color { get; }
        string Name { get; }
        bool canMoveTo(Point positon);
        void moveTo(Point position);
    }

    public abstract class Piece : IPiece
    {
        protected string color;
        protected bool wasMoved;
        protected string name;

        /// <summary>
        /// Checks whether piece was moved.
        /// </summary>
        public bool WasMoved
        {
            get { return this.wasMoved; }
        }

        /// <summary>
        /// Returns piece color.
        /// </summary>
        public string Color
        {
            get { return color; }
        }

        /// <summary>
        /// Returns piece Name.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        protected ChessBoard board;
        protected Point position;

        /// <summary>
        /// The Position of Piece.
        /// </summary>
        public Point Position
        {
            get { return position; }
            protected set {
                if (!board.CoordinateIsInRange(value))
                    throw new ArgumentException("Cannot set given position.");
                position = value;
            }
        }

        protected Point[] moveSet;
        protected Point[] killSet;


        /// <summary>
        /// Returns piece move set.
        /// </summary>
        public Point[] MoveSet
        {
            get { return moveSet; }
        }

        /// <summary>
        /// Returns piece kill set.
        /// </summary>
        public Point[] KillSet
        {
            get { return killSet; }
        }

        protected Piece(string color, string name, Point position, ChessBoard board)
        {
            this.color = color;
            this.name = name;
            this.wasMoved = false;
            this.board = board;
            this.Position = position;
            this.board.SetPiece(this, this.Position);
        }

        /// <summary>
        /// Performs the appropriate actions when the piece is moving for the first time.
        /// </summary>
        protected virtual void firstMove()
        {
            wasMoved = true;
        }

        /// <summary>
        /// Checks whether Piece can be moved to given position.
        /// </summary>
        /// <param name="position">Movement destination.</param>
        /// <returns></returns>
        public virtual bool canMoveTo(Point position)
        {
            if (!this.board.CoordinateIsInRange(position))
                return false;
            if (pretendMoveAndCheckIfKingIsChecked(position))
                return false;
            if (CanAchieve(position, MoveSet) && board.GetPiece(position) == null)
                return true;
            if(CanAchieve(position, KillSet) && board.GetPiece(position) != null)
            {
                if(board.GetPiece(position).Color != this.Color)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if Piece can move to any position.
        /// </summary>
        public bool canMoveAnywhere()
        {
            for(int x=0; x < board.Width; x++)
            {
                for(int y = 0; y < board.Height; y++)
                {
                    if (canMoveTo(new Point(x, y)))
                        return true;
                }
            }
            return false;
        }

        protected bool pretendMoveAndCheckIfKingIsChecked(Point position)
        {
            Piece pieceAtDestinationPosition = board.GetPiece(position);
            Point currentPiecePosition = Position;
            Position = position;

            board.SetPiece(null, currentPiecePosition);
            board.SetPiece(this, position);
            bool KingIsChecked;
            if (color == "White")
                KingIsChecked = board.WhiteKing.IsChecked();
            else
                KingIsChecked = board.BlackKing.IsChecked();
            board.SetPiece(pieceAtDestinationPosition, position);
            Position = currentPiecePosition;
            board.SetPiece(this, currentPiecePosition);
            return KingIsChecked;
        }

        /// <summary>
        /// Moves Piece to given position.
        /// </summary>
        /// <param name="position">Movement destination.</param>
        public virtual void moveTo(Point position)
        {
            if (!canMoveTo(position))
                throw new ArgumentException("Cannot move to given position.");
            if (board.GetPiece(position) == board.WhiteKing)
                board.WhiteKing = null;
            if (board.GetPiece(position) == board.BlackKing)
                board.BlackKing = null;
            board.SetPiece(null, Position);
            Position = position;
            board.SetPiece(this, position);
            if (!wasMoved)
            {
                firstMove();
            }
        }

        /// <summary>
        /// Checks whether given position can by achieved by one of the given movements.
        /// </summary>
        /// <param name="position">Destination position.</param>
        /// <param name="Movementset">Available movements</param>
        /// <returns></returns>
        public abstract bool CanAchieve(Point position, Point[] Movementset);
    }
}
