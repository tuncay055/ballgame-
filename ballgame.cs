
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp17
{
    public partial class Form1 : Form
    {
        int ballSpeedX = 5;
        int ballSpeedY = 5;

        int score = 0;

        // Best score
        static int bestScore = 0;

        bool gameOver = false;

        public Form1()
        {
            InitializeComponent();

            // Keyboard
            KeyDown += Form1_KeyDown;

            // Mouse movement
            MouseMove += Form1_MouseMove;
            panel1.MouseMove += Form1_MouseMove;
            lblBall.MouseMove += Form1_MouseMove;

            // New Game button
            btnNewGame.Click += btnNewGame_Click;

            // Timer
            timer1.Tick += timer1_Tick;

            timer1.Start();
        }

        // Keyboard control
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && panel1.Left > 0)
            {
                panel1.Left -= 20;
            }

            if (e.KeyCode == Keys.Right &&
                panel1.Right < ClientSize.Width)
            {
                panel1.Left += 20;
            }
        }

        // Mouse control
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (gameOver)
                return;

            Point mousePosition =
                PointToClient(Control.MousePosition);

            int newPosition = mousePosition.X - panel1.Width / 2;

            if (newPosition < 0)
            {
                newPosition = 0;
            }

            if (newPosition > ClientSize.Width - panel1.Width)
            {
                newPosition = ClientSize.Width - panel1.Width;
            }

            panel1.Left = newPosition;
        }

        // Ball movement
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblBall.Left += ballSpeedX;
            lblBall.Top += ballSpeedY;

            // Left wall
            if (lblBall.Left <= 0)
            {
                ballSpeedX = -ballSpeedX;
            }

            // Right wall
            if (lblBall.Right >= ClientSize.Width)
            {
                ballSpeedX = -ballSpeedX;
            }

            // Top wall
            if (lblBall.Top <= 0)
            {
                ballSpeedY = -ballSpeedY;
            }

            // Player collision
            if (lblBall.Bottom >= panel1.Top &&
                lblBall.Top < panel1.Bottom &&
                lblBall.Right >= panel1.Left &&
                lblBall.Left <= panel1.Right &&
                ballSpeedY > 0)
            {
                ballSpeedY = -ballSpeedY;

                score++;

                lblScore.Text = "Score: " + score;

                // Update best score
                if (score > bestScore)
                {
                    bestScore = score;

                    lblBestScore.Text =
                        "Best Score: " + bestScore;
                }
            }

            // Game Over
            if (lblBall.Top > ClientSize.Height)
            {
                EndGame();
            }
        }

        // End game
        private void EndGame()
        {
            timer1.Stop();

            gameOver = true;

            lblGameOver.Text =
                "GAME OVER\nYour Score: " + score;

            lblGameOver.Visible = true;
            lblGameOver.BringToFront();
        }

        // New Game button
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        // Restart game
        private void RestartGame()
        {
            score = 0;

            ballSpeedX = 5;
            ballSpeedY = 5;

            lblBall.Location = new Point(60, 60);

            panel1.Location = new Point(230, 410);

            lblScore.Text = "Score: 0";

            lblBestScore.Text =
                "Best Score: " + bestScore;

            lblGameOver.Visible = false;

            gameOver = false;

            timer1.Start();
        }
    }
}
