using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class FarStar : BaseObject
    {
        private readonly Image img = Image.FromFile(Game.path + "/FarStar.png");

        public FarStar(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(img, pos.X, pos.Y, size.Width + 15, size.Height + 15);
        }

    }
}
