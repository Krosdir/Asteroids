using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Asteroids
{
    class FarStar : BaseObject
    {
        private readonly Image img = Image.FromFile(Game.path + "/FarStar.png");

        Graphics g;
        Bitmap img2;

        float opacity = 1;
        int flagop = -1;

        public FarStar(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            //Анимация мерцания звезд, которая нагружает компьютер, из-за чего приложение тормозит
            //img2 = new Bitmap(img.Width, img.Height);
            //g = Graphics.FromImage(img2);
            //var attributes = new ImageAttributes();
            //var matrix = new ColorMatrix { Matrix33 = opacity };
            //attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            //g.DrawImage(img, new Rectangle(0, 0, img2.Width, img2.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, attributes);
            //g.Dispose();
            //Game.buffer.Graphics.DrawImage(img2, pos.X, pos.Y, size.Width + 15, size.Height + 15);
            Game.buffer.Graphics.DrawImage(img, pos.X, pos.Y, size.Width + 15, size.Height + 15);
        }

        public override void Update()
        {
            if (pos.X < 0)
            {
                pos.X = Game.Width + size.Width;
                pos.Y = r.Next(0, Game.Height);
            }
            pos.X = pos.X + dir.X;

            if (opacity == 1.0)
                flagop = r.Next(0, 20);
            if (flagop == 19)
            {
                opacity = opacity - 0.1f;
                if (opacity <= 0)
                    flagop = 20;
            }
            if (flagop == 20)
                opacity = opacity + 0.1f;
        }

    }
}
