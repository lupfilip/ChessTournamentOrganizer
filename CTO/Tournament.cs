using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CTO
{
    public partial class Tournament : Form
    {
        int roundsTotal { get; set; }
        int roundCurrent { get; set; } = 0;

        public List<List<Match>> Matches { get; set; } = new List<List<Match>>();

        public List<(Match, int)> Archive { get; set; } = new List<(Match, int)>();

        public Form1 Reference { get; set; }

        public int DisplayMatchIndex { get; set; } = 0;

        public Tournament(Form1 reference, int roundCurrent = 0, List<List<Match>> matches = null)
        {
            InitializeComponent();

            pnlMatch.MouseWheel += PnlMatch_MouseWheel;

            Reference = reference;

            if (Reference.Players.Count % 2 == 1)
            {
                Reference.Players.Add(new Player("BYE", 0));
            }

            roundsTotal = reference.IsRoundRobin ? reference.Rounds * (reference.Players.Count - 1) : reference.Rounds;
            this.roundCurrent = roundCurrent + 1;
            if (matches != null) this.Matches = matches;
        }
        private void PnlMatch_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (Matches.Last().Count == 0)
            {
                return;
            }

            int previous = DisplayMatchIndex;
            if (e.Delta < 0)
            {
                DisplayMatchIndex++;
                if (DisplayMatchIndex >= Matches.Last().Count) DisplayMatchIndex = 0;
                ClickSquares(previous);
                Matches.Last()[DisplayMatchIndex].RefreshBoard(pnlMatch);
                Matches.Last()[DisplayMatchIndex].ViewBoard(pnlMatch);
            }
            else
            {
                DisplayMatchIndex--;
                if(DisplayMatchIndex < 0) DisplayMatchIndex = Matches.Last().Count - 1;
                ClickSquares(previous);
                Matches.Last()[DisplayMatchIndex].RefreshBoard(pnlMatch);
                Matches.Last()[DisplayMatchIndex].ViewBoard(pnlMatch);
            }
        }

        private void ClickSquares(int previous)
        {
            Match previousMatch = Matches.Last()[previous];
            Match currentMatch = Matches.Last()[DisplayMatchIndex];

            Panel board =
                pnlMatch.Controls
                .Find("pnlChessboard", true)[0] as Panel;

            foreach (Control control in board.Controls)
            {
                if (control is Panel square)
                {
                    square.Click -= new EventHandler(previousMatch.SelectSquare);
                    square.Click += new EventHandler(currentMatch.SelectSquare);
                }
            }
        }

        private void Tournament_Load(object sender, EventArgs e)
        {
            Text = $"Round: {roundCurrent} / {roundsTotal}";

            Matches.Add(new List<Match>());

            if (!Reference.IsRoundRobin)
            {
                Reference.Players = Reference.Players.OrderByDescending(x => x.Points).ThenByDescending(x => x.Rating).ThenByDescending(x => Reference.rnd.Next()).ToList();
            }

            do
            {
                Matches.Last().Clear();

                int k = (roundCurrent - 1) / (Reference.Players.Count - 1);
                for (int i = 0; i < Reference.Players.Count; i++)
                {
                    if (Matches.Last().Any(x => x.White.ToString(false) == Reference.Players[i].ToString(false) || x.Black.ToString(false) == Reference.Players[i].ToString(false)))
                    {
                        continue;
                    }

                    for (int j = 0; j < Reference.Players.Count; j++)
                    {
                        if (j == i) continue;

                        if (Matches.Count(x => x.Any(y => y.White.ToString(false) == Reference.Players[i].ToString(false) && y.Black.ToString(false) == Reference.Players[j].ToString(false))) +
                            Matches.Count(x => x.Any(y => y.White.ToString(false) == Reference.Players[j].ToString(false) && y.Black.ToString(false) == Reference.Players[i].ToString(false))) > k)
                        {
                            continue;
                        }

                        if (Matches.Last().Any(x => x.White.ToString(false) == Reference.Players[j].ToString(false) || x.Black.ToString(false) == Reference.Players[j].ToString(false)))
                        {
                            continue;
                        }

                        if (Matches.Count(x => x.Any(y => y.White.ToString(false) == Reference.Players[i].ToString(false) && y.Black.ToString(false) == Reference.Players[j].ToString(false))) >
                            Matches.Count(x => x.Any(y => y.White.ToString(false) == Reference.Players[j].ToString(false) && y.Black.ToString(false) == Reference.Players[i].ToString(false))))
                        {
                            Matches.Last().Add(new Match(Reference.Players[j], Reference.Players[i]));
                        }
                        else if (Matches.Count(x => x.Any(y => y.White.ToString(false) == Reference.Players[j].ToString(false) && y.Black.ToString(false) == Reference.Players[i].ToString(false))) >
                            Matches.Count(x => x.Any(y => y.White.ToString(false) == Reference.Players[i].ToString(false) && y.Black.ToString(false) == Reference.Players[j].ToString(false))))
                        {
                            Matches.Last().Add(new Match(Reference.Players[i], Reference.Players[j]));
                        }
                        else
                        {
                            int rand = Reference.rnd.Next(0, 2);
                            Matches.Last().Add(new Match(Reference.Players[rand == 0 ? i : j], Reference.Players[rand == 0 ? j : i]));
                        }

                        break;
                    }
                }


                Reference.Players = Reference.Players.OrderByDescending(x => Reference.rnd.Next()).ToList();

            } while (Matches.Last().Count < Reference.Players.Count / 2);

            Matches[Matches.Count - 1] = Matches.Last().OrderByDescending(m => m.White.Points + m.Black.Points).ToList();

            /*Task.Run(() =>
            {
                while ((Matches.Last()[DisplayMatchIndex].White.Name == "BYE" && Matches.Last()[DisplayMatchIndex].Chessboard.IsWhiteToMove) || (Matches.Last()[DisplayMatchIndex].Black.Name == "BYE" && !Matches.Last()[DisplayMatchIndex].Chessboard.IsWhiteToMove) && !Matches.Last()[DisplayMatchIndex].Chessboard.GameOver())
                {
                    Matches.Last()[DisplayMatchIndex].Chessboard.MakeMove(Engine.FindBestMove(Matches.Last()[DisplayMatchIndex].Chessboard, 3));
                    Matches.Last()[DisplayMatchIndex].Chessboard.GenerateLegalMoves();

                    Invoke(() =>
                    {
                        Matches.Last()[DisplayMatchIndex].RefreshBoard(pnlMatch);
                        Matches.Last()[DisplayMatchIndex].ViewBoard(pnlMatch);
                    });
                }
            });*/

            Matches.Last()[DisplayMatchIndex].Chessboard.GenerateLegalMoves();

            Matches.Last()[DisplayMatchIndex].ViewBoard(pnlMatch);
            ClickSquares(0);
        }

        private void btnNextRound_Click(object sender, EventArgs e)
        {
            if (roundCurrent >= roundsTotal)
            {
                using (SqlConnection connection = new SqlConnection(Form1.connectionString))
                {
                    connection.Open();

                    foreach (Player p in Reference.Players)
                    {
                        string query = $"UPDATE Player SET Rating = {p.Rating} WHERE Name = '{p.Name}'";

                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                Reference.Players.RemoveAll(x => x.Name == "BYE");

                Reference.UpdateList(true);

                Reference.Show();
                Close();
                return;
            }

            Matches.Last().AddRange(Archive.Select(x => x.Item1));

            Tournament tournament = new Tournament(Reference, roundCurrent, Matches);
            tournament.Show();
            this.Close();
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            Matches.Last()[DisplayMatchIndex].White.Points += 2;

            Matches.Last()[DisplayMatchIndex].White.Rating = (int)(Matches.Last()[DisplayMatchIndex].White.Rating + 10 * (1 - (1 / (1 + Math.Pow(10,
                (Matches.Last()[DisplayMatchIndex].Black.Rating -
                Matches.Last()[DisplayMatchIndex].White.Rating) / 400)))));

            Matches.Last()[DisplayMatchIndex].Black.Rating = (int)(Matches.Last()[DisplayMatchIndex].Black.Rating + 10 * (0 - (1 / (1 + Math.Pow(10,
                (Matches.Last()[DisplayMatchIndex].White.Rating -
                Matches.Last()[DisplayMatchIndex].Black.Rating) / 400)))));

            Archive.Add((Matches.Last()[DisplayMatchIndex], 2));
            Matches.Last().RemoveAt(DisplayMatchIndex);
            UpdateScores();
        }

        private void btn01_Click(object sender, EventArgs e)
        {
            Matches.Last()[DisplayMatchIndex].Black.Points += 2;

            Matches.Last()[DisplayMatchIndex].White.Rating = (int)(Matches.Last()[DisplayMatchIndex].White.Rating + 5 * (0 - (1 / (1 + Math.Pow(10,
                (Matches.Last()[DisplayMatchIndex].Black.Rating -
                Matches.Last()[DisplayMatchIndex].White.Rating) / 400)))));

            Matches.Last()[DisplayMatchIndex].Black.Rating = (int)(Matches.Last()[DisplayMatchIndex].Black.Rating + 10 * (1 - (1 / (1 + Math.Pow(10,
                (Matches.Last()[DisplayMatchIndex].White.Rating -
                Matches.Last()[DisplayMatchIndex].Black.Rating) / 400)))));

            Archive.Add((Matches.Last()[DisplayMatchIndex], 0));
            Matches.Last().RemoveAt(DisplayMatchIndex);
            UpdateScores();
        }

        private void btn1212_Click(object sender, EventArgs e)
        {
            Matches.Last()[DisplayMatchIndex].White.Points += 1;
            Matches.Last()[DisplayMatchIndex].Black.Points += 1;

            Matches.Last()[DisplayMatchIndex].White.Rating = (int)(Matches.Last()[DisplayMatchIndex].White.Rating + 5 * (1 - (1 / (1 + Math.Pow(10,
                (Matches.Last()[DisplayMatchIndex].Black.Rating -
                Matches.Last()[DisplayMatchIndex].White.Rating) / 400)))));

            Matches.Last()[DisplayMatchIndex].Black.Rating = (int)(Matches.Last()[DisplayMatchIndex].Black.Rating + 5 * (1 - (1 / (1 + Math.Pow(10,
                (Matches.Last()[DisplayMatchIndex].White.Rating -
                Matches.Last()[DisplayMatchIndex].Black.Rating) / 400)))));

            Archive.Add((Matches.Last()[DisplayMatchIndex], 1));
            Matches.Last().RemoveAt(DisplayMatchIndex);
            UpdateScores();
        }

        public void UpdateScores()
        {
            if (Matches.Last().Count == 0)
            {
                pnlMatch.Controls.Clear();
                return;
            }

            DisplayMatchIndex = 0;
            Matches.Last()[DisplayMatchIndex].RefreshBoard(pnlMatch);
            Matches.Last()[DisplayMatchIndex].ViewBoard(pnlMatch);
        }

        private void Tournament_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Form1.connectionString))
            {
                connection.Open();

                foreach (Player p in Reference.Players)
                {
                    string query = $"UPDATE Player SET Rating = {p.Rating} WHERE Name = '{p.Name}'";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void pnlMatch_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            PnlMatch_MouseWheel(sender, new MouseEventArgs(MouseButtons.None, 0, 0, 0, 120));
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            PnlMatch_MouseWheel(sender, new MouseEventArgs(MouseButtons.None, 0, 0, 0, -120));
        }
    }
}
