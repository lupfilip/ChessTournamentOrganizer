using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CTO
{
    public static class Engine
    {
        // =========================
        // EVALUATION
        // =========================
        public static int Evaluate(Board board)
        {
            int score = 0;

            foreach (var piece in board.Pieces)
            {
                int value = piece.Type switch
                {
                    PieceType.Pawn => 100,
                    PieceType.Knight => 320,
                    PieceType.Bishop => 330,
                    PieceType.Rook => 500,
                    PieceType.Queen => 900,
                    PieceType.King => 20000,
                    _ => 0
                };

                int mobility = board.GenerateMoves(piece).Count;

                if (piece.IsWhite == board.IsWhiteToMove)
                    score += value + mobility;
                else
                    score -= value + mobility;
            }

            // Merge DB knowledge
            score += FindResultInDatabase(board);

            return score;
        }

        // =========================
        // BEST MOVE
        // =========================
        public static Move FindBestMove(Board board, int depth)
        {
            board.GenerateLegalMoves();

            Stopwatch stopwatch = Stopwatch.StartNew();

            var moves = board.Moves.ToList();
            Move bestMove = null;
            int bestScore = int.MinValue;

            foreach (var (move, order) in moves)
            {
                board.MakeMove(move, true);

                int score = Alphabeta(
                    board,
                    depth - 1,
                    int.MinValue,
                    int.MaxValue,
                    false,
                    stopwatch,
                    1000
                );

                board.UndoMove();

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }

            return bestMove;
        }

        // =========================
        // ALPHA-BETA
        // =========================
        public static int Alphabeta(
            Board board,
            int depth,
            int alpha,
            int beta,
            bool maximizingPlayer,
            Stopwatch stopwatch,
            long milliseconds)
        {
            board.GenerateLegalMoves();
            var moves = board.Moves.ToList();

            // Terminal
            if (moves.Count == 0)
            {
                int terminal = maximizingPlayer ? int.MinValue + 1 : int.MaxValue - 1;
                InsertMatchIntoDatabase(board, terminal);
                return terminal;
            }

            // Leaf
            if (depth == 0 || stopwatch.ElapsedMilliseconds >= milliseconds)
            {
                int eval = Evaluate(board);
                InsertMatchIntoDatabase(board, eval);
                return eval;
            }

            if (maximizingPlayer)
            {
                int bestScore = int.MinValue;

                foreach (var (move, _) in moves)
                {
                    board.MakeMove(move, true);

                    int score = Alphabeta(
                        board,
                        depth - 1,
                        alpha,
                        beta,
                        false,
                        stopwatch,
                        milliseconds
                    );

                    board.UndoMove();

                    bestScore = Math.Max(bestScore, score);
                    alpha = Math.Max(alpha, score);

                    if (beta <= alpha)
                        break;
                }

                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;

                foreach (var (move, _) in moves)
                {
                    board.MakeMove(move, true);

                    int score = Alphabeta(
                        board,
                        depth - 1,
                        alpha,
                        beta,
                        true,
                        stopwatch,
                        milliseconds
                    );

                    board.UndoMove();

                    bestScore = Math.Min(bestScore, score);
                    beta = Math.Min(beta, score);

                    if (beta <= alpha)
                        break;
                }

                return bestScore;
            }
        }

        // =========================
        // DATABASE HELPERS
        // =========================

        private static string SerializeMoves(Board board)
        {
            return string.Join(",", board.MoveHistory);
        }

        public static void InsertMatchIntoDatabase(Board board, int result)
        {
            /*string moves = SerializeMoves(board);

            using SqlConnection connection = new SqlConnection(Form1.connectionString);
            connection.Open();

            string query = @"
                DELETE FROM Match
                WHERE @Moves LIKE Moves + '%'
                AND LEN(Moves) < LEN(@Moves);

                IF EXISTS(
                    SELECT 1 FROM Match
                    WHERE Moves LIKE @Moves + ',%'
                )
                    RETURN;

                INSERT INTO Match(Moves, Result)
                VALUES(@Moves, @Result);
            ";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Moves", moves);
            command.Parameters.AddWithValue("@Result", result);
            command.ExecuteNonQuery();*/
        }

        public static int FindResultInDatabase(Board board)
        {
            /*string moves = SerializeMoves(board);

            using SqlConnection connection = new SqlConnection(Form1.connectionString);
            connection.Open();

            string query = @"
                SELECT TOP 1 Result
                FROM Match
                WHERE @Moves LIKE Moves + '%'
                ORDER BY LEN(Moves) DESC
            ";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Moves", moves);

            object result = command.ExecuteScalar();

            return (result != null && int.TryParse(result.ToString(), out int val))
                ? val
                :*/ return 0;
        }

        public static void UpdateMatchResultInDatabase(Board board, int result)
        {
            /*string moves = SerializeMoves(board);

            using SqlConnection connection = new SqlConnection(Form1.connectionString);
            connection.Open();

            string query = @"
                UPDATE Match
                SET Result = Result + @Result
                WHERE @Moves LIKE Moves + '%'
            ";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Moves", moves);
            command.Parameters.AddWithValue("@Result", result);
            command.ExecuteNonQuery();*/
        }
    }
}