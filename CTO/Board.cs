using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTO
{
    public class Board
    {
        public List<Piece> Pieces { get; set; } = new List<Piece>();
        public List<(Move, int)> Moves { get; set; } = new List<(Move, int)>();
        public Stack<Move> MoveHistory { get; set; } = new Stack<Move>();
        public bool IsWhiteToMove { get; set; } = true;
        public Square EnPassant { get; set; } = null;

        public Board()
        {
            for (int i = 0; i < 8; i++)
            {
                Pieces.Add(new Piece(PieceType.Pawn, true, new Square(i, 1)));
                Pieces.Add(new Piece(PieceType.Pawn, false, new Square(i, 6)));
            }

            for (int i = 0; i < 2; i++)
            {
                bool isWhite = i == 0;
                int rank = isWhite ? 0 : 7;
                Pieces.Add(new Piece(PieceType.Rook, isWhite, new Square(0, rank)));
                Pieces.Add(new Piece(PieceType.Knight, isWhite, new Square(1, rank)));
                Pieces.Add(new Piece(PieceType.Bishop, isWhite, new Square(2, rank)));
                Pieces.Add(new Piece(PieceType.Queen, isWhite, new Square(3, rank)));
                Pieces.Add(new Piece(PieceType.King, isWhite, new Square(4, rank)));
                Pieces.Add(new Piece(PieceType.Bishop, isWhite, new Square(5, rank)));
                Pieces.Add(new Piece(PieceType.Knight, isWhite, new Square(6, rank)));
                Pieces.Add(new Piece(PieceType.Rook, isWhite, new Square(7, rank)));
            }

            GenerateLegalMoves();
        }

        public Piece[,] ChessBoard() 
        {             
            Piece[,] board = new Piece[8, 8];

            foreach (var piece in Pieces)
            {
                board[piece.Position.File, piece.Position.Rank] = piece;
            }

            return board;
        }

        public void GenerateMoves()
        {
            for (int i = 0; i < Pieces.Count; i++)
            {
                if (IsWhiteToMove == Pieces[i].IsWhite)
                {
                    Moves.AddRange(GenerateMoves(Pieces[i]));
                }
            }
        }

        public void GenerateLegalMoves()
        {
            List<Piece> pieces = new List<Piece>(Pieces);

            Moves.Clear();
            foreach (var piece in pieces)
            {
                if (IsWhiteToMove == piece.IsWhite)
                {
                    var pieceMoves = GenerateMoves(piece);
                    foreach (var move in pieceMoves)
                    {
                        if (move.Item2 == MoveHistory.Count)
                        {
                            MakeMove(move.Item1, true);
                            Piece attacked = Pieces.FirstOrDefault(p => p.Type == PieceType.King && p.IsWhite != IsWhiteToMove);
                            if (attacked != null && !IsAttacked(attacked.Position, !IsWhiteToMove))
                            {
                                Moves.Add(move);
                            }
                            UndoMove();
                        }
                    }
                }
            }
        }

        public List<(Move, int)> GenerateMoves(Piece piece, bool includeCastling = true)
        {
            List<(Move, int)> moves = new List<(Move, int)>();

            switch (piece.Type)
            {
                case PieceType.Pawn:
                    moves.AddRange(GeneratePawnMoves(piece));
                    break;
                case PieceType.Knight:
                    moves.AddRange(GenerateKnightMoves(piece));
                    break;
                case PieceType.Bishop:
                    moves.AddRange(GenerateBishopMoves(piece));
                    break;
                case PieceType.Rook:
                    moves.AddRange(GenerateRookMoves(piece));
                    break;
                case PieceType.Queen:
                    moves.AddRange(GenerateQueenMoves(piece));
                    break;
                case PieceType.King:
                    moves.AddRange(GenerateKingMoves(piece, includeCastling));
                    break;
            }

            return moves;
        }

        public List<(Move, int)> GeneratePawnMoves(Piece piece)
        {
            List<(Move, int)> moves = new List<(Move, int)>();
            int direction = piece.IsWhite ? 1 : -1;
            Square from = piece.Position;

            Square to = new Square(from.File, from.Rank + direction);
            if (Pieces.All(p => p.Position.File != to.File || p.Position.Rank != to.Rank))
            {
                if (to.Rank == 7 || to.Rank == 0)
                {
                    for(int i = 0; i < 4; i++)
                    {
                        moves.Add((new Move(from, to, piece, null, PieceType.Queen - i), MoveHistory.Count));
                    }
                }
                else
                {
                    moves.Add((new Move(from, to, piece), MoveHistory.Count));
                }

                if ((piece.IsWhite && from.Rank == 1) || (!piece.IsWhite && from.Rank == 6))
                {
                    to = new Square(from.File, from.Rank + 2 * direction);
                    if (Pieces.All(p => p.Position.File != to.File || p.Position.Rank != to.Rank))
                    {
                        moves.Add((new Move(from, to, piece), MoveHistory.Count));
                    }
                }
            }

            for (int fileOffset = -1; fileOffset <= 1; fileOffset += 2)
            {
                to = new Square(from.File + fileOffset, from.Rank + direction);
                if (Pieces.Any(p => p.Position.File == to.File && p.Position.Rank == to.Rank && p.IsWhite != piece.IsWhite))
                {
                    Piece captured = Pieces.FirstOrDefault(p => p.Position.File == to.File && p.Position.Rank == to.Rank);
                    if (to.Rank == 7 || to.Rank == 0)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            moves.Add((new Move(from, to, piece, captured, PieceType.Queen - i), MoveHistory.Count));
                        }
                    }
                    else
                    {
                        moves.Add((new Move(from, to, piece, captured), MoveHistory.Count));
                    }
                }
                else if (EnPassant != null && EnPassant.File == to.File && EnPassant.Rank == to.Rank)
                {
                    Piece captured = Pieces.FirstOrDefault(p => p.Position.File == to.File && p.Position.Rank == to.Rank - direction);
                    moves.Add((new Move(from, to, piece, captured, PieceType.Pawn, true), MoveHistory.Count));
                }
            }
            return moves;
        }

        public List<(Move, int)> GenerateKnightMoves(Piece piece)
        {
            List<(Move, int)> moves = new List<(Move, int)>();
            Square from = piece.Position;
            int[] fileOffsets = { -2, -1, 1, 2 };
            int[] rankOffsets = { -2, -1, 1, 2 };
            foreach (int fileOffset in fileOffsets)
            {
                foreach (int rankOffset in rankOffsets)
                {
                    if (Math.Abs(fileOffset) != Math.Abs(rankOffset))
                    {
                        Square to = new Square(from.File + fileOffset, from.Rank + rankOffset);
                        if (to.File >= 0 && to.File < 8 && to.Rank >= 0 && to.Rank < 8)
                        {
                            if (Pieces.All(p => p.Position.File != to.File || p.Position.Rank != to.Rank || p.IsWhite != piece.IsWhite))
                            {
                                Piece captured = Pieces.FirstOrDefault(p => p.Position.File == to.File && p.Position.Rank == to.Rank);
                                moves.Add((new Move(from, to, piece, captured), MoveHistory.Count));
                            }
                        }
                    }
                }
            }
            return moves;
        }

        public List<(Move, int)> GenerateBishopMoves(Piece piece)
        {
            List<(Move, int)> moves = new List<(Move, int)>();
            Square from = piece.Position;
            int[] fileOffsets = { -1, 1 };
            int[] rankOffsets = { -1, 1 };
            foreach (int fileOffset in fileOffsets)
            {
                foreach (int rankOffset in rankOffsets)
                {
                    for (int i = 1; i < 8; i++)
                    {
                        Square to = new Square(from.File + fileOffset * i, from.Rank + rankOffset * i);
                        if (to.File >= 0 && to.File < 8 && to.Rank >= 0 && to.Rank < 8)
                        {
                            if (Pieces.All(p => p.Position.File != to.File || p.Position.Rank != to.Rank || p.IsWhite != piece.IsWhite))
                            {
                                Piece captured = Pieces.FirstOrDefault(p => p.Position.File == to.File && p.Position.Rank == to.Rank);
                                moves.Add((new Move(from, to, piece, captured), MoveHistory.Count));
                                if (captured != null) break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return moves;
        }

        public List<(Move, int)> GenerateRookMoves(Piece piece)
        {
            List<(Move, int)> moves = new List<(Move, int)>();
            Square from = piece.Position;
            int[] fileOffsets = { -1, 0, 1, 0 };
            int[] rankOffsets = { 0, -1, 0, 1 };
            for (int dir = 0; dir < 4; dir++)
            {
                for (int i = 1; i < 8; i++)
                {
                    Square to = new Square(from.File + fileOffsets[dir] * i, from.Rank + rankOffsets[dir] * i);
                    if (to.File >= 0 && to.File < 8 && to.Rank >= 0 && to.Rank < 8)
                    {
                        if (Pieces.All(p => p.Position.File != to.File || p.Position.Rank != to.Rank || p.IsWhite != piece.IsWhite))
                        {
                            Piece captured = Pieces.FirstOrDefault(p => p.Position.File == to.File && p.Position.Rank == to.Rank);
                            moves.Add((new Move(from, to, piece, captured), MoveHistory.Count));
                            if (captured != null) break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return moves;
        }

        public List<(Move, int)> GenerateQueenMoves(Piece piece)
        {
            List<(Move, int)> moves = new List<(Move, int)>();
            moves.AddRange(GenerateBishopMoves(piece));
            moves.AddRange(GenerateRookMoves(piece));
            return moves;
        }

        public List<(Move, int)> GenerateKingMoves(Piece piece, bool includeCastling = true)
        {
            List<(Move, int)> moves = new List<(Move, int)>();
            Square from = piece.Position;
            for (int fileOffset = -1; fileOffset <= 1; fileOffset++)
            {
                for (int rankOffset = -1; rankOffset <= 1; rankOffset++)
                {
                    if (fileOffset != 0 || rankOffset != 0)
                    {
                        Square to = new Square(from.File + fileOffset, from.Rank + rankOffset);
                        if (to.File >= 0 && to.File < 8 && to.Rank >= 0 && to.Rank < 8)
                        {
                            if (Pieces.All(p => p.Position.File != to.File || p.Position.Rank != to.Rank || p.IsWhite != piece.IsWhite))
                            {
                                Piece captured = Pieces.FirstOrDefault(p => p.Position.File == to.File && p.Position.Rank == to.Rank);
                                moves.Add((new Move(from, to, piece, captured), MoveHistory.Count));
                            }
                        }
                    }
                }
            }

            if (includeCastling && piece.Position.File == 4 && (piece.IsWhite ? piece.Position.Rank == 0 : piece.Position.Rank == 7) &&
                !Moves.Any(m => m.Item1.Moved.IsWhite == piece.IsWhite && m.Item1.Moved.Type == PieceType.King))
            {
                if (Pieces.All(p => p.Position.File != 5 || p.Position.Rank != piece.Position.Rank && !IsAttacked(new Square(5, piece.Position.Rank), piece.IsWhite)) &&
                   Pieces.All(p => p.Position.File != 6 || p.Position.Rank != piece.Position.Rank && !IsAttacked(new Square(6, piece.Position.Rank), piece.IsWhite)) &&
                   Pieces.Any(p => p.Position.File == 7 && p.Position.Rank == piece.Position.Rank && p.Type == PieceType.Rook && p.IsWhite == piece.IsWhite))
                {
                    moves.Add((new Move(from, new Square(6, from.Rank), piece, null, PieceType.Pawn), MoveHistory.Count));
                }
                if (Pieces.All(p => p.Position.File != 3 || p.Position.Rank != piece.Position.Rank && !IsAttacked(new Square(3, piece.Position.Rank), piece.IsWhite)) &&
                   Pieces.All(p => p.Position.File != 2 || p.Position.Rank != piece.Position.Rank && !IsAttacked(new Square(2, piece.Position.Rank), piece.IsWhite)) &&
                   Pieces.All(p => p.Position.File != 1 || p.Position.Rank != piece.Position.Rank && !IsAttacked(new Square(1, piece.Position.Rank), piece.IsWhite)) &&
                   Pieces.Any(p => p.Position.File == 0 && p.Position.Rank == piece.Position.Rank && p.Type == PieceType.Rook && p.IsWhite == piece.IsWhite))
                {
                    moves.Add((new Move(from, new Square(2, from.Rank), piece, null, PieceType.Pawn), MoveHistory.Count));
                }
            }
            return moves;
        }

        public bool IsAttacked(Square square, bool byWhite)
        {
            return Pieces.Any(p => p.IsWhite != byWhite && GenerateMoves(p, false).Any(m => m.Item1.To.File == square.File && m.Item1.To.Rank == square.Rank));
        }

        public void MakeMove(Move move, bool generation = false)
        {
            if(move == null) return;
            Piece piece = Pieces.FirstOrDefault(p => p.Position.File == move.From.File && p.Position.Rank == move.From.Rank);
            if (piece == null) return;
            piece.Position = move.To;
            if (move.Captured != null)
            {
                Pieces.Remove(move.Captured);
            }
            if (move.Moved.Type == PieceType.Pawn && Math.Abs(move.To.Rank - move.From.Rank) == 2 && !generation)
            {
                EnPassant = new Square(move.To.File, (move.To.Rank + move.From.Rank) / 2);
            }
            else if(!generation)
            {
                EnPassant = null;
            }
            if(move.Moved.Type == PieceType.King && Math.Abs(move.To.File - move.From.File) == 2)
            {
                if(move.To.File == 6)
                {
                    Piece rook = Pieces.First(p => p.Position.File == 7 && p.Position.Rank == move.From.Rank);
                    rook.Position = new Square(5, move.From.Rank);
                }
                else
                {
                    Piece rook = Pieces.First(p => p.Position.File == 0 && p.Position.Rank == move.From.Rank);
                    rook.Position = new Square(3, move.From.Rank);
                }
            }
            if(move.Promotion != PieceType.Pawn)
            {
                piece.Type = move.Promotion;
            }

            MoveHistory.Push(move);
            IsWhiteToMove = !IsWhiteToMove;
        }

        public void UndoMove()
        {
            if (MoveHistory.Count > 0)
            {
                Move move = MoveHistory.Pop();
                Piece piece = Pieces.FirstOrDefault(p => p.Position.File == move.To.File && p.Position.Rank == move.To.Rank);
                if (piece == null) return;
                piece.Position = move.From;
                if (move.Captured != null)
                {
                    Pieces.Add(move.Captured);
                }
                if (move.Moved.Type == PieceType.King && Math.Abs(move.To.File - move.From.File) == 2)
                {
                    if (move.To.File == 6)
                    {
                        Piece rook = Pieces.First(p => p.Position.File == 5 && p.Position.Rank == move.From.Rank);
                        rook.Position = new Square(7, move.From.Rank);
                    }
                    else
                    {
                        Piece rook = Pieces.First(p => p.Position.File == 3 && p.Position.Rank == move.From.Rank);
                        rook.Position = new Square(0, move.From.Rank);
                    }
                }
                if (move.Promotion != PieceType.Pawn)
                {
                    piece.Type = PieceType.Pawn;
                }

                IsWhiteToMove = !IsWhiteToMove;
            }
        }

        public bool GameOver()
        {
            return Moves.Count == 0 || Pieces.Count <= 2;
        }
    }
}
