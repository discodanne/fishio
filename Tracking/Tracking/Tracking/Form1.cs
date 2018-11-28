using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Video.DirectShow;
using AForge.Controls;
using System.Windows.Forms;

namespace Tracking
{
    public partial class Form1 : Form
    {
        private int rL = 230;
        private int rU = 255;
        private int gL = 0;
        private int gU = 250;
        private int bL = 0;
        private int bU = 250;

        private VideoSourcePlayer player;

        public Form1()
        {
            InitializeComponent();
            Setup();
        }

        private void PlayerNewFrame(object sender, ref Bitmap image)
        {
            ColorFiltering colorFilter = new ColorFiltering();
            colorFilter.Red = new IntRange(rL, rU);
            colorFilter.Green = new IntRange(gL, gU);
            colorFilter.Blue = new IntRange(bL, bU);

            Bitmap objectImage = colorFilter.Apply(image);

            if (debugTrackingCB.Checked)
            {
                image = objectImage;
            }

            BlobCounter blobCounter = new BlobCounter();
            blobCounter.MinWidth = (int)blobWidth.Value;
            blobCounter.MinHeight = (int)blobHeight.Value;
            blobCounter.FilterBlobs = true;
            blobCounter.ObjectsOrder = ObjectsOrder.Size;

            Bitmap grayScale = Grayscale.CommonAlgorithms.BT709.Apply(objectImage);

            blobCounter.ProcessImage(grayScale);

            Rectangle[] rectangles = blobCounter.GetObjectsRectangles();

            if (rectangles.Length > 0)
            {
                Rectangle objectRect = rectangles[0];

                System.Drawing.Point center = new System.Drawing.Point();
                center.X = objectRect.X + (objectRect.Width / 2) - 3;
                center.Y = objectRect.Y + (objectRect.Height / 2) - 3;

                Graphics g = Graphics.FromImage(image);

                using (Pen p = new Pen(Color.Red, 3))
                {

                    g.DrawEllipse(p, center.X, center.Y, 6, 6);
                }

                g.Dispose();
            }
        }

        private void Setup()
        {
            player = new VideoSourcePlayer();
            player.Location = new System.Drawing.Point(0, 24);
            player.Size = new Size(1280, 720);
            player.NewFrame += PlayerNewFrame;
            this.Controls.Add(player);

            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            player.VideoSource = videoSource;
            player.Start();

            redLower.Value = rL;
            redUpper.Value = rU;
            greenLower.Value = gL;
            greenUpper.Value = gU;
            blueLower.Value = bL;
            blueUpper.Value = bU;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            player.Stop();
            player.Dispose();
        }



        private void redLower_ValueChanged(object sender, EventArgs e)
        {
            rL = (int)redLower.Value;
        }

        private void redUpper_ValueChanged(object sender, EventArgs e)
        {
            rU = (int)redUpper.Value;
        }

        private void greenLower_ValueChanged(object sender, EventArgs e)
        {
            gL = (int)greenLower.Value;
        }

        private void greenUpper_ValueChanged(object sender, EventArgs e)
        {
            gU = (int)greenUpper.Value;
        }

        private void blueLower_ValueChanged(object sender, EventArgs e)
        {
            bL = (int)blueLower.Value;
        }

        private void blueUpper_ValueChanged(object sender, EventArgs e)
        {
            bU = (int)blueUpper.Value;
        }
    }
}
