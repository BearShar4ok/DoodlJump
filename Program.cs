using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DoodlJamp
{
    
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            //строка 120 символов
            //поле 30 символов
            // U+07DB
            bool flag = false;
            Random random = new Random();
            Console.Clear();
            Plate[] plates = new Plate[15];
            for (int i = 0; i < 30; i+=2) // заполнение памяти 15 платформ
            {
                int x = random.Next(4, 24);
                int texture = random.Next(0, 3);
                int y = i;
                Plate plate = new Plate();
                plate.Spawn(x,y,texture);
                plates[i/2] = plate; 
            }
           
            while (true)
            {
                Thread.Sleep(1000);
                Console.Clear();
                flag = !flag;
                for (int i = 0; i < 15; i++)
                {
                    if (flag)
                    {
                        plates[i].Move();
                    }
                    plates[i].MoveXPlate();
                    plates[i].Draw();
                    //plates[i].Intersect();
                }
                if (flag)
                {
                    As(plates);
                }
            }
        }
        static void As(Plate[] plates)
        {
            Random random = new Random();
            int x = random.Next(4, 24);
            int texture = random.Next(0, 3);
            int y = 0;
            for (int i = 14; i > 0; i--)
            {
                plates[i] = plates[i - 1];
            }
            Plate plate = new Plate();
            plate.Spawn(x, y, texture);
            plate.Draw();
            plates[0] = plate;
        }
    }
    class Plate
    {
        //U+07DB
        public string[] texturetipe = { "==", "--", "ߛߛ" };
        int x;
        int y;
        string texture;
        bool isBreak;
        int direction;
        int schet;
        public void MovePossition()
        {

        }
        public void Draw()
        {
            if (isBreak == false)
            {
                Console.SetCursorPosition(this.x, this.y);
                Console.Write(texture);
            }
        }
        public void Spawn(int x, int y, int textureNumber)
        {
            Random random = new Random();
            this.x = x;
            this.y = y;
            Console.SetCursorPosition(this.x, this.y);
            this.texture = texturetipe[textureNumber];
            if (textureNumber == 1)
            {
                this.isBreak = false;
            }
            else if (textureNumber == 2)
            {
                this.direction = random.Next(0, 2);
                if (this.direction == 0)
                {
                    this.direction = -2;
                }
                else
                {
                    this.direction = 2;
                }
                this.schet = 0;
            }
        }
        public bool Intersect()
        {
            if (isBreak == false)//платформа сломана?
            {
                if (false)//пересечение игрока с платформой
                {
                    return true;
                }
            }
            return false;
        }
        public void Break()
        {
            this.isBreak = true;
        }
        public void MoveXPlate()
        {
            if (texture == "ߛߛ")
            {
                this.x += this.direction;
                this.schet += this.direction;
                if (this.schet == 4 || this.schet == -4)
                {
                    this.direction *= -1;
                }
            }
        }
        public void Move()
        {
            this.y+=2;
            
        }
    }
}
