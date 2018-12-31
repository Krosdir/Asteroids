using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Asteroids
{
    class Shard : Asteroid
    {
        private readonly Image img = Image.FromFile(Game.path + "/asteroid.png");

        public static event Action<Shard> ShardDie;

        private int keyX=0, keyY=0;
        Graphics g;
        Bitmap img2;

        public Shard(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            while (keyX == 0)
                keyX = r.Next(-1, 2);
            while (keyY == 0)
                keyY = r.Next(-1, 2);
        }


        public override void Draw()
        {
            img2 = new Bitmap(img.Width, img.Height, img.PixelFormat);
            g = Graphics.FromImage(img2);
            g.TranslateTransform(img.Width / 2, img.Height / 2);
            g.RotateTransform(angle);
            g.TranslateTransform(-img.Width / 2, -img.Height / 2);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(img, 0, 0, img.Width, img.Height);
            g.Dispose();
            Game.buffer.Graphics.DrawImage(img2, pos.X, pos.Y, size.Width, size.Height);
            if (keyX > 0) angle += dir.X;
            else if (angle >= 360) angle = 0;
            if (keyX < 0) angle -= dir.X;
            else if (angle <= -360) angle = 0;
        }

        public override void Update()
        {
            

            pos.X += dir.X*keyX;
            pos.Y += dir.Y * keyY;
            
            if (pos.X > Game.Width || pos.X < 0 || pos.Y > Game.Height || pos.Y < 0)
            {
                ShardDie?.Invoke(this);
            }
        }
    }
}
