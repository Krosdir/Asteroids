using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Asteroids
{
    class Asteroid : BaseObject
    {
        Image img = Image.FromFile(Game.path + "/asteroid.png");
        int key=0;
        public int Power { get; set; }
        public int powermem;
        public float angle;

        Graphics g;
        Bitmap img2;
        private readonly int up;
        private readonly int down;

        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = r.Next(24,70);
            powermem = Power;
            down = r.Next(30, 50);
            up = down * 2;
            angle = dir.X;
        }

        public override void Draw()
        {
            img2 = new Bitmap(img.Width,img.Height, img.PixelFormat);
            g = Graphics.FromImage(img2);
            g.TranslateTransform(img.Width / 2, img.Height / 2);
            g.RotateTransform(angle);
            g.TranslateTransform(-img.Width / 2, -img.Height / 2);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(img, 0, 0, img.Width, img.Height);
            g.Dispose();
            Game.buffer.Graphics.DrawImage(img2, pos.X,pos.Y,size.Width,size.Height);
            if (angle < 360) angle += dir.X;
            else angle = 0;
        }

        public override void Update()
        {
            if (key < down)
            {
                pos.X = pos.X + dir.X;
                pos.Y = pos.Y + (int)(dir.Y * 0.1);
                key++;
            }
            else if (key < up && key >= down)
            {
                pos.X = pos.X + dir.X;
                pos.Y = pos.Y - (int)(dir.Y  *0.1);
                key++;
            }
            if (key == up) key = 0;
            if (pos.X < 0)
            {
                pos.X = Game.Width + size.Width;
                pos.Y = r.Next(0,601);
            }
            //img.RotateFlip(RotateFlipType.Rotate270FlipXY);
        }
    }
}
