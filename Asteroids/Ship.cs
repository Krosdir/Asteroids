﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class Ship : BaseObject
    {
        private readonly Image img = Image.FromFile(Game.path + "/spaceship2.png");

        public static event Message MessageDie;

        public int Energy { get; private set; } = 100;
        public int Power { get; set; } = 10;
        public int Experience { get; set; } = 0;

        public static bool isUpHolded;
        public static bool isDownHolded;
        public static bool isLeftHolded;
        public static bool isRightHolded;
        public static bool isCtrlHolded;

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

        public void Left()
        {
            if (pos.X > 0) pos.X -= dir.X;
        }

        public void Right()
        {
            if (pos.X < Game.Width) pos.X += dir.X;
        }

        public void Die()
        {
            MessageDie?.Invoke();
        }
    }
}
