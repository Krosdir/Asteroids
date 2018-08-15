using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class BaseObject
    {
        Random r;

        protected Point pos;
        protected Point dir;
        protected Size size;

        public BaseObject(Point pos, Point dir, Size size)
        {
            this.pos = pos;
            this.dir = dir;
            this.size = size;
        }

        public virtual void Draw()
        {
            Game.buffer.Graphics.FillEllipse(Brushes.Wheat, pos.X, pos.Y, size.Width, size.Height);
        }

        public virtual void Update()
        {
            r =new Random();
            
            if (pos.X < 0)
            {
                pos.X = Game.Width + size.Width;
                pos.Y = r.Next(0, 601);
            }
            pos.X = pos.X + dir.X;
        }
    }
}
