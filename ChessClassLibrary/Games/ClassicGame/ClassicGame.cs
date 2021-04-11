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
            UpdateKingState(board.WhiteKing);
            UpdateKingState(board.BlackKing);
            if (board.WhiteKing.IsChecked 
                || board.WhiteKing.IsCheckmated 
                || board.WhiteKing.IsStalemated 
                || board.BlackKing.IsChecked 
                || board.BlackKing.IsCheckmated 
                || board.BlackKing.IsStalemated
                || InsufficientMatingMaterial())
            {
                GameState = GameState.Ended;
            }
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

        #region Board
        private void CreateBoard()
        {
            WhiteKing = CreateKing(new King(PieceColor.White, new Position(0, 4)));
            BlackKing = CreateKing(new King(PieceColor.Black, new Position(7, 4)));
            var pieces = new IPiece[8, 8];

            InsertRichRow(PieceColor.White, 0, pieces);
            InsertPawnRow(PieceColor.White, 1, pieces);
            InsertEmptyRow(2, pieces);
            InsertEmptyRow(3, pieces);
            InsertEmptyRow(4, pieces);
            InsertEmptyRow(5, pieces);
            InsertPawnRow(PieceColor.Black, 6, pieces);
            InsertRichRow(PieceColor.Black, 7, pieces);

            board = new ClassicBoard(pieces);
        }

        private void InsertEmptyRow(int row, IPiece[,] pieces)
        {
            int col = pieces.GetLength(1);
            for (int i = 0; i < col; i++)
            {
                pieces[row, col] = null;
            }
        }

        private void InsertPawnRow(PieceColor color, int row, IPiece[,] pieces)
        {
            int col = pieces.GetLength(1);
            if (color == PieceColor.White)
            {
                for (int i = 0; i < col; i++)
                {
                    pieces[row, col] = CreateProtector(CreateSlowPiece(new WhitePawn(new Position(row, col))));
                }
            }
            else if (color == PieceColor.Black)
            {
                for (int i = 0; i < col; i++)
                {
                    pieces[row, col] = CreateProtector(CreateSlowPiece(new WhitePawn(new Position(row, col))));
                }
            }
        }

        private void InsertRichRow(PieceColor color, int row, IPiece[,] pieces)
        {
            pieces[row, 0] = CreateProtector(CreateFastPiece(new Rook(color, new Position(row, 0))));

            pieces[row, 1] = CreateProtector(CreateFastPiece(new Knight(color, new Position(row, 1))));
            pieces[row, 2] = CreateProtector(CreateFastPiece(new Bishop(color, new Position(row, 2))));
            pieces[row, 3] = CreateProtector(CreateFastPiece(new Queen(color, new Position(row, 3))));
            if (color == PieceColor.White)
            {
                pieces[row, 4] = WhiteKing;
            }
            else if (color == PieceColor.Black)
            {
                pieces[row, 4] = BlackKing;
            }
            pieces[row, 5] = CreateProtector(CreateFastPiece(new Bishop(color, new Position(row, 5))));
            pieces[row, 6] = CreateProtector(CreateFastPiece(new Knight(color, new Position(row, 6))));
            pieces[row, 7] = CreateProtector(CreateFastPiece(new Rook(color, new Position(row, 7))));
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


            AfterMovePerformed();
        }

        public void AfterMovePerformed()
        {
            UpdateGameStatus();
            SwapPlayers();
        }
        #endregion Move manager



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
        #endregion

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
    }
}
