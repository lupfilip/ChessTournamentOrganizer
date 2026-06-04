using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTO
{
    public class Player
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public int Points { get; set; }

        public Player(string name, int rating)
        {
            Name = name;
            Rating = rating;
            Points = 0;
        }

        public string ToString(bool includePoints)
        {
            if (includePoints)
            {
                return "|" + Points + "| " + Name + ": " + Rating;
            }
            else
            {
                return Name + ": " + Rating;
            }
        }

        public Player(string data)
        {
            string[] parts = data.Split(' ');
            if (parts[0].Contains('|'))
            {
                Name = string.Join(" ", parts.Skip(1).Take(parts.Length - 3)).Trim().Trim(':');
                Rating = int.Parse(parts[parts.Length - 1].Trim());
                Points = int.Parse(parts[0].Trim().Trim('|'));
            }
            else
            {
                Name = string.Join(" ", parts.Take(parts.Length - 1)).Trim().Trim(':');
                Rating = int.Parse(parts[parts.Length - 1].Trim());
            }
        }
    }
}
