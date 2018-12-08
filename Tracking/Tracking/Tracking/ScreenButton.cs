using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;



namespace Tracking
{
    class ScreenButton
    {
        public Boolean canRandomize = true;

        public Point location
        {
            get;
            private set;
        }

        public Size size
        {
            get;
            private set;
        }

        public ScreenImage image
        {
            get;
            private set;
        }




        public ScreenButton(Point location, Size size)
        {
            this.location = location;
            this.size = size;
        }

        public void SetImage(ScreenImage image)
        {
            this.image = image;
        }

        public void randomizeImage(List<ScreenImage> images)
        {
            int index = new Random().Next(0, images.Count);

            SetImage(images[index]);

            images.RemoveAt(index);
        }

        public Boolean IsIn(Point p)
        {
            if (p.X >= location.X && p.X < location.X + size.Width)
            {
                if (p.Y >= location.Y && p.Y < location.Y + size.Height)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
