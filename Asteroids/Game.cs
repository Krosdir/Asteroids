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

        static Ship ship = new Ship(new Point(10,400), new Point(5,9), new Size(75,75));
        static BaseObject[] objs;
        static List<Asteroid> asteroids;
        static List<Bullet> bullets;

        static Random r;
        static Timer timer;
        static Timer timershoot = new Timer();
        

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
        }

        private static void Timershoot_Tick(object sender, EventArgs e)
        {
            if (Ship.isCtrlHolded)
            {
                bullets.Add(new Bullet(new Point(ship.Rect.Width + 15, ship.Rect.Y + 30), new Point(5, 0), new Size(16, 16)));
                Bullet.bulletcount++;
            }
        }

        private static void Form_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) Ship.isCtrlHolded = false;
            if (e.KeyCode == Keys.Up) Ship.isUpHolded = false;
            if (e.KeyCode == Keys.Down) Ship.isDownHolded = false;
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
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
            
            if (Ship.isUpHolded) ship.Up();
            if (Ship.isDownHolded) ship.Down();
        }

        public static void Draw()
        {
            //Проверяем вывод графики
            buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objs)
                obj.Draw();
            foreach (Asteroid obj in asteroids)
                obj.Draw();
            foreach (Bullet bullet in bullets)
                bullet.Draw();
            ship.Draw();
            buffer.Graphics.DrawString("Energy: " + ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            buffer.Graphics.DrawString("Count of bullet: " + Bullet.bulletcount, SystemFonts.DefaultFont, Brushes.White, 0, 15);
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
                    for (int j=0; j<bullets.Count; j++)
                    if (asteroids[i].Collision(bullets[j]))
                    {
                        //System.Media.SystemSounds.Hand.Play();
                        asteroids[i].Power -= ship.Power;
                        if (asteroids[i].Power<=0)
                        {
                            //asteroids.RemoveAt(i);
                            //asteroids.Add(new Asteroid(new Point(1930, r.Next(0, 1081)), new Point(r.Next(-10, -1), 15), new Size(1, 1)));
                            //asteroids[i].Size(new Size(asteroids[i].Power, asteroids[i].Power));
                            //asteroids[i].Draw();
                            asteroids[i].X = Width;
                            asteroids[i].Y = r.Next(0, Height - 50);
                        }

                        bullets.RemoveAt(j);
                            j--;
                    }
                    if (ship.Collision(asteroids[i]))
                    {
                        ship.EnergyLow(r.Next(1,10));
                        asteroids[i].X = Width;
                        asteroids[i].Y = r.Next(0, Height - 50);
                        System.Media.SystemSounds.Asterisk.Play();
                        if (ship.Energy <= 0) ship.Die();
                    }
            }
            for (int i = 0; i < bullets.Count; i++)
                bullets[i].Update();
        }

        public static void Load()
        {
            r = new Random();
            objs = new BaseObject[320];
            asteroids = new List<Asteroid>(9);
            bullets = new List<Bullet>();
            for (int i = 0; i < objs.Length / 2+40; i++)
                objs[i] = new FarStar(new Point(r.Next(0, 1921), r.Next(0, 1081)), new Point(r.Next(-2,0), 0), new Size(2, 2));
            for (int i = objs.Length / 2 + 40; i < objs.Length; i++)
                objs[i] = new Star(new Point(r.Next(0, 1921), r.Next(0, 1081)), new Point(r.Next(-4,-1), 0), new Size(5, 5));
            for (int i = 0; i < 20; i++)
            {
                asteroids.Add(new Asteroid(new Point(r.Next(0, 1921), r.Next(0, 1081)), new Point(r.Next(-10, -1), 15), new Size(1, 1)));
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
