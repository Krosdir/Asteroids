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

        static BaseObject[] objs;
        static Star[] stars;
        static BigStar[] bigStars;

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
            buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in objs)
                obj.Update();
        }

        public static void Load()
        {
            objs = new BaseObject[45];
            for (int i = 0; i < objs.Length/3; i++)
                objs[i] = new BaseObject(new Point(600, i*52), new Point(-i, -i), new Size(10,10));
            for (int i = objs.Length/3; i < objs.Length*2/3; i++)
                objs[i] = new Star(new Point(600, (30-i)*40), new Point(-i,0), new Size(5, 5));
            for (int i = objs.Length*2/3; i < objs.Length; i++)
                objs[i] = new BigStar(new Point(600, (45-i) * 50), new Point((int)(-0.8*i), 15), new Size(12, 12));
        }
    }
}
