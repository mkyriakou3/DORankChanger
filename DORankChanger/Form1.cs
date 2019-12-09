using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DORankChanger
{
    public partial class Form1 : Form
    {
        private bool mouseDown;
        private Point lastLocation;
        private PictureBox currentRankPictureBox = new PictureBox();
        private int currentRankId = 0;

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.Salmon;
            this.TransparencyKey = Color.Salmon;
            this.RenderRankPictureBox();
            this.TopMost = true;

        }

        private void RenderRankPictureBox()
        {
            Image rankBitMap = Image.FromFile(GetRankFileLocation());
            this.Controls.Remove(this.currentRankPictureBox);
            this.currentRankPictureBox = new PictureBox();
            Size bitmapSize = new Size(rankBitMap.Width, rankBitMap.Height);
            this.MinimumSize = bitmapSize;
            this.MaximumSize = bitmapSize;
            this.Size = bitmapSize;
            currentRankPictureBox.ClientSize = bitmapSize;

            currentRankPictureBox.Image = rankBitMap;
            currentRankPictureBox.Dock = DockStyle.Fill;
            this.Controls.Add(currentRankPictureBox);
            this.FormBorderStyle = FormBorderStyle.None;

            currentRankPictureBox.MouseDown += Form1_MouseDown;
            currentRankPictureBox.MouseMove += Form1_MouseMove;
            currentRankPictureBox.MouseUp += Form1_MouseUp;

            this.Update();
        }

        private string GetRankFileLocation()
        {
            string RankDirectory = "Ranks";
            string[] rankImagesFileLocationsWithExtension = Directory.GetFiles(RankDirectory);
            if (this.currentRankId > 0)
                this.currentRankId %= rankImagesFileLocationsWithExtension.Length;
            return rankImagesFileLocationsWithExtension[this.currentRankId];
        }



        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mouseDown = true;
                this.lastLocation = e.Location;
            }
            else if (e.Button == MouseButtons.Right)
            {
                currentRankId++;
                this.RenderRankPictureBox();
            }
            
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            this.mouseDown = false;
        }

    }
}
