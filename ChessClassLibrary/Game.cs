using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary
{
    /// <summary>
    /// Desctibes game state.
    /// </summary>
    public enum GameStates { 
        /// <summary>Game in progress.</summary>
        inProgress,
        /// <summary>Black King checkmated. White Player wins.</summary>
        whiteWin,
        /// <summary>White King checkmated. Black Player wins.</summary>
        blackWin,
        /// <summary>White King checked.</summary>
        whiteCheck,
        /// <summary>Black King checked.</summary>
        blackCheck,
        /// <summary>No one wins. Game ended.</summary>
        stalemate
    };
    /// <summary>
    /// Types of players.
    /// </summary>
    public enum Players { WhitePlayer, BlackPlayer, None };
    /// <summary>
    /// Types of Pieces.
    /// </summary>
    public enum PieceTypes {Pawn, Rook, Knight, Bishop, Queen, King,
        /// <summary>Empty field on a board. </summary>
        Empty};

    /// <summary>
    /// Class responsible for all operations on Board.
    /// </summary>
    public class BoardManager: IEnumerable<IPieceManager>
    {
        private sealed class BoardIterator : IEnumerator<IPieceManager>
        {
            private BoardManager board;
            private int X;
            private int Y;
            public BoardIterator(BoardManager board)
            {
                this.board = board;
                X = -1;
                Y = 0;
            }

            public IPieceManager Current => board.GetPiece(X, Y);

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                X += 1;
                if (X >= board.BoardWidth)
                {
                    X = 0;
                    Y += 1;
                }
                return (X < board.BoardWidth && Y < board.BoardHeight);
            }

            public void Reset()
            {
                X = -1;
                Y = 0;
            }
        }

        private ChessBoard board;
        private Game game;
        public BoardManager(ChessBoard board ,Game game)
        {
            if(game is null)
                throw new Exception("Game does not exist.");
            if (board is null)
                throw new Exception("ChessBoard does not exist.");
            this.game = game;
            this.board = board;
        }


        /// <summary>
        /// Returns White King peice manager.
        /// </summary>
        /// <returns></returns>
        public PieceManager getWhiteKing()
        {
            return new PieceManager(board.WhiteKing, game);
        }

        /// <summary>
        /// Returns Black King piece manager.
        /// </summary>
        /// <returns></returns>
        public PieceManager getBlackKing()
        {
            return new PieceManager(board.BlackKing, game);
        }

        /// <summary>
        /// Gets the width of the board.
        /// </summary>
        public int BoardWidth
        {
            get { return board.Width; }
        }

        /// <summary>
        /// Gest the height of the board.
        /// </summary>
        public int BoardHeight
        {
            get { return board.Height; }
        }

        /// <summary>
        /// Returns a PieceManager from given position.
        /// </summary>
        /// <param name="x">Horizontal coordinate.</param>
        /// <param name="y">Vertical coordinate.</param>
        /// <returns>PieceManager from given position.</returns>
        public IPieceManager GetPiece(int x, int y)
        {
            Piece p = board.GetPiece(new Point(x, y));
            if (p is null)
                return new EmptyPieceManager(x, y, game);
            return new PieceManager(p, game);
        }

        /// <summary>
        /// Returns current state of the board.
        /// </summary>
        /// <returns></returns>
        public GameStates GetState()
        {
            return board.GetState();
        }

        public IEnumerator<IPieceManager> GetEnumerator()
        {
            return new BoardIterator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// Is responsible for all operations on Pieces or empty fields.
    /// </summary>
    public interface IPieceManager
    {
        /// <summary>
        /// Gets Piece owner.
        /// </summary>
        Players Owner { get; }
        /// <summary>
        /// Gets Piece type.
        /// </summary>
        PieceTypes Type { get; }
        /// <summary>
        /// Gets piece Horizontal position.
        /// </summary>
        int PieceXPosition { get; }
        /// <summary>
        /// Gets piece Vertical position.
        /// </summary>
        int PieceYPosition { get; }
        /// <summary>
        /// Checks whether Piece can be moved to given position.
        /// </summary>
        /// <param name="x">Horizontal coordinate.</param>
        /// <param name="y">Vertical coordinate.</param>
        /// <returns></returns>
        bool canMoveTo(int x, int y);
        /// <summary>
        /// Moves Piece to given position.
        /// </summary>
        /// <param name="x">Horizontal coordinate.</param>
        /// <param name="y">Vertical coordinate.</param>
        void moveTo(int x, int y);
    }

    /// <summary>
    /// Class responsible for operations on non nullable Piece.
    /// </summary>
    public class PieceManager: IPieceManager
    {
        private Piece piece;
        private Game game;
        public PieceManager(Piece piece, Game game)
        {
            if (game is null)
                throw new Exception("Game does not exist.");
            if (piece is null)
                throw new Exception("Piece does not exist.");
            this.piece = piece;
            this.game = game;
        }

        /// <summary>
        /// Gets Piece owner.
        /// </summary>
        public Players Owner
        {
            get {
                if (piece is null)
                    throw new Exception("Piece does not exist.");
                switch (piece.Color)
                {
                    case "White":
                        return Players.WhitePlayer;
                    case "Black":
                        return Players.BlackPlayer;
                    default:
                        throw new Exception("Unexpected ovner.");
                }
            }
        }

        /// <summary>
        /// Gets Piece type.
        /// </summary>
        public PieceTypes Type
        {
            get {
                if (piece is null)
                    throw new Exception("Piece does not exist.");
                switch(piece.Name)
                {
                    case "Pawn":
                        return PieceTypes.Pawn;
                    case "Rook":
                        return PieceTypes.Rook;
                    case "Knight":
                        return PieceTypes.Knight;
                    case "Bishop":
                        return PieceTypes.Bishop;
                    case "Queen":
                        return PieceTypes.Queen;
                    case "King":
                        return PieceTypes.King;
                    default:
                        throw new Exception("Unexpected Piece type.");
                }
            }
        }

        /// <summary>
        /// Gets piece Horizontal position.
        /// </summary>
        public int PieceXPosition
        {
            get { return piece.Position.X; }
        }

        /// <summary>
        /// Gets piece Vertical position.
        /// </summary>
        public int PieceYPosition
        {
            get { return piece.Position.Y; }
        }

        /// <summary>
        /// Checks whether Piece can be moved to given position.
        /// </summary>
        /// <param name="x">Horizontal coordinate.</param>
        /// <param name="y">Vertical coordinate.</param>
        /// <returns></returns>
        public bool canMoveTo(int x, int y)
        {
            if (game.PlayerTurn == Players.WhitePlayer && Owner == Players.BlackPlayer)
                return false;
            if (game.PlayerTurn == Players.BlackPlayer && Owner == Players.WhitePlayer)
                return false;
            if (game.GameState == GameStates.blackWin || game.GameState == GameStates.whiteWin || game.GameState == GameStates.stalemate)
                return false;
            if (piece is null)
                return false;
            return piece.canMoveTo(new Point(x, y));
        }

        /// <summary>
        /// Moves Piece to given position.
        /// </summary>
        /// <param name="x">Horizontal coordinate.</param>
        /// <param name="y">Vertical coordinate.</param>
        public void moveTo(int x, int y)
        {
            if (game.GameState == GameStates.blackWin || game.GameState == GameStates.whiteWin)
                throw new Exception("Cannot move, the game is over.");
            if (piece is null)
                throw new Exception("Cannot move not existed Piece.");
            if (!canMoveTo(x, y))
                throw new Exception("Cannot move Piece to given position.");
            piece.moveTo(new Point(x, y));
            game.AnotherPlayerTurn();
            game.UpdateState();
        }
    }

    /// <summary>
    /// Class responsible for operations on empty field.
    /// </summary>
    public class EmptyPieceManager: IPieceManager
    {
        private int x;
        private int y;
        private Game game;
        /// <param name="x">Horizontal coordinate.</param>
        /// <param name="y">Vertical coordinate.</param>
        public EmptyPieceManager(int x, int y, Game game)
        {
            this.x = x;
            this.y = y;
            this.game = game;
        }

        /// <summary>
        /// Gets Piece owner.
        /// </summary>
        public Players Owner
        {
            get
            {
                return Players.None;
            }
        }

        /// <summary>
        /// Gets Piece type.
        /// </summary>
        public PieceTypes Type
        {
            get
            {
                return PieceTypes.Empty;
            }
        }

        /// <summary>
        /// Gets piece Horizontal position.
        /// </summary>
        public int PieceXPosition
        {
            get { return x; }
        }

        /// <summary>
        /// Gets piece Vertical position.
        /// </summary>
        public int PieceYPosition
        {
            get { return y; }
        }

        /// <summary>
        /// Checks whether Piece can be moved to given position.
        /// </summary>
        /// <param name="x">Horizontal coordinate.</param>
        /// <param name="y">Vertical coordinate.</param>
        /// <returns></returns>
        public bool canMoveTo(int x, int y)
        {
            return false;
        }

        /// <summary>
        /// Moves Piece to given position.
        /// </summary>
        /// <param name="x">Horizontal coordinate.</param>
        /// <param name="y">Vertical coordinate.</param>
        public void moveTo(int x, int y)
        {
            throw new Exception("Cannot move not existed Piece.");
        }
    }

    /// <summary>
    /// Interface which enables user to communicate with game.
    /// </summary>
    public interface UserGame
    {
        /// <summary>
        /// Gets board manager.
        /// </summary>
        BoardManager Board { get; }

        /// <summary>
        /// Gets current game state.
        /// </summary>
        GameStates GameState { get; }

        /// <summary>
        /// Gets current playing player.
        /// </summary>
        Players PlayerTurn { get; }
    }

    /// <summary>
    /// Main class. Controls subclasses.
    /// </summary>
    public class Game: UserGame
    {
        private BoardManager board;
        private GameStates gameState;
        private Players playerTurn;

        /// <summary>
        /// Gets board manager.
        /// </summary>
        public BoardManager Board
        {
            get { return board; }
        }
        /// <summary>
        /// Gets current game state.
        /// </summary>
        public GameStates GameState
        {
            get { return gameState; }
        }
        /// <summary>
        /// Gets current playing player.
        /// </summary>
        public Players PlayerTurn
        {
            get { return playerTurn; }
        }

        public Game()
        {
            board = new BoardManager(new ChessBoard(), this);
            gameState = GameStates.inProgress;
            playerTurn = Players.WhitePlayer;
        }

        /// <summary>
        /// Change current playing player.
        /// </summary>
        public void AnotherPlayerTurn()
        {
            if (PlayerTurn == Players.WhitePlayer)
                playerTurn = Players.BlackPlayer;
            else if(PlayerTurn == Players.BlackPlayer)
                playerTurn = Players.WhitePlayer;
        }

        /// <summary>
        /// Updates state.
        /// </summary>
        public void UpdateState()
        {
            gameState = board.GetState();
        }
    }
}
