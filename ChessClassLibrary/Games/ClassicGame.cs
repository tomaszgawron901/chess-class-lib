using ChessClassLibrary.Boards;
using ChessClassLibrary.enums;
using ChessClassLibrary.Logic;
using ChessClassLibrary.Logic.Containers;
using ChessClassLibrary.Logic.PieceTransformation;
using ChessClassLibrary.Logic.Rules;
using ChessClassLibrary.Models;
using ChessClassLibrary.Pieces;
using ChessClassLibrary.Pieces.FasePieces;
using ChessClassLibrary.Pieces.SlowPieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessClassLibrary.Games.ClassicGame
{
    public class ClassicGame: BaseClassicGame, IGame, IClassicGame
    {
        public ClassicGame(): base() {}

        protected override bool InsufficientMatingMaterial()
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


        #region Create Board
        protected override void CreateBoard()
        {
            Board = new ClassicBoard(new IPiece[8, 8]);
            WhiteKing = CreateKing(new King(PieceColor.White, new Position(4, 0)));
            BlackKing = CreateKing(new King(PieceColor.Black, new Position(4, 7)));

            WhiteKing.AtackedPiece = BlackKing;
            BlackKing.AtackedPiece = WhiteKing;

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
            for (int i = 0; i < Board.Width; i++)
            {
                Board.SetPiece(CreatePawn(color, new Position(i, row)));
            }
        }

        private void InsertRichRow(PieceColor color, int row)
        {
            Board.SetPiece(CreateRook(color, new Position(0, row)));
            Board.SetPiece(CreateKnight(color, new Position(1, row)));
            Board.SetPiece(CreateBishop(color, new Position(2, row)));
            Board.SetPiece(CreateQueen(color, new Position(3, row)));
            if (color == PieceColor.White)
            {
                Board.SetPiece(WhiteKing);
            }
            else if (color == PieceColor.Black)
            {
                Board.SetPiece(BlackKing);
            }
            Board.SetPiece(CreateBishop(color, new Position(5, row)));
            Board.SetPiece(CreateKnight(color, new Position(6, row)));
            Board.SetPiece(CreateRook(color, new Position(7, row)));
        }
        #endregion

        #region Move manager
        public bool CanPerformMove(BoardMove move)
        {
            IPiece pickedPiece = Board.GetPiece(move.Current);
            if (pickedPiece != null && pickedPiece.Color == CurrentPlayerColor)
            {
                return pickedPiece.GetMoveTo(move.Destination) != null;
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
            Board.GetPiece(move.Current).MoveToPosition(move.Destination);
            AfterMovePerformed();
        }

        public void AfterMovePerformed()
        {
            UpdateGameStatus();
            SwapPlayers();
        }
        #endregion Move manager

        #region Piece Creators
        private BasePieceDecorator CreatePawn(PieceColor color, Position position)
        {
            if (color == PieceColor.White)
            {
                var currentPiece = new WhitePawnFirstMoveRule(new PieceOnBoard(new WhitePawn(position), Board));
                var afterPiece = new FastPieceOnBoard(new Queen(PieceColor.White, position), Board);
                IEnumerable<Position> positions = Enumerable.Range(0, Board.Width).Select(x => new Position(x, Board.Height - 1));

                return CreateProtector(new KillRule(new MoveRule(new AfterMoveToPositionTransformation(currentPiece, afterPiece, positions))));
            }
            else if(color == PieceColor.Black)
            {
                var currentPiece = new BlackPawnFirstMoveRule(new PieceOnBoard(new BlackPawn(position), Board));
                var afterPiece = new FastPieceOnBoard(new Queen(PieceColor.Black, position), Board);
                IEnumerable<Position> positions = Enumerable.Range(0, Board.Width).Select(x => new Position(x, 0));

                return CreateProtector(new KillRule(new MoveRule(new AfterMoveToPositionTransformation(currentPiece, afterPiece, positions))));
            }
            throw new Exception();
        }

        private BasePieceDecorator CreateRook(PieceColor color, Position position)
        {
            return CreateProtector(CreateFastPiece(new Rook(color, position)));
        }

        private BasePieceDecorator CreateKnight(PieceColor color, Position position)
        {
            return CreateProtector(CreateSlowPiece(new Knight(color, position)));
        }

        private BasePieceDecorator CreateBishop(PieceColor color, Position position)
        {
            return CreateProtector(CreateFastPiece(new Bishop(color, position)));
        }

        private BasePieceDecorator CreateQueen(PieceColor color, Position position)
        {
            return CreateProtector(CreateFastPiece(new Queen(color, position)));
        }

        private ProtectedPieceRule CreateKing(IPiece piece)
        {
            return new CastleRule(new ProtectedPieceRule(new KillRule(new MoveRule(new PieceOnBoard(piece, Board)))));
        }
        #endregion
    }
}
