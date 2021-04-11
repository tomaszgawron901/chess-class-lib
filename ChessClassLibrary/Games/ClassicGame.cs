using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.Logic;
using ChessClassLibrary.Logic.Containers;
using ChessClassLibrary.Logic.Rules;
using ChessClassLibrary.Pieces;
using ChessClassLibrary.Pieces.FasePieces;
using ChessClassLibrary.Pieces.SlowPieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Games.ClassicGame
{
    public class ClassicGame : IGame
    {
        private ClassicBoard board;
        private PieceColor currentPlayerColor;
        public GameState GameState { get; private set; }

        private ProtectedPieceRule WhiteKing { get; set; }
        private ProtectedPieceRule BlackKing { get; set; }


        public ClassicGame()
        {
            CreateBoard();
            currentPlayerColor = PieceColor.White;
            GameState = GameState.NotStarted;
        }

        #region Game status
        public void UpdateGameStatus()
        {
            WhiteKing.UpdateState();
            BlackKing.UpdateState();
            if (WhiteKing.IsChecked 
                || WhiteKing.IsCheckmated 
                || WhiteKing.IsStalemated 
                || BlackKing.IsChecked 
                || BlackKing.IsCheckmated
                || BlackKing.IsStalemated
                || InsufficientMatingMaterial())
            {
                GameState = GameState.Ended;
            }
        }

        public bool InsufficientMatingMaterial()
        {
            return InsufficientMatingMaterial(PieceColor.White) && InsufficientMatingMaterial(PieceColor.Black);
        }

        private bool InsufficientMatingMaterial(PieceColor color)
        {
            var colorPieces = board.Where(x => x != null && x.Color == color);
            var kingCount = colorPieces.Count(x => x.Type == PieceType.King);
            var knightCount = colorPieces.Count(x => x.Type == PieceType.Knight);
            var bishopCount = colorPieces.Count(x => x.Type == PieceType.Bishop);
            var otherCount = colorPieces.Count() - kingCount - knightCount - bishopCount;
            return (knightCount <= 1 && bishopCount == 0) || (knightCount == 1 && bishopCount <= 1) && otherCount == 0;
        }

        public void SwapPlayers()
        {
            if (currentPlayerColor == PieceColor.White)
            {
                currentPlayerColor = PieceColor.Black;
            } else
            {
                currentPlayerColor = PieceColor.White;
            }
        }
        #endregion

        #region Create Board
        private void CreateBoard()
        {
            WhiteKing = CreateKing(new King(PieceColor.White, new Position(0, 4)));
            BlackKing = CreateKing(new King(PieceColor.Black, new Position(7, 4)));
            board = new ClassicBoard(new IPiece[8, 8]);

            InsertRichRow(PieceColor.White, 0);
            InsertPawnRow(PieceColor.White, 1);
            InsertEmptyRow(2);
            InsertEmptyRow(3);
            InsertEmptyRow(4);
            InsertEmptyRow(5);
            InsertPawnRow(PieceColor.Black, 6);
            InsertRichRow(PieceColor.Black, 7);

            
        }

        private void InsertEmptyRow(int row)
        {
            for (int i = 0; i < board.Width; i++)
            {
                board.SetPiece(null, new Position(row, 1));
            }
        }

        private void InsertPawnRow(PieceColor color, int row)
        {
            if (color == PieceColor.White)
            {
                for (int i = 0; i < board.Width; i++)
                {
                    board.SetPiece(CreateProtector(CreateSlowPiece(new WhitePawn(new Position(row, i)))));
                }
            }
            else if (color == PieceColor.Black)
            {
                for (int i = 0; i < board.Width; i++)
                {
                    board.SetPiece(CreateProtector(CreateSlowPiece(new BlackPawn(new Position(row, i)))));
                }
            }
        }

        private void InsertRichRow(PieceColor color, int row)
        {
            board.SetPiece(CreateProtector(CreateFastPiece(new Rook(color, new Position(row, 0)))));
            board.SetPiece(CreateProtector(CreateFastPiece(new Knight(color, new Position(row, 1)))));
            board.SetPiece(CreateProtector(CreateFastPiece(new Bishop(color, new Position(row, 2)))));
            board.SetPiece(CreateProtector(CreateFastPiece(new Queen(color, new Position(row, 3)))));
            if (color == PieceColor.White)
            {
                board.SetPiece(WhiteKing);
            }
            else if (color == PieceColor.Black)
            {
                board.SetPiece(BlackKing);
            }
            board.SetPiece(CreateProtector(CreateFastPiece(new Bishop(color, new Position(row, 5)))));
            board.SetPiece(CreateProtector(CreateFastPiece(new Knight(color, new Position(row, 6)))));
            board.SetPiece(CreateProtector(CreateFastPiece(new Rook(color, new Position(row, 7)))));
        }
        #endregion

        #region Move manager
        public bool CanPerformMove(BoardMove move)
        {
            IPiece pickedPiece = board.GetPiece(move.current);
            if (pickedPiece != null && pickedPiece.Color == currentPlayerColor)
            {
                return pickedPiece.GetMoveTo(move.destination) != null;
            }
            return false;
        }

        public void TryPerformMove(BoardMove move)
        {
            if (CanPerformMove(move))
            {
                PerformMove(move);
            }
            else
            {
                throw new Exception();
            }
        }

        public void PerformMove(BoardMove move)
        {
            board.GetPiece(move.current).MoveToPosition(move.destination);
            AfterMovePerformed();
        }

        public void AfterMovePerformed()
        {
            UpdateGameStatus();
            SwapPlayers();
        }
        #endregion Move manager

        #region Create Piece

        #region Protector
        private BasePieceDecorator CreateWhiteProtector(BasePieceDecorator piece)
        {
            return new ProtectAttackRule(piece, WhiteKing, BlackKing);
        }

        private BasePieceDecorator CreateBlackProtector(BasePieceDecorator piece)
        {
            return new ProtectAttackRule(piece, BlackKing, WhiteKing);
        }

        private BasePieceDecorator CreateProtector(BasePieceDecorator piece)
        {
            if (piece.Color == PieceColor.White)
            {
                return this.CreateWhiteProtector(piece);
            }
            else if (piece.Color == PieceColor.Black)
            {
                return this.CreateBlackProtector(piece);
            }
            return piece;
        }
        #endregion Protector

        private ProtectedPieceRule CreateKing(IPiece piece)
        {
            return new CastleRule(new ProtectedPieceRule(new KillRule(new MoveRule(new PieceOnBoard(piece, board)))));
        }

        private BasePieceDecorator CreateSlowPiece(IPiece piece)
        {
            return this.CreateProtector(new KillRule(new MoveRule(new PieceOnBoard(piece, board))));
        }

        private BasePieceDecorator CreateFastPiece(IPiece piece)
        {
            return this.CreateProtector(new KillRule(new MoveRule(new FastPieceOnBoard(piece, board))));
        }
        #endregion Create Piece

    }
}
