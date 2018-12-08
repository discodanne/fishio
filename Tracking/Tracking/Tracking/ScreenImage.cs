using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tracking
{
    class ScreenImage
    {
        public Boolean lockedInPlace = false;
        public Bitmap image;
        public string value;



        public ScreenImage(Bitmap image, string value)
        {
            this.image = image;
            this.value = value;
        }

        public ScreenImage(Bitmap image, string value, Boolean lockedInPlace)
        {
            this.image = image;
            this.value = value;
            this.lockedInPlace = true;
        }
    }
}
