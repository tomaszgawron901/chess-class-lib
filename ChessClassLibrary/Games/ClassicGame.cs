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

namespace ChessClassLibrary.Games
{
    public class ClassicGame : IGame
    {
        private ClassicBoard board;
        private PieceColor currentPlayerColor;
        public ClassicGame()
        {
            board = new ClassicBoard();
            currentPlayerColor = PieceColor.White;
        }

        public bool CanPerformMove(Move move)
        {
            Piece pickedPiece = this.board.GetPiece(move.current);
            if (pickedPiece == null || pickedPiece.Color == currentPlayerColor)
            {
                return false;
            }

            if (pickedPiece is King)
            {
                return CanMoveKing(pickedPiece as King, move.destination);
            }
            else if (pickedPiece is SlowPiece)
            {
                return CanMoveSlowPiece(pickedPiece as SlowPiece, move.destination);
            }
            else if (pickedPiece is FastPiece)
            {
                return CanMoveFastPiece(pickedPiece as FastPiece, move.destination);
            }
            return false;
        }

        public void PerormMove(Move move)
        {
            throw new NotImplementedException();
        }

        private bool CanMoveSlowPiece(SlowPiece piece, Position destination)
        {
            if (!board.IsInRange(destination)) return false;

            var moveMovement = piece.CanMoveAchieve(destination);
            if (moveMovement != null)
            {
                var destinationPiece = board.GetPiece(destination);
                if (destinationPiece == null)
                {
                    // check if king is not heckmated
                    throw new NotImplementedException();
                }
            }

            var killMovement = piece.CanKillAchieve(destination);
            if (killMovement != null)
            {
                var destinationPiece = board.GetPiece(destination);
                if (destinationPiece == null)
                {
                    // check if king is not heckmated
                    throw new NotImplementedException();
                }
            }
            return false;
        }

        private bool CanMoveFastPiece(FastPiece piece, Position destination)
        {
            // check if can perform fast move
            throw new NotImplementedException();
        }

        private bool CanMoveKing(King king, Position destination)
        {
            // check if can perform slow move and castle move
            throw new NotImplementedException();
        }

        private bool IsKingChecked(King king)
        {
            // check is king is checked
            throw new NotImplementedException();
        }

        private bool IsKingCheckmated(King king)
        {
            // check is king is checkmated
            throw new NotImplementedException();
        }

        private bool IsKingStalemated(King king)
        {
            // check is king is stalemated
            throw new NotImplementedException();
        }

        private bool PretendMoveAndCheckIfKingIsChecked()
        {
            throw new NotImplementedException();
        }
    }
}
