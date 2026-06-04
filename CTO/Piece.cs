using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTO
{
    public enum PieceType
    {
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen,
        King
    }

    public class Piece
    {
        public PieceType Type { get; set; }
        public bool IsWhite { get; set; }
        public Square Position { get; set; }

        public Piece(PieceType type, bool isWhite, Square position)
        {
            Type = type;
            IsWhite = isWhite;
            Position = position;
        }

        public override string ToString()
        {
            string color = IsWhite ? "White" : "Black";
            return $"{char.ToLower(color[0])}{char.ToLower(ToFenChar())}";
        }

        public char ToFenChar()
        {
            char c = Type switch
            {
                PieceType.Pawn => 'p',
                PieceType.Knight => 'n',
                PieceType.Bishop => 'b',
                PieceType.Rook => 'r',
                PieceType.Queen => 'q',
                PieceType.King => 'k',
                _ => ' '
            };

            return IsWhite ? char.ToUpper(c) : c;
        }
    }
}
