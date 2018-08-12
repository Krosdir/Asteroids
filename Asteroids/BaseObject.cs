﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class BaseObject
    {
        protected Point pos;
        protected Point dir;
        protected Size size;

        public BaseObject(Point pos, Point dir, Size size)
        {
            this.pos = pos;
            this.dir = dir;
            this.size = size;
        }

        public void Draw()
        {
            Game.buffer.Graphics.DrawEllipse(Pens.Wheat, pos.X, pos.Y, size.Width, size.Height);
        }

        public void Update()
        {
            pos.X = pos.X + dir.X;
            pos.Y = pos.Y + dir.Y;
            if ((pos.X < 0) || (pos.X > Game.Width)) dir.X = -dir.X;
            if ((pos.Y < 0) || (pos.Y > Game.Width)) dir.Y = -dir.Y;
        }
    }
}
