using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
    }

    abstract class BaseObject : ICollision
    {

        protected Point pos;
        protected Point dir;
        protected Size size;

        protected static Random r;

        public BaseObject(Point pos, Point dir, Size size)
        {
            this.pos = pos;
            this.dir = dir;
            this.size = size;
        }

        public Rectangle Rect
        {
            get
            {
                return new Rectangle(pos, size);
            }
        }

        abstract public void Draw();

        public virtual void Update()
        {
            r =new Random();
            if (pos.X < 0)
            {
                pos.X = Game.Width + size.Width;
                pos.Y = r.Next(0, Game.Height);
            }
            pos.X = pos.X + dir.X;

        }

        public bool Collision(ICollision o)
        {
            if (o.Rect.IntersectsWith(Rect))
            {
                pos.X = Game.Width + size.Width;
                pos.Y = r.Next(0, Game.Height - 50);
                return true;
            }
            else
                return false;
        }
    }
}
