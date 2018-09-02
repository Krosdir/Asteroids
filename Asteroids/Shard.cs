using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class Shard : Asteroid
    {
        private readonly Image img = Image.FromFile(Game.path + "/asteroid.png");

        public static event Action<Shard> ShardDie;

        private int keyX, keyY;

        public Shard(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            keyX = r.Next(-1, 2);
            while (keyX == 0)
                keyX = r.Next(-1, 2);
            keyY = r.Next(-1, 2);
            while (keyY == 0)
                keyY = r.Next(-1, 2);
        }


        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(img, pos.X, pos.Y, size.Width, size.Height);
        }

        public override void Update()
        {
            

            pos.X += dir.X*keyX;
            pos.Y += dir.Y * keyY;
            
            if (pos.X > Game.Width || pos.X < 0 || pos.Y > Game.Height || pos.Y < 0)
            {
                //pos.X = 0;
                //pos.Y = r.Next(0, Game.Height-50);
                ShardDie?.Invoke(this);
            }
        }
    }
}
