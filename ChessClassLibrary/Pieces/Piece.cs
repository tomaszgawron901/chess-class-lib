using ChessClassLibrary.enums;

namespace ChessClassLibrary.Pieces
{
    public interface IPiece
    {
        Position Position { get; set; }
        PieceColor Color { get; }
        PieceType Type { get; }
        Position[] MoveSet { get; }
        Position[] KillSet { get; }
        Position? CanKillAchieve(Position position);
        Position? CanMoveAchieve(Position position);
    }

    public abstract class Piece : IPiece
    {
        protected PieceColor color;
        protected PieceType type;
        protected Position position;

        public bool WasMoved { get; protected set; }

        /// <summary>
        /// Returns piece color.
        /// </summary>
        public PieceColor Color
        {
            get { return color; }
        }

        /// <summary>
        /// Returns piece Name.
        /// </summary>
        public PieceType Type
        {
            get { return type; }
        }

        /// <summary>
        /// The Position of Piece.
        /// </summary>
        public Position Position
        {
            get { return position; }
            set
            {
                position = value;
            }
        }

        public abstract Position[] MoveSet { get; }

        public abstract Position[] KillSet { get; }

        protected Piece(PieceColor color, PieceType type, Position position)
        {
            this.color = color;
            this.type = type;
            this.WasMoved = false;
            this.Position = position;
        }

        /// <summary>
        /// Performs the appropriate actions when the piece is moving for the first time.
        /// </summary>
        protected virtual void firstMove()
        {
            WasMoved = true;
        }

        /// <summary>
        /// Checks whether given position can by achieved by one of the given movements.
        /// </summary>
        /// <param name="position">Destination position.</param>
        /// <param name="Movementset">Available movements</param>
        /// <returns></returns>
        public abstract Position? CanMoveAchieve(Position position);
        public abstract Position? CanKillAchieve(Position position);
    }
}
