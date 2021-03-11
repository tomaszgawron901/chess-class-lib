using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary
{
    public interface IKing
    {
        bool IsChecked();
    }

    public abstract class King: SlowPiece, IKing
    {
        public King(string color,Point position, ChessBoard board) :
            base(color, "King", position, board)
        {
            this.moveSet = new Point[] {
                new Point(-1, 1), new Point(0, 1), new Point(1, 1),
                new Point(-1, 0), new Point(1, 0),
                new Point(-1, -1), new Point(0, -1), new Point(1, -1),
            };
            this.killSet = this.moveSet;
        }

        public override bool canMoveTo(Point position)
        {
            if (canCastle(position))
                return true;
            return base.canMoveTo(position);
        }

        public override void moveTo(Point position)
        {
            if(canCastle(position))
            {
                Castle(position);
            }else
            {
                base.moveTo(position);
            }

        }

        public bool IsChecked()
        {
            foreach (var row in board.Board)
            {
                foreach (var piece in row)
                {
                    if (piece != null)
                        if (piece.Color != Color && piece.CanAchieve(Position, piece.KillSet))
                            return true;
                }
            }
            return false;
        }

        public bool isCheckMated()
        {
            if (!IsChecked())
                return false;
            foreach (var row in board.Board)
            {
                foreach (var piece in row)
                {
                    if (piece != null && piece.Color == Color && piece.canMoveAnywhere())
                        return false;
                }
            }
            return true;
        }

        public bool isStaleMated()
        {
            if (IsChecked())
                return false;
            if (board.onlyKingsAlive())
                return true;
            foreach (var row in board.Board)
            {
                foreach (var piece in row)
                {
                    if (piece != null && piece.Color == Color && piece.canMoveAnywhere())
                        return false;
                }
            }
            return true;
        }

        protected abstract bool canCastle(Point position);
        protected abstract void Castle(Point position);
    }


    public class WhiteKing:King
    {
        public WhiteKing(Point position, ChessBoard board):
            base("White", position, board)
        {
            this.board.WhiteKing = this;
        }

        protected override void Castle(Point position)
        {
            if (!canCastle(position))
                throw new ArgumentException("Cannot execute that Castle move.");
            if (position == new Point(2, 0))
                leftCastle();
            else if (position == new Point(6, 0))
                rightCastle();
            else
                throw new Exception("Wrong position for a Castle.");
        }

        private void leftCastle()
        {
            if (!canLeftCastle())
                throw new Exception("Cannot execute that Castle move.");
            board.GetPiece(new Point(0, 0)).moveTo(new Point(3, 0));
            position = new Point(2, 0);
            board.SetPiece(this, new Point(2, 0));
            board.SetPiece(null, new Point(4, 0));
        }

        private void rightCastle()
        {
            if (!canRightCastle())
                throw new Exception("Cannot execute that Castle move.");
            board.GetPiece(new Point(7, 0)).moveTo(new Point(5, 0));
            position = new Point(6, 0);
            board.SetPiece(this, new Point(6, 0));
            board.SetPiece(null, new Point(4, 0));
        }

        protected override bool canCastle(Point position)
        {
            if(position == new Point(2, 0))
            {
                if (canLeftCastle())
                    return true;
            }else if(position == new Point(6, 0))
            {
                if (canRightCastle())
                    return true;
            }
            return false;
        }

        private bool canLeftCastle()
        {
            if (IsChecked())
                return false;
            if (wasMoved)
                return false;
            Piece leftRook = board.GetPiece(new Point(0, 0));
            if (leftRook is Rook && !leftRook.WasMoved && leftRook.Color == Color)
            {
                if (board.GetPiece(new Point(1, 0)) != null)
                    return false;
                foreach (var position in new Point[] {new Point(2, 0) , new Point(3, 0) })
                {
                    if (board.GetPiece(position) != null)
                        return false;
                    if (pretendMoveAndCheckIfKingIsChecked(position))
                        return false;
                }
                return true;
            }
            return false;
        }

        private bool canRightCastle()
        {
            if (IsChecked())
                return false;
            if (wasMoved)
                return false;
            Piece rightRook = board.GetPiece(new Point(7, 0));
            if (rightRook is Rook && !rightRook.WasMoved && rightRook.Color == Color)
            {
                foreach (var position in new Point[] { new Point(5, 0), new Point(6, 0) })
                {
                    if (board.GetPiece(position) != null)
                        return false;
                    if (pretendMoveAndCheckIfKingIsChecked(position))
                        return false;
                }
                return true;
            }
            return false;
        }
    }

    public class BlackKing : King
    {
        public BlackKing(Point position, ChessBoard board) :
            base("Black", position, board)
        {
            this.board.BlackKing = this;
        }

        protected override void Castle(Point position)
        {
            if (!canCastle(position))
                throw new ArgumentException("Cannot execute that Castle move.");
            if (position == new Point(2, 7))
                leftCastle();
            else if (position == new Point(6, 7))
                rightCastle();
            else
                throw new Exception("Wrong position for a Castle.");
        }

        private void leftCastle()
        {
            if (!canLeftCastle())
                throw new Exception("Cannot execute that Castle move.");
            board.GetPiece(new Point(0, 7)).moveTo(new Point(3, 7));
            position = new Point(2, 7);
            board.SetPiece(this, new Point(2, 7));
            board.SetPiece(null, new Point(4, 7));
        }

        private void rightCastle()
        {
            if (!canRightCastle())
                throw new Exception("Cannot execute that Castle move.");
            board.GetPiece(new Point(7, 7)).moveTo(new Point(5, 7));
            position = new Point(6, 7);
            board.SetPiece(this, new Point(6, 7));
            board.SetPiece(null, new Point(4, 7));
        }

        protected override bool canCastle(Point position)
        {
            if (position == new Point(2, 7))
            {
                if (canLeftCastle())
                    return true;
            }
            else if (position == new Point(6, 7))
            {
                if (canRightCastle())
                    return true;
            }
            return false;
        }

        private bool canLeftCastle()
        {
            if (IsChecked())
                return false;
            if (wasMoved)
                return false;
            Piece leftRook = board.GetPiece(new Point(0, 7));
            if (leftRook is Rook && !leftRook.WasMoved && leftRook.Color == Color)
            {
                if (board.GetPiece(new Point(1, 7)) != null)
                    return false;
                foreach (var position in new Point[] { new Point(2, 7), new Point(3, 7) })
                {
                    if (board.GetPiece(position) != null)
                        return false;
                    if (pretendMoveAndCheckIfKingIsChecked(position))
                        return false;
                }
                return true;
            }
            return false;
        }

        private bool canRightCastle()
        {
            if (IsChecked())
                return false;
            if (wasMoved)
                return false;
            Piece rightRook = board.GetPiece(new Point(7, 7));
            if (rightRook is Rook && !rightRook.WasMoved && rightRook.Color == Color)
            {
                foreach (var position in new Point[] { new Point(5, 7), new Point(6, 7) })
                {
                    if (board.GetPiece(position) != null)
                        return false;
                    if (pretendMoveAndCheckIfKingIsChecked(position))
                        return false;
                }
                return true;
            }
            return false;
        }
    }

}
