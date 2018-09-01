using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class Rocket : BaseObject
    {
        private readonly Image img = Image.FromFile(Game.path + "/rocket.png");

        public static event Action<Rocket> RocketDie;

        public Rocket(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(img, pos.X, pos.Y, size.Width, size.Height);
        }

        public override void Update()
        {
            pos.X += 30;
            if (pos.X > Game.Width)
            {
                //pos.X = 0;
                //pos.Y = r.Next(0, Game.Height-50);
                RocketDie?.Invoke(this);
            }
        }

    }
}
