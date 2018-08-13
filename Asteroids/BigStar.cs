using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class BigStar : BaseObject
    {

        int key=0;
        public BigStar(Point pos, Point dir, Size size) : base(pos, dir, size)
        { }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawLine(Pens.Aqua, pos.X+size.Width/4, pos.Y+size.Height/4, pos.X + size.Width*3/4, pos.Y + size.Height*3/4);
            Game.buffer.Graphics.DrawLine(Pens.Aqua, pos.X + size.Width*3/4, pos.Y+size.Height/4, pos.X+size.Width/4, pos.Y + size.Height*3/4);
            Game.buffer.Graphics.DrawLine(Pens.Aqua, pos.X+size.Width/2, pos.Y, pos.X+size.Width/2, pos.Y+size.Height);
            Game.buffer.Graphics.DrawLine(Pens.Aqua, pos.X, pos.Y+size.Height/2, pos.X + size.Width, pos.Y + size.Height/2);
        }

        public override void Update()
        {
            if (key == 0)
            {
                pos.X = pos.X + dir.X;
                pos.Y = pos.Y + dir.Y*2;
                key++;
            }
            else if (key == 1)
            {
                pos.X = pos.X + dir.X;
                pos.Y = pos.Y - dir.Y*2;
                key--;
            }
            if (pos.X < 0) pos.X = Game.Width + size.Width;
        }
    }
}
