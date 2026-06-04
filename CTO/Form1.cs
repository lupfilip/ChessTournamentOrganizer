using System.Data.SQLite;

namespace CTO
{
    public partial class Form1 : Form
    {
        public static string connectionString =
        $@"Data Source={Application.StartupPath}\CTO.db";

        public bool IsRoundRobin { get; set; } = true;
        public int Rounds { get; set; } = 1;

        public List<Player> Players { get; set; } = new List<Player>();

        public Random rnd = new Random();

        public Form1()
        {
            

            InitializeComponent();

            CreateDatabase();
        }

        private void CreateDatabase()
        {
            using var connection = new SQLiteConnection(connectionString);

            connection.Open();

            string query =
            @"CREATE TABLE IF NOT EXISTS Player(
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Name TEXT NOT NULL UNIQUE,
        Rating INTEGER NOT NULL
    );";

            using var command = new SQLiteCommand(query, connection);

            command.ExecuteNonQuery();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string queryFind = $"SELECT * FROM Player WHERE Name = '{txtName.Text}'";

                using (SQLiteCommand commandFind = new SQLiteCommand(queryFind, connection))
                {
                    using (SQLiteDataReader reader = commandFind.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            MessageBox.Show("Player already exists!", "Duplicate player", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                string query = "INSERT INTO Player (Name, Rating) VALUES (@Name, @Rating)";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", txtName.Text);
                    command.Parameters.AddWithValue("@Rating", int.TryParse(txtRating.Text, out int rating2) ? rating2 : 0);
                    command.ExecuteNonQuery();
                }
            }

            Players.Add(new Player(
                txtName.Text,
                int.TryParse(txtRating.Text, out int rating) ? rating : 0)
            );


            lsbPlayers.Items.Add(Players.Last().ToString(false));
        }

        private void Form1_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lsbPlayers.SelectedItems != null)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete " + lsbPlayers.SelectedItem.ToString() + "?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    Players.RemoveAt(lsbPlayers.SelectedIndex);
                    lsbPlayers.Items.RemoveAt(lsbPlayers.SelectedIndex);
                }
            }
        }

        private void txtRating_TextChanged(object sender, EventArgs e)
        {
            if (txtRating.Text.Any(c => !char.IsDigit(c)) || txtRating.Text == string.Empty)
            {
                MessageBox.Show("Please enter a valid number for the rating.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRating.Text = "1000";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnAddExisting_Click(object sender, EventArgs e)
        {
            ExistingPlayers existingPlayersForm = new ExistingPlayers(this);
            existingPlayersForm.Show();
            Hide();
        }

        public void UpdateList(bool points)
        {
            lsbPlayers.Items.Clear();
            Players = Players.OrderByDescending(x => x.Points).ThenByDescending(x => x.Rating).ThenByDescending(x => rnd.Next()).ToList();
            foreach (var player in Players)
            {
                lsbPlayers.Items.Add(player.ToString(points));
                player.Points = 0;
            }
        }

        private void txtRounds_TextChanged(object sender, EventArgs e)
        {
            if (txtRounds.Text == string.Empty)
            {
                txtRounds.Text = "1";
                Rounds = 1;
            }
            else
            {
                if (txtRounds.Text.Any(c => !char.IsDigit(c)))
                {
                    MessageBox.Show("Please enter a valid number for the rounds.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRounds.Text = "1";
                    Rounds = 1;
                }
                else
                {
                    Rounds = int.Parse(txtRounds.Text);
                }
            }
        }

        private void lsbSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsbSystem.SelectedItem.ToString() == "Round Robin")
            {
                IsRoundRobin = true;
            }
            else
            {
                IsRoundRobin = false;
            }
        }

        private void btnStartTournament_Click(object sender, EventArgs e)
        {
            Tournament tournament = new Tournament(this);
            tournament.Show();
            Hide();
        }
    }
}
