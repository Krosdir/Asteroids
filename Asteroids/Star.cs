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
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawLine(Pens.SandyBrown, pos.X, pos.Y, pos.X+size.Width, pos.Y+size.Height);
            Game.buffer.Graphics.DrawLine(Pens.SandyBrown, pos.X+size.Width, pos.Y, pos.X, pos.Y+size.Height);
        }

        

    }
}
