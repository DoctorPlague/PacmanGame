namespace Base
{
    public class TheGame
    {
        private char[,] map;
        public TheGame(string mapPath)
        {
            this.map = TxtToCharArray(mapPath);
        }


        public void Start()
        {
            Console.CursorVisible = false;
            int score = 0;

            int maxScore = ToDot();

            Pacman dog = new()
            {
                x = 24,
                y = 5
            };

            Console.WriteLine("Press any key");

            while (true)
            {

                Move(dog, ref score);
                Console.SetCursorPosition(50, 0);
                Console.WriteLine($"Score:" + score);
                if (score == maxScore)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(50, 15);
                    Console.Write("Congratulations!!");
                    break;
                }
            }
        }

        private void DrawMap()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        private int ToDot()
        {
            int dotCount = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == ' ')
                    {
                        map[i, j] = '.';
                        dotCount++;
                    }
                }
            }
            return dotCount;
        }

        private void EatDot(Pacman dog, ref int score)
        {
            score++;
            map[dog.y, dog.x] = ' ';
        }

        private void Move(Pacman dog, ref int score)
        {
            ConsoleKeyInfo pressedKey = Console.ReadKey();
            bool IsDot;
            switch (pressedKey.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (Check(dog, -1, 0, out IsDot))
                    {
                        dog.x--;

                        if (IsDot == true)
                            EatDot(dog, ref score);
                    }
                    break;

                case ConsoleKey.UpArrow:
                    if (Check(dog, 0, -1, out IsDot))
                    {
                        dog.y--;

                        if (IsDot == true)
                            EatDot(dog, ref score);
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (Check(dog, 0, 1, out IsDot))
                    {
                        dog.y++;

                        if (IsDot == true)
                            EatDot(dog, ref score);
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (Check(dog, 1, 0, out IsDot))
                    {
                        dog.x++;
                        if (IsDot == true)
                            EatDot(dog, ref score);
                    }
                    break;
            }
            Console.Clear();
            DrawMap();
            Console.SetCursorPosition(dog.x, dog.y);
            Console.Write('@');
        }

        public bool Check(Pacman dog, int x, int y, out bool IsDot)
        {
            IsDot = false;
            if (map[dog.y + y, dog.x + x] == ' ' || map[dog.y + y, dog.x + x] == '.')
            {

                if (map[dog.y + y, dog.x + x] == '.')
                {
                    IsDot = true;
                }
                return true;
            }
            return false;
        }

        static char[,] TxtToCharArray(string path)
        {
            string[] stringBystring = File.ReadAllLines(path);

            char[,] map = new char[stringBystring.Length, stringBystring[0].Length];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = stringBystring[i][j];
                }
            }
            return map;
        }
    }
}
