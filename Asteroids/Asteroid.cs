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
        Random r;
        private readonly Image img = Image.FromFile(Application.StartupPath + "/asteroid.png");
        int key=0;
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        { }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(img, pos.X,pos.Y,size.Width+10,size.Height+10);
        }

        public override void Update()
        {
            r = new Random();
            if (key < 40)
            {
                pos.X = pos.X + dir.X;
                pos.Y = pos.Y + (int)(dir.Y * 0.1);
                key++;
            }
            else if (key < 80 && key >= 40)
            {
                pos.X = pos.X + dir.X;
                pos.Y = pos.Y - (int)(dir.Y  *0.1);
                key++;
            }
            if (key == 80) key = 0;
            if (pos.X < 0)
            {
                pos.X = Game.Width + size.Width;
                pos.Y = r.Next(0,601);
            }
        }
    }
}
