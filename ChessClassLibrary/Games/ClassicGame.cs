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
        public ClassicBoard Board { get; private set; }
        public PieceColor CurrentPlayerColor { get; private set; }
        public GameState GameState { get; private set; }

        public ProtectedPieceRule WhiteKing { get; protected set; }
        public ProtectedPieceRule BlackKing { get; protected set; }


        public ClassicGame()
        {
            CreateBoard();
            CurrentPlayerColor = PieceColor.White;
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
            var colorPieces = Board.Where(x => x != null && x.Color == color);
            var kingCount = colorPieces.Count(x => x.Type == PieceType.King);
            var knightCount = colorPieces.Count(x => x.Type == PieceType.Knight);
            var bishopCount = colorPieces.Count(x => x.Type == PieceType.Bishop);
            var otherCount = colorPieces.Count() - kingCount - knightCount - bishopCount;
            return (knightCount <= 1 && bishopCount == 0) || (knightCount == 1 && bishopCount <= 1) && otherCount == 0;
        }

        public void SwapPlayers()
        {
            if (CurrentPlayerColor == PieceColor.White)
            {
                CurrentPlayerColor = PieceColor.Black;
            } else
            {
                CurrentPlayerColor = PieceColor.White;
            }
        }
        #endregion

        #region Create Board
        private void CreateBoard()
        {
            Board = new ClassicBoard(new IPiece[8, 8]);
            WhiteKing = CreateKing(new King(PieceColor.White, new Position(4, 0)));
            BlackKing = CreateKing(new King(PieceColor.Black, new Position(4, 7)));
            

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
            for (int i = 0; i < Board.Width; i++)
            {
                Board.SetPiece(null, new Position(1, row));
            }
        }

        private void InsertPawnRow(PieceColor color, int row)
        {
            if (color == PieceColor.White)
            {
                for (int i = 0; i < Board.Width; i++)
                {
                    Board.SetPiece(CreateProtector(CreateSlowPiece(new WhitePawn(new Position(i, row)))));
                }
            }
            else if (color == PieceColor.Black)
            {
                for (int i = 0; i < Board.Width; i++)
                {
                    Board.SetPiece(CreateProtector(CreateSlowPiece(new BlackPawn(new Position(i, row)))));
                }
            }
        }

        private void InsertRichRow(PieceColor color, int row)
        {
            Board.SetPiece(CreateProtector(CreateFastPiece(new Rook(color, new Position(0, row)))));
            Board.SetPiece(CreateProtector(CreateFastPiece(new Knight(color, new Position(1, row)))));
            Board.SetPiece(CreateProtector(CreateFastPiece(new Bishop(color, new Position(2, row)))));
            Board.SetPiece(CreateProtector(CreateFastPiece(new Queen(color, new Position(3, row)))));
            if (color == PieceColor.White)
            {
                Board.SetPiece(WhiteKing);
            }
            else if (color == PieceColor.Black)
            {
                Board.SetPiece(BlackKing);
            }
            Board.SetPiece(CreateProtector(CreateFastPiece(new Bishop(color, new Position(5, row)))));
            Board.SetPiece(CreateProtector(CreateFastPiece(new Knight(color, new Position(6 , row)))));
            Board.SetPiece(CreateProtector(CreateFastPiece(new Rook(color, new Position(7, row)))));
        }
        #endregion

        #region Move manager
        public bool CanPerformMove(BoardMove move)
        {
            IPiece pickedPiece = Board.GetPiece(move.current);
            if (pickedPiece != null && pickedPiece.Color == CurrentPlayerColor)
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
            if (GameState == GameState.NotStarted)
            {
                GameState = GameState.InProgress;
            }
            Board.GetPiece(move.current).MoveToPosition(move.destination);
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
            return new CastleRule(new ProtectedPieceRule(new KillRule(new MoveRule(new PieceOnBoard(piece, Board)))));
        }

        private BasePieceDecorator CreateSlowPiece(IPiece piece)
        {
            return new KillRule(new MoveRule(new PieceOnBoard(piece, Board)));
        }

        private BasePieceDecorator CreateFastPiece(IPiece piece)
        {
            return new KillRule(new MoveRule(new FastPieceOnBoard(piece, Board)));
        }
        #endregion Create Piece

    }
}
