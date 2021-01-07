using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DoodleJump
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Game.Start();
            Console.ReadLine();
        }
    }
                    //##############     #####################################
                    //##############     #####################################
                    //###                #####################################
                    //###                #####################################
                    //###        ###     #####################################
                    //###        ###     #####################################
                    //##############     #####################################
                    //##############     #####################################
                    
    static class Game
    {
        //
        public const int hightOfJump = 4;
        //public const int hightOfFall = -9;
        public static int checkOfHight = 0;
        static bool isGameOver = false;
        //
        public static void Start()
        {
            Console.OutputEncoding = Encoding.UTF8;
            //строка 120 символов
            //поле 30 символов
            // U+07DB
            bool flag = false;
            bool isJump = false;

            Random random = new Random();
            Console.Clear();

            int playerX = 0;
            int playerY = 0;

            Plate[] plates = new Plate[15];
            Plate[] dopPlates = new Plate[6];

            // СПАВН НА СТАРТЕ
            for (int i = 0; i < 30; i += 2) // заполнение памяти 15 платформ
            {
                int x = random.Next(4, 24);
                int texture = random.Next(0, 3);
                int y = i;
                int direction = random.Next(0, 2);
                Plate plate = new Plate();
                plate.Spawn(x, y, texture, direction);
                plates[i / 2] = plate;

                if (i == 28)
                {
                    playerX = x;
                    playerY = 27;
                }
            }

            Player player = new Player(playerX, playerY);
            player.Set();

            while (!isGameOver)
            {
                Thread.Sleep(100);
                Console.Clear();
                if (Console.KeyAvailable == true)
                {
                    ConsoleKey a = Console.ReadKey(true).Key;
                    Movement.personalMove(player, a);
                }
                player.Set();
                bool isIntersect = false;
                for (int j = 0; j < plates.Length; j++)
                {
                    if (plates[j].Intersect(player.X, player.Y + 1))
                    {
                        isIntersect = true;
                        break;
                    }
                }
                if (isIntersect && isJump == false)
                {
                    Movement.jumpA(player);
                    checkOfHight++;
                    if (checkOfHight >= hightOfJump)
                    {
                        checkOfHight = -1;
                        isJump = !isJump;
                    }
                }
                else if (checkOfHight > 0)
                {
                    Movement.jumpA(player);
                    checkOfHight++;
                    if (checkOfHight >= hightOfJump)
                    {
                        checkOfHight = -1;
                        isJump = !isJump;
                    }
                }
                else if (checkOfHight < 0 || !isIntersect)
                {
                    if (isIntersect)
                    {
                        Movement.jumpA(player);
                        checkOfHight = 1;
                        isJump = false;
                    }
                    Movement.gravitationA(player);
                    checkOfHight--;

                }
                // передвижение платформ
                flag = !flag;
                for (int i = 0; i < 15; i++)
                {
                    if (flag)
                    {
                        //plates[i].Move();
                    }
                    plates[i].MoveXPlate();
                    plates[i].Draw();
                    //plates[i].Intersect();
                }
                if (flag)
                {
                    //As(plates);
                }
            }
            GameOver();
        }
        public static void GameOver()
        {
            isGameOver = !isGameOver;
            Console.Clear();
            Console.SetCursorPosition(15, 15);
            Console.Write("GAME OVER");
        }
        static void As(Plate[] plates)
        {
            Random random = new Random();
            int x = random.Next(4, 24);
            int texture = random.Next(0, 3);
            int direction = random.Next(0, 2);
            int y = 0;
            for (int i = 14; i > 0; i--)
            {
                plates[i] = plates[i - 1];
            }
            Plate plate = new Plate();
            plate.Spawn(x, y, texture, direction);
            plate.Draw();
            plates[0] = plate;
        }
    }


    class Player
    {
        private int x;
        private int y;

        private char texture = '0';

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public char T
        {
            get { return texture; }
        }

        public Player(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Set()
        {
            if (x == -1)
            {
                x = 30;
            }
            else if (x == 31)
            {
                x = 0;
            }
            if (y == 32)
            {
                Game.GameOver();
            }
            Console.SetCursorPosition(x, y);
            Console.Write(texture);
        }
    }

    static class Movement
    {
        public static void personalMove(Player player, ConsoleKey key)
        {
            if (key == ConsoleKey.D)
            {
                Console.SetCursorPosition(player.X, player.Y);
                Console.Write(" ");
                player.X++;
                player.Set();
            }
            if (key == ConsoleKey.A)
            {
                Console.SetCursorPosition(player.X, player.Y);
                Console.Write(" ");
                player.X--;
                Console.SetCursorPosition(player.X + 2, player.Y);
                Console.Write(" ");
                player.Set();
            }
        }
        public static void jumpA(Player player)
        {
            Console.SetCursorPosition(player.X, player.Y);
            Console.Write(" ");
            player.Y--;
            player.Set();
        }

        public static void gravitationA(Player player)
        {
            Console.SetCursorPosition(player.X, player.Y);
            Console.Write(" ");
            player.Y++;
            player.Set();
        }
        public static void jumpB(Plate[] plates)
        {
            Random random = new Random();
            int x = random.Next(4, 24);
            int texture = random.Next(0, 3);
            // int y = 0;
            for (int i = 14; i > 0; i--)
            {
                plates[i] = plates[i - 1];
            }
            Plate plate = new Plate();
            //plate.Spawn(x, y, texture);
            plate.Draw();
            plates[0] = plate;
        }

        public static void gravitationB(Plate[] plates)
        {
            Random random = new Random();
            int x = random.Next(4, 24);
            int texture = random.Next(0, 3);
            //int y = 0;
            for (int i = 14; i > 0; i--)
            {
                plates[i] = plates[i - 1];
            }
            Plate plate = new Plate();
            //plate.Spawn(x, y, texture);
            plate.Draw();
            plates[0] = plate;
        }
    }

    class Plate
    {
        //U+07DB
        public string[] texturetipe = { "===", "---", "ߛߛߛ" };
        int x;
        int y;
        string texture;
        bool isBreak;
        int direction;
        int schet;

        public int X
        {
            get { return x; }
        }
        public int Y
        {
            get { return y; }
        }

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
        public void Spawn(int x, int y, int textureNumber, int direction)
        {
            //Random random = new Random();
            this.x = x;
            this.y = y;
            this.isBreak = false;
            Console.SetCursorPosition(this.x, this.y);
            this.texture = texturetipe[textureNumber];
            if (textureNumber == 2)
            {
                this.direction = direction;
                System.Diagnostics.Debug.WriteLine(this.direction);
                if (this.direction == 0)
                {
                    this.direction = -1;
                }
                else
                {
                    this.direction = 1;
                }
                this.schet = this.x;
            }
        }
        public bool Intersect(int x, int y)
        {
            if (isBreak == false)//платформа сломана?
            {
                if ((this.x == x || this.x + 1 == x || this.x + 2 == x) && this.y == y)//пересечение игрока с платформой
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
            if (texture == texturetipe[2])
            {
                this.x += this.direction;
                this.schet += this.direction;
                if (this.schet >= 27 || this.schet <= 1)
                {
                    this.direction *= -1;
                }
            }
        }
        public void Move()
        {
            this.y += 2;

        }
    }
}