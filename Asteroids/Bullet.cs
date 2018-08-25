using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class Bullet : BaseObject
    {
        public static event Action<Bullet> BulletDie;

        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }

        public override void Draw()
        {
            Game.buffer.Graphics.FillRectangle(Brushes.OrangeRed, pos.X, pos.Y, size.Width, size.Height);
        }

        public override void Update()
        {
            pos.X+=10;
            if (pos.X > Game.Width)
            {
                //pos.X = 0;
                //pos.Y = r.Next(0, Game.Height-50);
                BulletDie?.Invoke(this);
            }
        }

    }
}
