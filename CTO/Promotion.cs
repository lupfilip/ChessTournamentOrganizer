using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CTO
{
    public partial class Promotion : Form
    {
        public Match Reference { get; set; }
        public Square To { get; set; }
        public Panel panel { get; set; }

        public Promotion(Match reference, Square square, Panel panel)
        {
            InitializeComponent();
            Reference = reference;
            this.panel = panel;
            To = square;
        }

        private void Promotion_Load(object sender, EventArgs e)
        {
            pnlQueen.BackgroundImage = Reference.Chessboard.IsWhiteToMove ? Properties.Resources.wq : Properties.Resources.bq;
            pnlRook.BackgroundImage = Reference.Chessboard.IsWhiteToMove ? Properties.Resources.wr : Properties.Resources.br;
            pnlBishop.BackgroundImage = Reference.Chessboard.IsWhiteToMove ? Properties.Resources.wb : Properties.Resources.bb;
            pnlKnight.BackgroundImage = Reference.Chessboard.IsWhiteToMove ? Properties.Resources.wn : Properties.Resources.bn;

            pnlQueen.Click += (s, ev) =>
            {
                Reference.Chessboard.MakeMove(Reference.Chessboard.Moves.Where(m => m.Item1.To.Rank == To.Rank && m.Item1.To.File == To.File && m.Item1.Promotion == PieceType.Queen).First().Item1);
                Reference.RefreshBoard(panel);
                Reference.ViewBoard(panel);
                Close();
            };
            pnlRook.Click += (s, ev) =>
            {
                Reference.Chessboard.MakeMove(Reference.Chessboard.Moves.Where(m => m.Item1.To.Rank == To.Rank && m.Item1.To.File == To.File && m.Item1.Promotion == PieceType.Rook).First().Item1);
                Reference.RefreshBoard(panel);
                Reference.ViewBoard(panel);
                Close();
            };
            pnlBishop.Click += (s, ev) =>
            {
                Reference.Chessboard.MakeMove(Reference.Chessboard.Moves.Where(m => m.Item1.To.Rank == To.Rank && m.Item1.To.File == To.File && m.Item1.Promotion == PieceType.Bishop).First().Item1);
                Reference.RefreshBoard(panel);
                Reference.ViewBoard(panel);
                Close();
            };
            pnlKnight.Click += (s, ev) =>
            {
                Reference.Chessboard.MakeMove(Reference.Chessboard.Moves.Where(m => m.Item1.To.Rank == To.Rank && m.Item1.To.File == To.File && m.Item1.Promotion == PieceType.Knight).First().Item1);
                Reference.RefreshBoard(panel);
                Reference.ViewBoard(panel);
                Close();
            };
        }
    }
}
