namespace CTO
{
    partial class ExistingPlayers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExistingPlayers));
            lsbPlayers = new ListBox();
            btnAdd = new Button();
            SuspendLayout();
            // 
            // lsbPlayers
            // 
            lsbPlayers.Anchor = AnchorStyles.None;
            lsbPlayers.BorderStyle = BorderStyle.FixedSingle;
            lsbPlayers.FormattingEnabled = true;
            lsbPlayers.HorizontalScrollbar = true;
            lsbPlayers.ItemHeight = 25;
            lsbPlayers.Location = new Point(12, 12);
            lsbPlayers.Name = "lsbPlayers";
            lsbPlayers.SelectionMode = SelectionMode.MultiSimple;
            lsbPlayers.Size = new Size(360, 277);
            lsbPlayers.TabIndex = 0;
            lsbPlayers.KeyDown += lsbPlayers_KeyDown;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(12, 295);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(360, 54);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // ExistingPlayers
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 361);
            Controls.Add(btnAdd);
            Controls.Add(lsbPlayers);
            Font = new Font("Segoe UI", 14F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5);
            Name = "ExistingPlayers";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Player List";
            FormClosing += ExistingPlayers_FormClosing;
            Load += ExistingPlayers_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListBox lsbPlayers;
        private Button btnAdd;
    }
}