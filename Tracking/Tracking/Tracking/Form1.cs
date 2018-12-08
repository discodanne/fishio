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

        private int brightnessCorrection = 0;

        private Bitmap imgA;
        private Bitmap imgB;
        private Bitmap imgUp;
        private Bitmap imgDown;
        private Bitmap imgRight;
        private Bitmap imgLeft;
        private Bitmap imgStart;
        private Bitmap imgSelect;

        private List<ScreenButton> screenButtons = new List<ScreenButton>();
        private List<ScreenImage> screenImages = new List<ScreenImage>();

        private VideoSourcePlayer player;

        private String currentButton = "WAIT";

        public Form1()
        {
            InitializeComponent();
            LoadImages();
            Setup();
        }

        private void PlayerNewFrame(object sender, ref Bitmap image)
        {
            BrightnessCorrection brightnessFilter = new BrightnessCorrection(brightnessCorrection);
            brightnessFilter.ApplyInPlace(image);



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



            using (Graphics g = Graphics.FromImage(image))
            {
                foreach (ScreenButton s in screenButtons)
                {
                    Pen p = new Pen(Color.Black);

                    Rectangle destRect = new Rectangle(s.location, s.size);
                    Rectangle srcRect = new Rectangle(0, 0, s.image.image.Width, s.image.image.Height);


                    g.DrawImage(s.image.image, destRect, srcRect, GraphicsUnit.Pixel);

                    g.DrawRectangle(p, destRect);
                }
            }
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
            for (int i = 0; i < screenButtons.Count; i++)
            {
                if (screenButtons[i].IsIn(location))
                {
                    return screenButtons[i].image.value;
                }
            }

            return "WAIT";
        }



        private void LoadImages()
        {
            try
            {
                imgA = new Bitmap(System.Drawing.Image.FromFile(@"..\..\..\..\..\pictures\a.png"));
                imgB = new Bitmap(System.Drawing.Image.FromFile(@"..\..\..\..\..\pictures\b.png"));
                imgDown = new Bitmap(System.Drawing.Image.FromFile(@"..\..\..\..\..\pictures\down.png"));
                imgLeft = new Bitmap(System.Drawing.Image.FromFile(@"..\..\..\..\..\pictures\left.png"));
                imgRight = new Bitmap(System.Drawing.Image.FromFile(@"..\..\..\..\..\pictures\right.png"));
                imgSelect = new Bitmap(System.Drawing.Image.FromFile(@"..\..\..\..\..\pictures\select.png"));
                imgStart = new Bitmap(System.Drawing.Image.FromFile(@"..\..\..\..\..\pictures\start.png"));
                imgUp = new Bitmap(System.Drawing.Image.FromFile(@"..\..\..\..\..\pictures\up.png"));

                screenImages.Add(new ScreenImage(imgA, "A"));
                screenImages.Add(new ScreenImage(imgB, "B"));
                screenImages.Add(new ScreenImage(imgDown, "Down"));
                screenImages.Add(new ScreenImage(imgLeft, "Left"));
                screenImages.Add(new ScreenImage(imgRight, "Right"));
                screenImages.Add(new ScreenImage(imgUp, "Up"));
                screenImages.Add(new ScreenImage(imgStart, "Start", true));
                screenImages.Add(new ScreenImage(imgSelect, "Select", true));

                List<ScreenImage> images = new List<ScreenImage>();

                for (int i = 0; i < screenImages.Count; i++)
                {
                    images.Add(new ScreenImage(screenImages[i].image, screenImages[i].value));
                }

                screenButtons.Add(new ScreenButton(new System.Drawing.Point(0, 0), new Size(320, 360)));
                screenButtons.Add(new ScreenButton(new System.Drawing.Point(320, 0), new Size(320, 360)));
                screenButtons.Add(new ScreenButton(new System.Drawing.Point(320 * 2, 0), new Size(320, 360)));
                screenButtons.Add(new ScreenButton(new System.Drawing.Point(0, 360), new Size(320, 360)));
                screenButtons.Add(new ScreenButton(new System.Drawing.Point(320, 360), new Size(320, 360)));
                screenButtons.Add(new ScreenButton(new System.Drawing.Point(320 * 2, 360), new Size(320, 360)));
                ScreenButton start = new ScreenButton(new System.Drawing.Point(320 * 3, 360), new Size(320, 180));
                ScreenButton select = new ScreenButton(new System.Drawing.Point(320 * 3, 540), new Size(320, 180));
                start.canRandomize = false;
                select.canRandomize = false;
                screenButtons.Add(start);
                screenButtons.Add(select);



                for (int i = 0; i < screenButtons.Count; i++)
                {
                    screenButtons[i].SetImage(images[i]);
                }
            }
            catch (Exception e)
            {

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
            foreach (VideoCapabilities vc in videoSource.VideoCapabilities)
            {
                if (vc.FrameSize.Height == 720)
                {
                    videoSource.VideoResolution = vc;
                }
            }
            player.VideoSource = videoSource;
            player.Start();

            redLower.Value = rL;
            redUpper.Value = rU;
            greenLower.Value = gL;
            greenUpper.Value = gU;
            blueLower.Value = bL;
            blueUpper.Value = bU;
            brightnessCorrectionNUM.Value = brightnessCorrection;
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

        private void randomizeTime_ValueChanged(object sender, EventArgs e)
        {
            randomizerTimer.Interval = (int)randomizeTime.Value;
        }

        private void randomizerTimer_Tick(object sender, EventArgs e)
        {
            clockTimer.Stop();
            clockTimer.Start();

            List<ScreenImage> images = new List<ScreenImage>();

            for (int i = 0; i < screenImages.Count; i++)
            {
                if (!screenImages[i].lockedInPlace)
                {
                    images.Add(new ScreenImage(screenImages[i].image, screenImages[i].value));
                }
            }



            screenButtons = screenButtons.OrderBy(a => Guid.NewGuid()).ToList();

            foreach (ScreenButton s in screenButtons)
            {
                if (s.canRandomize)
                {
                    s.randomizeImage(images);
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            brightnessCorrection = (int)brightnessCorrectionNUM.Value;
        }

        private void clockTimer_Tick(object sender, EventArgs e)
        {

        }
    }
}
