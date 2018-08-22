using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Star : BaseObject
    {
        private readonly Image img = Image.FromFile(Game.path + "/CloseStar.png");

        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(img, pos.X, pos.Y, size.Width+15, size.Height+15);
        }

        public override void Update()
        {
            r = new Random();
            if (pos.X < 0)
            {
                pos.X = Game.Width + size.Width;
                pos.Y = r.Next(0, Game.Height);
            }
            pos.X = pos.X + dir.X;

        }

    }
}
