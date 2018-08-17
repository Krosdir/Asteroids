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

        static BaseObject[] objs;
        static Asteroid[] asteroids;
        static Bullet bullet;

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
            Load();
            //Связываем буфер в памяти с графическим объектом
            //Для того чтобы рисовать в буфере
            buffer = context.Allocate(grx, new Rectangle(0,0,Width,Height));
            Timer timer = new Timer();
            timer.Interval = 35;
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            //Проверяем вывод графики
            buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objs)
                obj.Draw();
            foreach (Asteroid obj in asteroids)
                obj.Draw();
            bullet.Draw();
            buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in objs)
                obj.Update();
            foreach (Asteroid obj in asteroids)
            {
                obj.Update();
                if (obj.Collision(bullet))
                {
                    MessageBox.Show("");
                }
            }
            bullet.Update();
        }

        public static void Load()
        {
            Random r = new Random();
            objs = new BaseObject[80];
            asteroids = new Asteroid[9];
            bullet = new Bullet(new Point(0,200), new Point(5,0), new Size(8,2));
            for (int i = 0; i < objs.Length / 2+10; i++)
                objs[i] = new FarStar(new Point(r.Next(0, 801), r.Next(0, 601)), new Point(r.Next((int)-2.5,(int)-0.1), 0), new Size(2, 2));
            for (int i = objs.Length / 2 + 10; i < objs.Length; i++)
                objs[i] = new Star(new Point(r.Next(0, 801), r.Next(0, 601)), new Point((int)(-i * 0.05), 0), new Size(5, 5));
            for (int i = 0; i < asteroids.Length; i++)
                asteroids[i] = new Asteroid(new Point(r.Next(0, 801), r.Next(0, 601)), new Point(r.Next(-10,-1), 15), new Size(r.Next(2,25), r.Next(2, 25)));
        }
    }
}
