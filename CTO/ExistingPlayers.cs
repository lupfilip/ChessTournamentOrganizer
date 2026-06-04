using System.Data.SQLite;

namespace CTO
{
    public partial class ExistingPlayers : Form
    {
        Form1 Reference { get; set; }

        public ExistingPlayers(Form1 reference)
        {
            InitializeComponent();
            Reference = reference;
        }

        private void ExistingPlayers_Load(object sender, EventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(Form1.connectionString))
            {
                connection.Open();
                List<Player> list = new List<Player>();
                string query = "SELECT Name, Rating FROM Player ORDER BY Rating DESC";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString(0);
                            int rating = reader.GetInt32(1);
                            list.Add(new Player(name, rating));
                            lsbPlayers.Items.Add(list.Last().ToString(false));
                        }
                    }
                }
            }

            foreach (var player in Reference.Players)
            {
                if (lsbPlayers.Items.Contains(player.ToString(false)))
                {
                    lsbPlayers.SelectedIndices.Add(lsbPlayers.Items.IndexOf(player.ToString(false)));
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Reference.Players.Clear();
            foreach (var item in lsbPlayers.SelectedItems)
            {
                Player player = Reference.Players.FirstOrDefault(p => p.ToString(false) == item.ToString());
                if (player == null)
                {
                    Reference.Players.Add(new Player(item.ToString()));
                }
            }

            Reference.UpdateList(false);
            Close();
        }

        private void ExistingPlayers_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to continue?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Reference.Show();
            }
        }

        private void lsbPlayers_KeyDown(object sender, KeyEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(Form1.connectionString))
            {
                connection.Open();

                if (e.KeyCode == Keys.Delete)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete the selected player(s)?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                    {
                        return;
                    }

                    foreach (var item in lsbPlayers.SelectedItems)
                    {
                        string playerName = "";

                        Player p = new Player(item.ToString());

                        playerName = p.Name;

                        string query = $"DELETE FROM Player WHERE Name = @name";
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@name", playerName);
                            command.ExecuteNonQuery();
                        }
                    }

                    for (int i = lsbPlayers.Items.Count - 1; i >= 0; i--)
                    {
                        if (lsbPlayers.GetSelected(i))
                        {
                            lsbPlayers.Items.RemoveAt(i);
                        }
                    }
                }
            }
        }
    }
}
