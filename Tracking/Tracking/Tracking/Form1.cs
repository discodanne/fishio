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

        private String currentButton = "WAIT";

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

                currentButton = GetButton(center);
            }
            else
            {
                currentButton = "WAIT";
            }

            Console.WriteLine(currentButton);
        }



        private void WriteToFile(string button)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\..\..\..\..\LuaScripts\input.txt"))
            {
                file.Write(button);
            }
        }

        private String GetButton(System.Drawing.Point location)
        {
            int x = location.X;
            int y = location.Y;

            if (isBetween(x, 0, 300) && isBetween(y, 0, 342))
            {
                return "Left";
            }
            else if (isBetween(x, 300, 600) && isBetween(y, 0, 342))
            {
                return "Right";
            }
            else if (isBetween(x, 600, 900) && isBetween(y, 0, 342))
            {
                return "B";
            }
            else if (isBetween(x, 0, 300) && isBetween(y, 342, 720))
            {
                return "Up";
            }
            else if (isBetween(x, 300, 600) && isBetween(y, 342, 720))
            {
                return "Down";
            }
            else if (isBetween(x, 600, 900) && isBetween(y, 342, 720))
            {
                return "A";
            }
            else if (isBetween(x, 900, 1280) && isBetween(y, 342, 533))
            {
                return "Start";
            }
            else if (isBetween(x, 900, 1280) && isBetween(y, 533, 720))
            {
                return "Select";
            }
            else
            {
                return "WAIT";
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
            videoSource.VideoResolution = videoSource.VideoCapabilities[1];
            player.VideoSource = videoSource;
            player.Start();

            redLower.Value = rL;
            redUpper.Value = rU;
            greenLower.Value = gL;
            greenUpper.Value = gU;
            blueLower.Value = bL;
            blueUpper.Value = bU;
        }

        private bool isBetween(int value, int lower, int upper)
        {
            return (value >= lower && value < upper);
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

        private void writeInterval_ValueChanged(object sender, EventArgs e)
        {
            writeTimer.Interval = (int)writeInterval.Value;
        }

        private void writeTimer_Tick(object sender, EventArgs e)
        {
            WriteToFile(currentButton);

            Console.WriteLine("Wrote " + currentButton + " to file!");
        }
    }
}
