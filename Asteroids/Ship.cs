using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    delegate int Delegate();

    class Ship : BaseObject
    {
        private readonly Image img = Image.FromFile(Game.path + "/spaceship.png");

        public static event Message MessageDie;
        public static Delegate Event;

        public int Energy { get; private set; } = 100;
        public static int bulletcount = 0;

        public void EnergyLow(int n)
        {
            Energy -= n;
        }

        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(img, pos.X, pos.Y, size.Width, size.Height);
        }

        public override void Update()
        {
        }

        public void Up()
        {
            if (pos.Y>0) pos.Y-=dir.Y;
        }

        public void Down()
        {
            if (pos.Y < Game.Height) pos.Y += dir.Y;
        }

        public void Die()
        {
            MessageDie?.Invoke();
        }
    }
}
