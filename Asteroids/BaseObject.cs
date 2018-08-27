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

    delegate void Message();

    abstract class BaseObject : ICollision
    {

        protected Point pos;
        protected Point dir;
        protected Size size;

        protected static Random r = new Random();

        public BaseObject(Point pos, Point dir, Size size)
        {
            this.pos = pos;
            this.dir = dir;
            this.size = size;
        }

        public int X
        {
            get
            {
                return pos.X;
            }
            set
            {
                pos.X = value;
            }
        }
        public int Y
        {
            get
            {
                return pos.Y;
            }
            set
            {
                pos.Y = value;
            }
        }
        public Size Size(Size size)
        {
            return this.size = size;
        }

        public Rectangle Rect
        {
            get
            {
                return new Rectangle(pos, size);
            }
        }

        abstract public void Draw();

        abstract public void Update();

        public bool Collision(ICollision o)
        {
            if (o.Rect.IntersectsWith(Rect))
                return true;
            else
                return false;
        }
    }
}
