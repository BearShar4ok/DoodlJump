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
            
            Menu.Spawn();
            
            while (true)
            {
                Menu.Choose();
            }
        }
    }
    class Button
    {
        int x;
        int y;
        int color;
        int gertc = 10;
        public Button(int color)
        {
            this.color = color;
        }
        public int Gertc
        {
            get { return gertc; }
            set { gertc = value; }
        }
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
        public int Color
        {
            get { return color; }
            set { color = value; }
        }
        public void Set(int value)
        {
            Console.SetCursorPosition(this.x, this.y);
            if (value == 0) 
            {
                Console.ForegroundColor = (ConsoleColor)color;
                if (color == 0)
                {
                    Console.ForegroundColor = (ConsoleColor)15;
                }
                Game.BackGroundColor = (ConsoleColor)color;
                Console.Write((ConsoleColor)color);
            }
            else if (value == 1)
            {
                Console.ForegroundColor = (ConsoleColor)color;
                if (color == 0)
                {
                    Console.ForegroundColor = (ConsoleColor)15;
                }
                Game.ForeGroundColor = (ConsoleColor)color;
                Console.Write((ConsoleColor)color);
            }
            else if (value == 2)
            {
                Console.ForegroundColor = (ConsoleColor)color;
                if (color == 0)
                {
                    Console.ForegroundColor = (ConsoleColor)15;
                }
                Game.player.PlayerColor = (ConsoleColor)color;
                Console.Write((ConsoleColor)color);
            }
            else if (value == 3)
            {
                Console.ForegroundColor = (ConsoleColor)10;
                Console.Write(gertc);
            }
            Console.ResetColor();
        }
        public void Chose(int y, ConsoleKey a)
        {
            if (a == ConsoleKey.LeftArrow)
            {
                if (color == 0)
                {
                    color = 16;
                }
                if (gertc == 1) 
                {
                    gertc = 2;
                }
                gertc--;
                color--;
            }
            else if (a == ConsoleKey.RightArrow)
            {
                if (color == 15)
                {
                    color = -1;
                }
                if (gertc == 100)
                {
                    gertc = 99;
                }
                gertc++;
                color++;
            }
            if (y == 18)
            {
                Set(3);
            }
            else if(y == 16)
            {
                Set(2);
            }
            else if (y == 14)
            {
                Set(1);
            }
            else if (y == 12)
            {
                Set(0);
            }
        }
    }
    static class Menu
    {
        static string[] choise = { " start play ", " settings " };
        static string[] choise2 = { " частота кадров ",
                                    " цвет фона игры ",
                                    " цвет текстур ",
                                    " цвет игрока ", 
                                       " выход "};
        static bool up = false;
        static Button background = new Button(11);
        static Button textureButton = new Button(0);
        static Button player = new Button(4);
        public static Button gerc = new Button(10);

        static int xc = 44;
        static int yc = 12;

        static bool enter1 = false;

        public static bool Up
        {
            set { up = value; }
        }
        public static void SettingsButton()
        {
            background.X = 70;
            background.Y = 12;
            background.Set(0);

            textureButton.X = 70;
            textureButton.Y = 14;
            textureButton.Set(1);

            player.X = 70;
            player.Y = 16;
            player.Set(2);

            gerc.X = 70;
            gerc.Y = 18;
            gerc.Set(3);
        }
        public static void Cursor()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            string texture = "->";
            Console.SetCursorPosition(xc, yc);
            Console.Write(texture);
            ConsoleKey a = Console.ReadKey(true).Key;
            if (a == ConsoleKey.DownArrow && yc != 20)
            {
                yc += 2;
            }
            else if (a == ConsoleKey.UpArrow && yc!=12)
            {
                yc -= 2; 
            }
            else if (yc ==20 && a == ConsoleKey.Enter)
            {
                enter1 = true;
            }
            
            switch (yc)
            {
                case 12:
                    background.Chose(12,a);
                    break;
                case 14:
                    textureButton.Chose(14,a);
                    break;
                case 16:
                    player.Chose(16,a);
                    break;
                case 18:
                    gerc.Chose(18,a);
                    break;
            }
            Console.Clear();
        }
        
        public static void Settings()
        {
            if (yc == 12)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.SetCursorPosition(47, 12);//цвет фона игры
            Console.Write(choise2[1]);
            if (yc == 14)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.SetCursorPosition(47, 14);//цвет текстур
            Console.Write(choise2[2]);
            if (yc == 16)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.SetCursorPosition(47, 16);//цвет игрока
            Console.Write(choise2[3]);
            if (yc == 18)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.SetCursorPosition(47, 18);//частота кадров
            Console.Write(choise2[0]);
            if (yc == 20)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.SetCursorPosition(47, 20);//частота кадров
            Console.Write(choise2[4]);
            SettingsButton();
        }
        public static void Spawn()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(53, 14);
            Console.Write("->");
            Console.SetCursorPosition(55, 14);
            Console.Write(choise[0]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(56, 16);
            Console.Write(choise[1]);
            up = !up;
        }
        public static void Spawn2()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(55, 14);
            Console.Write(choise[0]);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(54, 16);
            Console.Write("->");
            Console.SetCursorPosition(56, 16);
            Console.Write(choise[1]);
            up = !up;
        }
        public static void Choose()
        {
            if (Console.KeyAvailable == true)
            {
                ConsoleKey a = Console.ReadKey(true).Key;
                if (a == ConsoleKey.DownArrow && up)
                {
                    Spawn2();
                }
                else if (a == ConsoleKey.UpArrow && !up)
                {
                    Spawn();
                }
                else if (a == ConsoleKey.Enter && up)
                {
                    Console.BackgroundColor = Game.BackGroundColor;
                    Game.isGameOver = false;
                    Game.Start();
                }
                else if (a == ConsoleKey.Enter && !up)
                {
                    yc = 12;
                    Console.Clear();
                    while (true)
                    {
                        
                        Settings();
                        Cursor();
                        if (enter1)
                        {
                            Spawn();
                            enter1 = !enter1;
                            break;
                        }
                    }
                   
                }
            }
        }
    }
    public static class Game
    {
        //SETTINGS
        public const int hightOfJump = 6;  // высота прыжка
        public static int checkOfHight = 0;

        private static int gerts = Menu.gerc.Gertc;

        private static ConsoleColor backGroundColor = ConsoleColor.Cyan;
        private static ConsoleColor foreGroundColor = ConsoleColor.Black;
        public static ConsoleColor BackGroundColor
        {
            get { return backGroundColor; }
            set { backGroundColor = value; }
        }
        public static ConsoleColor ForeGroundColor
        {
            get { return foreGroundColor; }
            set { foreGroundColor = value; }
        }

        //
        public static bool isGameOver = false;
        static long obshiySchet = 0;

        public static Player player = new Player();
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

            
            player.StartSet(playerX, playerY);
            player.Set();



            //игра. обновление экрана => сама игра
            while (!isGameOver)
            {                                                                                                                       
                Schet();                                                                                                            
                Console.BackgroundColor = BackGroundColor;                                                                          
                Thread.Sleep(gerts);
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
            while (Console.ReadKey(true).Key != ConsoleKey.Enter)
            {

            }
            Console.ResetColor();
            Menu.Spawn();
            Menu.Up = true;
            obshiySchet = 0;
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
            Console.SetCursorPosition(50, 0);
            Console.Write("Ваш счет: " + obshiySchet);
            
            Console.SetCursorPosition(0, 1);
        }
    }


    public class Player
    {
        private int x;
        private int y;
        private ConsoleColor playerColor = ConsoleColor.DarkRed;
        private char texture = '0';

        public ConsoleColor PlayerColor
        {
            get { return playerColor; }
            set { playerColor = value; }
        }
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

        public void StartSet(int x, int y)
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
            Console.BackgroundColor = Game.BackGroundColor;
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
            if (key == ConsoleKey.D || key == ConsoleKey.RightArrow)
            {
                Console.SetCursorPosition(player.X, player.Y);
                Console.ForegroundColor = Game.BackGroundColor;
                Console.Write(" ");
                Console.ForegroundColor = Game.ForeGroundColor;
                player.X++;
                player.Set();
            }
            if (key == ConsoleKey.A || key == ConsoleKey.LeftArrow)
            {
                Console.SetCursorPosition(player.X, player.Y);
                Console.ForegroundColor = Game.BackGroundColor;
                Console.Write(" ");
                Console.ForegroundColor = Game.ForeGroundColor;
                player.X--;
                player.Set();
            }
        }
        public static void jumpA(Player player)
        {
            Console.SetCursorPosition(player.X, player.Y);
            Console.BackgroundColor = Game.BackGroundColor;
            Console.Write(" ");
            Console.BackgroundColor = Game.ForeGroundColor;
            player.Y--;
            player.Set();
        }

        public static void gravitationA(Player player)
        {
            Console.SetCursorPosition(player.X, player.Y);
            Console.BackgroundColor = Game.BackGroundColor;
            Console.Write(" ");
            Console.BackgroundColor = Game.ForeGroundColor;
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
                Console.BackgroundColor = Game.BackGroundColor;
                Console.ForegroundColor = Game.ForeGroundColor;
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
            Console.BackgroundColor = Game.BackGroundColor;
            Console.ForegroundColor = Game.ForeGroundColor;
            this.texture = texturetipe[textureNumber];
            if (textureNumber == 2)
            {
                this.direction = direction;
                
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