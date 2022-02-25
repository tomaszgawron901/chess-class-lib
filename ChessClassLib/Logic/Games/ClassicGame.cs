using ChessClassLib.Helpers;
using ChessClassLib.Logic.PieceRules.BasePieceRules;
using ChessClassLib.Logic.PieceRules.PieceRuleDecorators;
using ChessClassLib.Logic.PieceRules.PieceRuleDecorators.CastleRules;
using ChessClassLib.Logic.PieceRules.PieceRuleDecorators.ProtectionRules;
using ChessClassLib.Pieces;
using ChessClassLib.Enums;
using ChessClassLib.Logic.Boards;
using ChessClassLib.Logic.Games;
using ChessClassLib.Models;
using ChessClassLib.Pieces.SlowPieces;
using System.Linq;

namespace hessClassLibrary.Logic.Games
{
    /// <summary>
    /// Standard Chess Game.
    /// </summary>
    public class ClassicGame: BaseClassicGame
    {
        public ClassicGame(): base() {}

        protected override bool InsufficientMatingMaterial()
        {
            return InsufficientMatingMaterial(PieceColor.White) && InsufficientMatingMaterial(PieceColor.Black);
        }

        /// <summary>
        /// Checks if there is enough Pieces with given color to play the Game.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
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

            WhiteKingManager = new KingStateProvider(null, Board);
            var whiteKingPiece = new King(PieceColor.White, new Position(4, 0))
                .AddPieceOnBoard(Board)
                .AddKillRule()
                .AddMoveRule()
                .AddLeftCastleRule(WhiteKingManager)
                .AddRightCastleRule(WhiteKingManager)
                .AddSelfProtectRule();
            WhiteKingManager.Piece = whiteKingPiece;

            BlackKingManager = new KingStateProvider(null, Board);
            var blackKingPiece = new King(PieceColor.Black, new Position(4, 7))
                .AddPieceOnBoard(Board)
                .AddKillRule()
                .AddMoveRule()
                .AddLeftCastleRule(BlackKingManager)
                .AddRightCastleRule(BlackKingManager)
                .AddSelfProtectRule();
            BlackKingManager.Piece = blackKingPiece;

            InsertRichRow(PieceColor.White, 0);
            InsertPawnRow(PieceColor.White, 1);
            InsertEmptyRow(2);
            InsertEmptyRow(3);
            InsertEmptyRow(4);
            InsertEmptyRow(5);
            InsertPawnRow(PieceColor.Black, 6);
            InsertRichRow(PieceColor.Black, 7);
        }

        private void InsertRichRow(PieceColor color, int row)
        {
            Board.SetPiece(CreateRook(color, new Position(0, row)));
            Board.SetPiece(CreateKnight(color, new Position(1, row)));
            Board.SetPiece(CreateBishop(color, new Position(2, row)));
            Board.SetPiece(CreateQueen(color, new Position(3, row)));
            if (color == PieceColor.White)
            {
                Board.SetPiece(WhiteKingManager.Piece);
            }
            else if (color == PieceColor.Black)
            {
                Board.SetPiece(BlackKingManager.Piece);
            }
            Board.SetPiece(CreateBishop(color, new Position(5, row)));
            Board.SetPiece(CreateKnight(color, new Position(6, row)));
            Board.SetPiece(CreateRook(color, new Position(7, row)));
        }
        #endregion
    }
}
