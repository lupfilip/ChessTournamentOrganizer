namespace CTO
{
    partial class Tournament
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tournament));
            btnNextRound = new Button();
            btn01 = new Button();
            btn1212 = new Button();
            btn10 = new Button();
            pnlMatch = new Panel();
            btnRight = new Button();
            btnLeft = new Button();
            SuspendLayout();
            // 
            // btnNextRound
            // 
            btnNextRound.Location = new Point(12, 295);
            btnNextRound.Name = "btnNextRound";
            btnNextRound.Size = new Size(79, 60);
            btnNextRound.TabIndex = 3;
            btnNextRound.Text = "Next Round";
            btnNextRound.UseVisualStyleBackColor = true;
            btnNextRound.Click += btnNextRound_Click;
            // 
            // btn01
            // 
            btn01.Location = new Point(293, 295);
            btn01.Name = "btn01";
            btn01.Size = new Size(79, 60);
            btn01.TabIndex = 4;
            btn01.Text = "0-1";
            btn01.UseVisualStyleBackColor = true;
            btn01.Click += btn01_Click;
            // 
            // btn1212
            // 
            btn1212.Location = new Point(201, 295);
            btn1212.Name = "btn1212";
            btn1212.Size = new Size(86, 60);
            btn1212.TabIndex = 5;
            btn1212.Text = "1/2-1/2";
            btn1212.UseVisualStyleBackColor = true;
            btn1212.Click += btn1212_Click;
            // 
            // btn10
            // 
            btn10.Location = new Point(116, 295);
            btn10.Name = "btn10";
            btn10.Size = new Size(79, 60);
            btn10.TabIndex = 6;
            btn10.Text = "1-0";
            btn10.UseVisualStyleBackColor = true;
            btn10.Click += btn10_Click;
            // 
            // pnlMatch
            // 
            pnlMatch.Anchor = AnchorStyles.None;
            pnlMatch.Location = new Point(57, 12);
            pnlMatch.Name = "pnlMatch";
            pnlMatch.Size = new Size(277, 277);
            pnlMatch.TabIndex = 7;
            pnlMatch.Scroll += pnlMatch_Scroll;
            // 
            // btnRight
            // 
            btnRight.Location = new Point(340, 12);
            btnRight.Name = "btnRight";
            btnRight.Size = new Size(32, 277);
            btnRight.TabIndex = 9;
            btnRight.Text = ">";
            btnRight.UseVisualStyleBackColor = true;
            btnRight.Click += btnRight_Click;
            // 
            // btnLeft
            // 
            btnLeft.Location = new Point(19, 12);
            btnLeft.Name = "btnLeft";
            btnLeft.Size = new Size(32, 277);
            btnLeft.TabIndex = 10;
            btnLeft.Text = "<";
            btnLeft.UseVisualStyleBackColor = true;
            btnLeft.Click += btnLeft_Click;
            // 
            // Tournament
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 361);
            ControlBox = false;
            Controls.Add(btnLeft);
            Controls.Add(btnRight);
            Controls.Add(pnlMatch);
            Controls.Add(btn10);
            Controls.Add(btn1212);
            Controls.Add(btn01);
            Controls.Add(btnNextRound);
            Font = new Font("Segoe UI", 14F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5);
            Name = "Tournament";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Round: #";
            FormClosing += Tournament_FormClosing;
            Load += Tournament_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnNextRound;
        private Button btn01;
        private Button btn1212;
        private Button btn10;
        private Panel pnlMatch;
        private Button btnRight;
        private Button btnLeft;
    }
}