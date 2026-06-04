using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTO
{
    public class Move
    {
        public Square From { get; set; }
        public Square To { get; set; }
        public Piece Moved { get; set; }
        public Piece Captured { get; set; }
        public PieceType Promotion { get; set; }
        public bool IsEnPassant { get; set; }
        bool IsCastling { get; set; }

        public Move(Square from, Square to, Piece moved, Piece captured = null, PieceType promotion = PieceType.Pawn, bool isEnPassant = false, bool isCastling = false)
        {
            From = from;
            To = to;
            Moved = moved;
            Captured = captured;
            Promotion = promotion;
            IsEnPassant = isEnPassant;
            IsCastling = isCastling;
        }

        public override string ToString()
        {
            string moveStr = $"{From}-{To}";
            if (Captured != null)
                moveStr = $"{From}x{To}";
            if (Promotion != PieceType.Queen)
                moveStr += $"={Promotion.ToString()[0]}";
            if (IsEnPassant)
                moveStr += " e. p.";
            if (IsCastling)
                moveStr = "0-0" + (From.File > To.File ? "-0" : "");
            return moveStr;
        }
    }
}
