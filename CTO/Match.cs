using CTO.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CTO
{
    public class Match
    {
        public Player White { get; set; }
        public Player Black { get; set; }
        public Board Chessboard { get; set; }
        public Square Selected { get; set; } = null;

        public Match(Player white, Player black)
        {
            White = white;
            Black = black;

            Chessboard = new Board();
        }

        public override string ToString()
        {
            return White.ToString(true) + " - " + Black.ToString(true);
        }

        public void ViewBoard(Panel panel)
        {
            if (panel.Controls.Count > 0)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (k == 0)
                    {
                        Label black = panel.Controls.Find("lblBlack", true).FirstOrDefault() as Label;

                        black.Text = Black.Points + " - " + Black.Name + " | " + Black.Rating;
                    }
                    else if (k == 2)
                    {
                        Label white = panel.Controls.Find("lblWhite", true).FirstOrDefault() as Label;

                        white.Text = White.Points + " - " + White.Name + " | " + White.Rating;
                    }
                    else
                    {
                        Panel board = panel.Controls.Find("pnlChessboard", true).FirstOrDefault() as Panel;


                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                Panel square = board.Controls.Find((char)('a' + j) + "|" + (i + 1), true).FirstOrDefault() as Panel;

                                if (Chessboard.ChessBoard()[j, i] != null)
                                    square.BackgroundImage = Properties.Resources.ResourceManager.GetObject(name: Chessboard.ChessBoard()[j, i].ToString() ?? "empty") as Image;
                            }
                        }
                    }
                }

                return;
            }

            for (int k = 0; k < 3; k++)
            {
                if (k == 0)
                {
                    Label black = new Label
                    {
                        Name = "lblBlack",
                        Size = new Size(277, 40),
                        Location = new Point(0, 0),
                        Text = Black.Points + " - " + Black.Name + " | " + Black.Rating
                    };

                    panel.Controls.Add(black);
                }
                else if (k == 2)
                {
                    Label white = new Label
                    {
                        Name = "lblWhite",
                        Size = new Size(277, 40),
                        Location = new Point(0, 237),
                        Text = White.Points + " - " + White.Name + " | " + White.Rating
                    };

                    panel.Controls.Add(white);
                }
                else
                {
                    Panel board = new Panel
                    {
                        Name = "pnlChessboard",
                        BackColor = Color.DarkGray,
                        Location = new Point(40, 40),
                        Size = new Size(197, 197),
                    };

                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if ((i + j) % 2 == 0)
                            {
                                Panel square = new Panel
                                {
                                    Size = new Size(board.Width / 8, board.Width / 8),
                                    Location = new Point(board.Width * j / 8, 197 - board.Width / 8 - board.Width * i / 8),
                                    Name = (char)('a' + j) + "|" + (i + 1),
                                    BackColor = Color.FromArgb(128, 128, 128),
                                    BackgroundImageLayout = ImageLayout.Stretch
                                };

                                if (Chessboard.ChessBoard()[j, i] != null)
                                    square.BackgroundImage = Properties.Resources.ResourceManager.GetObject(name: Chessboard.ChessBoard()[j, i].ToString() ?? "empty") as Image;
                                else
                                    square.BackgroundImage = null;
                                board.Controls.Add(square);
                            }
                            else
                            {
                                Panel square = new Panel
                                {
                                    Size = new Size(board.Width / 8, board.Width / 8),
                                    Location = new Point(board.Width * j / 8, 197 - board.Width / 8 - board.Width * i / 8),
                                    Name = (char)('a' + j) + "|" + (i + 1),
                                    BackColor = Color.FromArgb(192, 192, 192),
                                    BackgroundImageLayout = ImageLayout.Stretch
                                };

                                if (Chessboard.ChessBoard()[j, i] != null)
                                    square.BackgroundImage = Properties.Resources.ResourceManager.GetObject(name: Chessboard.ChessBoard()[j, i].ToString() ?? "empty") as Image;
                                else
                                    square.BackgroundImage = null;
                                board.Controls.Add(square);
                            }
                        }
                    }

                    panel.Controls.Add(board);
                }
            }
        }

        public void MovePiece(Square from, Square to, Panel panel)
        {
            if(Chessboard.Moves.Any(m => m.Item1.From.File == from.File && m.Item1.From.Rank == from.Rank && m.Item1.To.File == to.File && m.Item1.To.Rank == to.Rank))
            {
                if ((to.Rank == 0 || to.Rank == 7) && Chessboard.Moves.Any(
                    m => m.Item1.From.File == from.File && m.Item1.From.Rank == from.Rank && m.Item1.To.File == to.File && m.Item1.To.Rank == to.Rank && m.Item1.Promotion != PieceType.Pawn))
                {
                    Promotion promotion = new Promotion(this, to, panel);
                    promotion.Show();
                }
                else
                {
                    Chessboard.MakeMove(Chessboard.Moves.First(m => m.Item1.From.File == from.File && m.Item1.From.Rank == from.Rank && m.Item1.To.File == to.File && m.Item1.To.Rank == to.Rank).Item1);
                    RefreshBoard(panel as Panel);
                    ViewBoard(panel as Panel);

                    /*if ((White.Name == "BYE" && Chessboard.IsWhiteToMove) || (Black.Name == "BYE" && !Chessboard.IsWhiteToMove))
                    {
                        Chessboard.MakeMove(Engine.FindBestMove(Chessboard, 2));
                        RefreshBoard(panel as Panel);
                        ViewBoard(panel as Panel);
                        Chessboard.GenerateLegalMoves();
                    }*/

                    Chessboard.GenerateLegalMoves();
                }
            }
        }

        public void SelectSquare(Square square, Panel panel)
        {
            if (Selected == null)
            {
                if (Chessboard.ChessBoard()[square.File, square.Rank] != null)
                    Selected = square;
            }
            else
            {
                MovePiece(Selected, square, panel);
                Selected = null;
            }
        }

        public void SelectSquare(object sender, EventArgs e)
        {
            Panel square = sender as Panel;
            if (square != null)
            {
                string[] parts = square.Name.Split('|');
                char file = parts[0][0];
                int rank = int.Parse(parts[1]);
                SelectSquare(new Square(file - 'a', rank - 1), square.Parent.Parent as Panel);
            }
        }

        public void RefreshBoard(Panel panel)
        {
            Panel board = panel.Controls.Find("pnlChessboard", true)[0] as Panel;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Panel square = board.Controls.Find((char)('a' + j) + "|" + (i + 1), true).FirstOrDefault() as Panel;
                    if (Chessboard.ChessBoard()[j, i] != null)
                        square.BackgroundImage = Properties.Resources.ResourceManager.GetObject(name: Chessboard.ChessBoard()[j, i].ToString() ?? "empty") as Image;
                    else
                        square.BackgroundImage = null;
                }
            }
        }
    }
}
