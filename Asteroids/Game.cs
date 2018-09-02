using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroids
{
    class Game
    {
        static BufferedGraphicsContext context;
        public static BufferedGraphics buffer;

        public static string path = @"C:/Users/pokem/Documents/Visual Studio 2017/Projects/Asteroids/Asteroids/pictures";

        static Ship ship = new Ship(new Point(10,400), new Point(12,12), new Size(75,75));
        static BaseObject[] objs;
        static List<Asteroid> asteroids;
        static List<Shard> shards;
        static List<Bullet> bullets;
        static List<Rocket> rockets;

        static Random r;
        static Timer timer;
        static Timer timershoot = new Timer();
        static int countasteroids = 20;
        

        public static int Width { get; set; }
        public static int Height { get; set; }

        static Game()
        {
        }

        public static void Init(Form form)
        {
            //Графическое устройство для вывода графики
            Graphics grx;
            //Предоставляет доступ к главному буферу графического контекста для текущего приложения
            context = BufferedGraphicsManager.Current;
            //Создаем объект - поверхность рисования и связываем его с формой
            grx = form.CreateGraphics();
            //Запоминаем размеры формы
            Width = form.Width;
            Height = form.Height;
            //form.BackgroundImage = new Bitmap(img);
            Load();
            //Связываем буфер в памяти с графическим объектом
            //Для того чтобы рисовать в буфере
            buffer = context.Allocate(grx, new Rectangle(0, 0, Width, Height));
            timer = new Timer();
            timer.Interval = 35;
            timershoot.Interval = 85;
            timer.Start();
            timershoot.Start();
            timer.Tick += Timer_Tick;
            timershoot.Tick += Timershoot_Tick;
            form.KeyDown += Form_KeyDown;
            form.KeyUp += Form_KeyUp;
            Ship.MessageDie += Finish;
            Bullet.BulletDie += Bullet_BulletDie;
            Rocket.RocketDie += Rocket_RocketDie;
            Shard.ShardDie += Shard_ShardDie;
        }

        private static void Shard_ShardDie(Shard shard)
        {
            shards.Remove(shard);
        }

        private static void Rocket_RocketDie(Rocket rocket)
        {
            rockets.Remove(rocket);
        }

        private static void Timershoot_Tick(object sender, EventArgs e)
        {
            if (Ship.isCtrlHolded && ship.Experience >= 0 && ship.Experience < 500)
            {
                bullets.Add(new Bullet(new Point(ship.Rect.X +ship.Rect.Width + 15, ship.Rect.Y + 30), new Point(5, 0), new Size(16, 16)));
                Bullet.bulletcount++;
            }
            if (Ship.isCtrlHolded && ship.Experience >= 500)
            {
                bullets.Add(new Bullet(new Point(ship.Rect.X + ship.Rect.Width + 15, ship.Rect.Y + 20), new Point(5, 0), new Size(16, 16)));
                bullets.Add(new Bullet(new Point(ship.Rect.X + ship.Rect.Width + 15, ship.Rect.Y + 40), new Point(5, 0), new Size(16, 16)));
                Bullet.bulletcount=Bullet.bulletcount+2;
            }
        }

        private static void Form_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) Ship.isCtrlHolded = false;
            if (e.KeyCode == Keys.Up) Ship.isUpHolded = false;
            if (e.KeyCode == Keys.Down) Ship.isDownHolded = false;
            if (e.KeyCode == Keys.Left) Ship.isLeftHolded = false;
            if (e.KeyCode == Keys.Right) Ship.isRightHolded = false;
        }

        private static void Bullet_BulletDie(Bullet bullet)
        {
            bullets.Remove(bullet);
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) Ship.isCtrlHolded = true;
            if (e.KeyCode == Keys.Up) Ship.isUpHolded = true;
            if (e.KeyCode == Keys.Down) Ship.isDownHolded = true;
            if (e.KeyCode == Keys.Left) Ship.isLeftHolded = true;
            if (e.KeyCode == Keys.Right) Ship.isRightHolded = true;
            //TO DOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
            if (e.KeyCode == Keys.Z) rockets.Add(new Rocket(new Point(ship.Rect.X + ship.Rect.Width + 15, ship.Rect.Y+5), new Point(5, 0), new Size(66, 66)));
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
            
            if (Ship.isUpHolded) ship.Up();
            if (Ship.isDownHolded) ship.Down();
            if (Ship.isLeftHolded) ship.Left();
            if (Ship.isRightHolded) ship.Right();
        }

        public static void Draw()
        {
            //Проверяем вывод графики
            buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objs)
                obj.Draw();
            foreach (Asteroid obj in asteroids)
                obj.Draw();
            foreach (Shard obj in shards)
                obj.Draw();
            foreach (Bullet bullet in bullets)
                bullet.Draw();
            foreach (Rocket rocket in rockets)
                rocket.Draw();
            ship.Draw();
            buffer.Graphics.DrawString("Energy: " + ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            buffer.Graphics.DrawString("Count of bullet: " + Bullet.bulletcount, SystemFonts.DefaultFont, Brushes.White, 0, 15);
            buffer.Graphics.DrawString("Experience: " + ship.Experience, SystemFonts.DefaultFont, Brushes.White, 1800, 0);
            //TO DOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
            //buffer.Graphics.DrawString("Round 1", new Font(FontFamily.GenericSansSerif, 40, FontStyle.Bold), Brushes.RoyalBlue, 850, 100);
            buffer.Render();
        }

        public static void Update()
        {
            //foreach (BaseObject obj in objs)
            //    obj.Update();
            //foreach (Asteroid obj in asteroids)
            //{
            //    obj.Update();
            //    if (obj.Collision(bullet))
            //    {
            //        obj.X = Width;
            //        obj.Y = r.Next(0, Height - 50);
            //        bullet.X = 0;
            //        bullet.Y = r.Next(0, Height-50);
            //        MessageBox.Show("");
            //    }
            //}
            //bullet.Update();
            foreach (BaseObject obj in objs) obj.Update();
            //foreach (Bullet bullet in bullets) bullet.Update();
            for (int i = 0; i < asteroids.Count; i++)
            {
                    asteroids[i].Update();
                    if (ship.Collision(asteroids[i]))
                    {
                        ship.EnergyLow(r.Next(1,10));
                        asteroids[i].X = Width;
                        asteroids[i].Y = r.Next(0, Height - 50);
                        System.Media.SystemSounds.Asterisk.Play();
                        if (ship.Energy <= 0) ship.Die();
                    }
                for (int j = 0; j < bullets.Count && asteroids.Count > 0; j++)
                    if (bullets[j].Collision(asteroids[i]))
                    {
                        //System.Media.SystemSounds.Hand.Play();
                        //TO DOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
                        if (asteroids[i].Power - ship.Power < 0)
                        {
                            ship.Experience += asteroids[i].Power;
                            asteroids[i].Power -= asteroids[i].Power;
                        }
                        else
                        {
                            ship.Experience += ship.Power;
                            asteroids[i].Power -= ship.Power;
                        }
                        if (asteroids[i].Power <= 0)
                        {
                            for (int q = 0; q < 4; q++)
                                shards.Add(new Shard(new Point(asteroids[i].X, asteroids[i].Y), new Point(r.Next(-10, -1), 15), new Size(asteroids[i].powermem/3, asteroids[i].powermem / 3)));
                            asteroids.RemoveAt(i);
                            if (i > 0)
                                i--;
                        }
                        bullets.RemoveAt(j);
                        j--;
                        if (asteroids.Count == 0)
                            ReviveWave();
                    }
                
            }
            for (int i = 0; i < shards.Count; i++)
                shards[i].Update();
            for (int i = 0; i < bullets.Count; i++)
                bullets[i].Update();
            for (int i = 0; i < rockets.Count; i++)
                rockets[i].Update();
        }

        public static void ReviveWave()
        {
            buffer.Graphics.Clear(Color.Black);
            countasteroids++;
            for (int i = 0; i < countasteroids; i++)
            {
                asteroids.Add(new Asteroid(new Point(r.Next(1921,3842), r.Next(0, 1061)), new Point(r.Next(-10, -1), 15), new Size(1, 1)));
                asteroids[i].Size(new Size(asteroids[i].Power, asteroids[i].Power));
            }
        }

        public static void Load()
        {
            r = new Random();
            objs = new BaseObject[320];
            asteroids = new List<Asteroid>();
            shards = new List<Shard>();
            bullets = new List<Bullet>();
            rockets = new List<Rocket>();
            for (int i = 0; i < objs.Length / 2+40; i++)
                objs[i] = new FarStar(new Point(r.Next(0, 1921), r.Next(0, 1081)), new Point(r.Next(-2,0), 0), new Size(2, 2));
            for (int i = objs.Length / 2 + 40; i < objs.Length; i++)
                objs[i] = new Star(new Point(r.Next(0, 1921), r.Next(0, 1081)), new Point(r.Next(-4,-1), 0), new Size(5, 5));
            for (int i = 0; i < countasteroids; i++)
            {
                asteroids.Add(new Asteroid(new Point(r.Next(300, 1921), r.Next(0, 1061)), new Point(r.Next(-10, -1), 15), new Size(1, 1)));
                asteroids[i].Size(new Size(asteroids[i].Power, asteroids[i].Power));
            }
        }

        public static void Finish()
        {
            timer.Stop();
            buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif,60,FontStyle.Underline), Brushes.White, 200, 100);
            buffer.Render();
        }

    }
}
