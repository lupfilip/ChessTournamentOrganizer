using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTO
{
    public class Square
    {
        public int File { get; set; }
        public int Rank { get; set; }

        public Square(int file, int rank)
        {
            File = file;
            Rank = rank;
        }

        public override string ToString()
        {
            char fileChar = (char)('a' + File);
            char rankChar = (char)('1' + Rank);
            return $"{fileChar}{rankChar}";
        }
    }
}
