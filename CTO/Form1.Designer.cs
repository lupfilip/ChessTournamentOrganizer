namespace CTO
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            lblPlayers = new Label();
            lsbPlayers = new ListBox();
            lblName = new Label();
            txtName = new TextBox();
            txtRating = new TextBox();
            lblRating = new Label();
            btnAdd = new Button();
            btnAddExisting = new Button();
            lsbSystem = new ListBox();
            txtRounds = new TextBox();
            lblRounds = new Label();
            btnStartTournament = new Button();
            SuspendLayout();
            // 
            // lblPlayers
            // 
            lblPlayers.Anchor = AnchorStyles.None;
            lblPlayers.AutoSize = true;
            lblPlayers.Location = new Point(42, 9);
            lblPlayers.Name = "lblPlayers";
            lblPlayers.Size = new Size(76, 25);
            lblPlayers.TabIndex = 0;
            lblPlayers.Text = "Players:";
            // 
            // lsbPlayers
            // 
            lsbPlayers.Anchor = AnchorStyles.None;
            lsbPlayers.BorderStyle = BorderStyle.FixedSingle;
            lsbPlayers.FormattingEnabled = true;
            lsbPlayers.HorizontalScrollbar = true;
            lsbPlayers.ItemHeight = 25;
            lsbPlayers.Location = new Point(42, 37);
            lsbPlayers.Name = "lsbPlayers";
            lsbPlayers.Size = new Size(480, 277);
            lsbPlayers.TabIndex = 1;
            lsbPlayers.KeyDown += Form1_KeyPress;
            // 
            // lblName
            // 
            lblName.Anchor = AnchorStyles.None;
            lblName.AutoSize = true;
            lblName.Location = new Point(42, 317);
            lblName.Name = "lblName";
            lblName.Size = new Size(66, 25);
            lblName.TabIndex = 2;
            lblName.Text = "Name:";
            // 
            // txtName
            // 
            txtName.Anchor = AnchorStyles.None;
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Location = new Point(42, 345);
            txtName.Name = "txtName";
            txtName.Size = new Size(316, 32);
            txtName.TabIndex = 3;
            txtName.TextAlign = HorizontalAlignment.Center;
            // 
            // txtRating
            // 
            txtRating.Anchor = AnchorStyles.None;
            txtRating.BorderStyle = BorderStyle.FixedSingle;
            txtRating.Location = new Point(114, 383);
            txtRating.Name = "txtRating";
            txtRating.Size = new Size(84, 32);
            txtRating.TabIndex = 5;
            txtRating.Text = "1000";
            txtRating.TextAlign = HorizontalAlignment.Center;
            txtRating.TextChanged += txtRating_TextChanged;
            // 
            // lblRating
            // 
            lblRating.Anchor = AnchorStyles.None;
            lblRating.AutoSize = true;
            lblRating.Location = new Point(42, 385);
            lblRating.Name = "lblRating";
            lblRating.Size = new Size(70, 25);
            lblRating.TabIndex = 4;
            lblRating.Text = "Rating:";
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.None;
            btnAdd.Location = new Point(42, 421);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(66, 38);
            btnAdd.TabIndex = 8;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnAddExisting
            // 
            btnAddExisting.Anchor = AnchorStyles.None;
            btnAddExisting.Location = new Point(114, 421);
            btnAddExisting.Name = "btnAddExisting";
            btnAddExisting.Size = new Size(127, 38);
            btnAddExisting.TabIndex = 9;
            btnAddExisting.Text = "Add Existing";
            btnAddExisting.UseVisualStyleBackColor = true;
            btnAddExisting.Click += btnAddExisting_Click;
            // 
            // lsbSystem
            // 
            lsbSystem.Anchor = AnchorStyles.None;
            lsbSystem.BorderStyle = BorderStyle.FixedSingle;
            lsbSystem.FormattingEnabled = true;
            lsbSystem.ItemHeight = 25;
            lsbSystem.Items.AddRange(new object[] { "Round Robin", "Swiss" });
            lsbSystem.Location = new Point(364, 345);
            lsbSystem.Name = "lsbSystem";
            lsbSystem.Size = new Size(158, 52);
            lsbSystem.TabIndex = 11;
            lsbSystem.SelectedIndexChanged += lsbSystem_SelectedIndexChanged;
            // 
            // txtRounds
            // 
            txtRounds.Anchor = AnchorStyles.None;
            txtRounds.BorderStyle = BorderStyle.FixedSingle;
            txtRounds.Location = new Point(448, 403);
            txtRounds.Name = "txtRounds";
            txtRounds.Size = new Size(74, 32);
            txtRounds.TabIndex = 13;
            txtRounds.Text = "1";
            txtRounds.TextAlign = HorizontalAlignment.Center;
            txtRounds.TextChanged += txtRounds_TextChanged;
            // 
            // lblRounds
            // 
            lblRounds.Anchor = AnchorStyles.None;
            lblRounds.AutoSize = true;
            lblRounds.Location = new Point(364, 405);
            lblRounds.Name = "lblRounds";
            lblRounds.Size = new Size(78, 25);
            lblRounds.TabIndex = 12;
            lblRounds.Text = "Rounds:";
            // 
            // btnStartTournament
            // 
            btnStartTournament.Anchor = AnchorStyles.None;
            btnStartTournament.Location = new Point(42, 465);
            btnStartTournament.Name = "btnStartTournament";
            btnStartTournament.Size = new Size(480, 39);
            btnStartTournament.TabIndex = 14;
            btnStartTournament.Text = "Start Tournament";
            btnStartTournament.UseVisualStyleBackColor = true;
            btnStartTournament.Click += btnStartTournament_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 537);
            Controls.Add(btnStartTournament);
            Controls.Add(txtRounds);
            Controls.Add(lblRounds);
            Controls.Add(lsbSystem);
            Controls.Add(btnAddExisting);
            Controls.Add(btnAdd);
            Controls.Add(txtRating);
            Controls.Add(lblRating);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Controls.Add(lsbPlayers);
            Controls.Add(lblPlayers);
            Font = new Font("Segoe UI", 14F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Chess Tournament Ogranizer";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPlayers;
        private ListBox lsbPlayers;
        private Label lblName;
        private TextBox txtName;
        private TextBox txtRating;
        private Label lblRating;
        private Button btnAdd;
        private Button btnAddExisting;
        private ListBox lsbSystem;
        private TextBox txtRounds;
        private Label lblRounds;
        private Button btnStartTournament;
    }
}
