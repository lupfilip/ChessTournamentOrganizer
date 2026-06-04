namespace CTO
{
    partial class Promotion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Promotion));
            pnlQueen = new Panel();
            pnlRook = new Panel();
            pnlBishop = new Panel();
            pnlKnight = new Panel();
            SuspendLayout();
            // 
            // pnlQueen
            // 
            pnlQueen.Anchor = AnchorStyles.None;
            pnlQueen.BackColor = SystemColors.ControlDark;
            pnlQueen.BackgroundImageLayout = ImageLayout.Stretch;
            pnlQueen.Location = new Point(27, 12);
            pnlQueen.Name = "pnlQueen";
            pnlQueen.Size = new Size(100, 100);
            pnlQueen.TabIndex = 0;
            // 
            // pnlRook
            // 
            pnlRook.Anchor = AnchorStyles.None;
            pnlRook.BackColor = SystemColors.ControlDark;
            pnlRook.BackgroundImageLayout = ImageLayout.Stretch;
            pnlRook.Location = new Point(133, 12);
            pnlRook.Name = "pnlRook";
            pnlRook.Size = new Size(100, 100);
            pnlRook.TabIndex = 1;
            // 
            // pnlBishop
            // 
            pnlBishop.Anchor = AnchorStyles.None;
            pnlBishop.BackColor = SystemColors.ControlDark;
            pnlBishop.BackgroundImageLayout = ImageLayout.Stretch;
            pnlBishop.Location = new Point(27, 118);
            pnlBishop.Name = "pnlBishop";
            pnlBishop.Size = new Size(100, 100);
            pnlBishop.TabIndex = 1;
            // 
            // pnlKnight
            // 
            pnlKnight.Anchor = AnchorStyles.None;
            pnlKnight.BackColor = SystemColors.ControlDark;
            pnlKnight.BackgroundImageLayout = ImageLayout.Stretch;
            pnlKnight.Location = new Point(133, 118);
            pnlKnight.Name = "pnlKnight";
            pnlKnight.Size = new Size(100, 100);
            pnlKnight.TabIndex = 1;
            // 
            // Promotion
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(260, 237);
            ControlBox = false;
            Controls.Add(pnlKnight);
            Controls.Add(pnlBishop);
            Controls.Add(pnlRook);
            Controls.Add(pnlQueen);
            Font = new Font("Segoe UI", 14F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5);
            Name = "Promotion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Promotion";
            Load += Promotion_Load;
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlQueen;
        private Panel pnlRook;
        private Panel pnlBishop;
        private Panel pnlKnight;
    }
}