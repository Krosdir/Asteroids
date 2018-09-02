using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroids
{
    class Asteroid : BaseObject
    {
        private readonly Image img = Image.FromFile(Game.path + "/asteroid.png");
        int key=0;
        public int Power { get; set; }
        public int powermem;

        private readonly int up;
        private readonly int down;

        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = r.Next(24,70);
            powermem = Power;
            down = r.Next(30, 50);
            up = down * 2;
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(img, pos.X,pos.Y,size.Width,size.Height);
        }

        public override void Update()
        {
            if (key < down)
            {
                pos.X = pos.X + dir.X;
                pos.Y = pos.Y + (int)(dir.Y * 0.1);
                key++;
            }
            else if (key < up && key >= down)
            {
                pos.X = pos.X + dir.X;
                pos.Y = pos.Y - (int)(dir.Y  *0.1);
                key++;
            }
            if (key == up) key = 0;
            if (pos.X < 0)
            {
                pos.X = Game.Width + size.Width;
                pos.Y = r.Next(0,601);
            }
        }
    }
}
