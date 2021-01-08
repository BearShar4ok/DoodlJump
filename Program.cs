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
            Console.BackgroundColor = Game.backGroundColor;
            Game.Start();
            Console.ReadLine();
        }
    }
    public static class Game
    {
        //SETTINGS
        public const int hightOfJump = 6;  // высота прыжка
        public static int checkOfHight = 0;
        public const ConsoleColor backGroundColor = ConsoleColor.Cyan;
        public const ConsoleColor foreGroundColor = ConsoleColor.Black;
        
        //
        static bool isGameOver = false;
        static long obshiySchet = 0;
        //
        public static void Start()
        {
            Console.OutputEncoding = Encoding.UTF8;

            bool isFildJump;

            Random random = new Random();
            Console.Clear();

            int playerX = 0;
            int playerY = 0;

            Plate[] plates = new Plate[15];
            Plate[] dopPlates = new Plate[6];

            // СПАВН НА СТАРТЕ
            for (int i = 0; i < 30; i += 2) // заполнение памяти 15 платформ
            {
                int x = random.Next(4, 14);
                int texture = random.Next(0, 11);
                if (texture >=0 && texture <= 5)
                {
                    texture = 0; //===
                }
                else if (texture >= 6 && texture <= 8)
                {
                    texture = 2;//ߛߛߛ
                }
                else
                {             
                    texture = 1;//---   
                }
                int y = i;
                int direction = random.Next(0, 2);
                Plate plate = new Plate();
                plate.Spawn(x, y, texture, direction);
                plates[i / 2] = plate;

                if (i == 24)
                {
                    playerX = x + 1;
                    playerY = 23;
                }
            }

            Player player = new Player(playerX, playerY);
            player.Set();



            //игра. обновление экрана => сама игра
            while (!isGameOver)
            {
                Schet();
                Console.BackgroundColor = backGroundColor;
                Thread.Sleep(50);
                Console.Clear();
                if (Console.KeyAvailable == true)
                {
                    ConsoleKey a = Console.ReadKey(true).Key;
                    Movement.personalMove(player, a);
                }
                bool isIntersect = false;
                for (int j = 0; j < plates.Length; j++)
                {
                    if (plates[j].Intersect(player.X, player.Y + 1, checkOfHight))
                    {
                        isIntersect = true;
                        break;
                    }
                }
                // двигать экран или игрока
                if (player.Y <= 15) 
                {
                    isFildJump = true;
                }
                else
                {
                    isFildJump = false;
                }
                // само перемещение экрана или игрока
                if (!isFildJump)//перемещение игрока
                {
                    Movement.Physic(player, isIntersect, ref checkOfHight, hightOfJump);
                }
                else if (isFildJump)//перемещение экрана
                {
                    obshiySchet+=2;
                    for (int i = 0; i < 15; i++)
                    {
                        plates[i].Move();
                    }
                    Movement.jumpB(plates);
                    Movement.gravitationA(player);
                }
                // передвижение платформ
                
                for (int i = 0; i < 15; i++)
                {
                    
                    plates[i].MoveXPlate();
                    DrawBorder();
                    plates[i].Draw();
                    
                }
                
                isFildJump = false;
            }
            GameOver();
        }
        static void DrawBorder()
        {
            for (int i = 0; i < 30; i++)
            {
                Console.SetCursorPosition(16, i);
                Console.Write("|");
                
            }
        }
        static void Schet()
        {
            Console.SetCursorPosition(30, 0);
            Console.Write("Ваш счет: "+obshiySchet);
        }
        public static void GameOver()
        {
            isGameOver = true;
            Console.Clear();
            Console.SetCursorPosition(37, 7);
            Console.Write(" #####      ###    #     #  #######" + "\n"
    + "                                     ##   ##    ## ##   ##   ##  ##     " + "\n"
    + "                                     ##        ##   ##  ### ###  ##     " + "\n"
    + "                                     ##  ###   ##   ##  #######  ######" + "\n"
    + "                                     ##   ##   #######  ## # ##  ##     " + "\n"
    + "                                     ##   ##   ##   ##  ##   ##  ##     " + "\n"
    + "                                      #####    ##   ##  ##   ##  #######" + "\n" + "\n"
    + "                                      #####    ##   ##  #######  ###### " + "\n"
    + "                                     ##   ##   ##   ##  ##       ##   ##" + "\n"
    + "                                     ##   ##   ##   ##  ##       ##   ##" + "\n"
    + "                                     ##   ##   ##   ##  ######   ######" + "\n"
    + "                                     ##   ##   ##   ##  ##       ## ##" + "\n"
    + "                                     ##   ##    ## ##   ##       ##  ##" + "\n"
    + "                                      #####      ###    #######  ##   ##");
            Schet();
        }
    }


    class Player
    {
        private int x;
        private int y;
        private ConsoleColor playerColor = ConsoleColor.Red;
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
                x = 15;
            }
            else if (x == 16)
            {
                x = 0;
            }
            if (y == 31)
            {
                Game.GameOver();
            }
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = playerColor;
            Console.BackgroundColor = Game.backGroundColor;
            Console.Write(texture);
            Console.ResetColor();
        }
    }

    static class Movement
    {
        public static void Physic(Player player, bool isIntersect, ref int checkOfHight, int hightOfJump)
        {
            if (isIntersect && checkOfHight < 0)
            {
                checkOfHight++;
                jumpA(player);
            }
            else if (checkOfHight >= 0)
            {
                checkOfHight++;
                jumpA(player);
            
                if (checkOfHight >= hightOfJump)
                {
                    checkOfHight = -1;
                }
            }
            else
            {
                gravitationA(player);
            }
        }
        public static void personalMove(Player player, ConsoleKey key)
        {
            if (key == ConsoleKey.D)
            {
                Console.SetCursorPosition(player.X, player.Y);
                Console.ForegroundColor = Game.backGroundColor;
                Console.Write(" ");
                Console.ForegroundColor = Game.foreGroundColor;
                player.X++;
                player.Set();
            }
            if (key == ConsoleKey.A)
            {
                Console.SetCursorPosition(player.X, player.Y);
                Console.ForegroundColor = Game.backGroundColor;
                Console.Write(" ");
                Console.ForegroundColor = Game.foreGroundColor;
                player.X--;
                player.Set();
            }
        }
        public static void jumpA(Player player)
        {
            Console.SetCursorPosition(player.X, player.Y);
            Console.BackgroundColor = Game.backGroundColor;
            Console.Write(" ");
            Console.BackgroundColor = Game.foreGroundColor;
            player.Y--;
            player.Set();
        }

        public static void gravitationA(Player player)
        {
            Console.SetCursorPosition(player.X, player.Y);
            Console.BackgroundColor = Game.backGroundColor;
            Console.Write(" ");
            Console.BackgroundColor = Game.foreGroundColor;
            player.Y++;
            player.Set();
        }
        public static void jumpB(Plate[] plates)//2
        {
            Random random = new Random();
            int x = random.Next(4, 14);
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

    class Plate
    {
        //U+07DB
        public string[] texturetipe = { "===", "‾‾‾", "ߛߛߛ" };
        int x;
        int y;
        int n = 0;
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

      
        public void Draw()
        {
            if (isBreak == false)
            {
                Console.SetCursorPosition(this.x, this.y);
                Console.BackgroundColor = Game.backGroundColor;
                Console.ForegroundColor = Game.foreGroundColor;
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
            Console.BackgroundColor = Game.backGroundColor;
            Console.ForegroundColor = Game.foreGroundColor;
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
        public bool Intersect(int x, int y, int chechOfHight)
        {
            if (texturetipe[1] == texture && (this.x == x || this.x + 1 == x || this.x + 2 == x) && this.y == y && chechOfHight < 0)
            {
                isBreak = true;
            }
            if (isBreak == false)//платформа сломана?
            {
                if ((this.x == x || this.x + 1 == x || this.x + 2 == x) && this.y == y) //пересечение игрока с платформой
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
                if (this.schet >= 12 || this.schet <= 1)
                {
                    if (this.schet > 12)
                    {
                        this.schet = 12;
                    }
                    if (this.schet < 1) 
                    { 
                        this.schet = 1;
                    }
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