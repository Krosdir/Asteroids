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
        private readonly Image img = Image.FromFile(Game.path + "/bullet.png");

        public static event Action<Bullet> BulletDie;
        public static int bulletcount = 0;

        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(img, pos.X, pos.Y, size.Width, size.Height);
        }

        public override void Update()
        {
            pos.X+=30;
            if (pos.X > Game.Width)
            {
                BulletDie?.Invoke(this);
            }
        }

    }
}
