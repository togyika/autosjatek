using System;
using System.Threading;
class Program
    
{
    const int LaneWidth = 10;
    const int Lanes = 3;
    const int Height = 20;

    static int playerLane;
    static int roadOffset;

    static int FieldWidth => Lanes * (LaneWidth + 1) + 1;
    static int OffsetX => (Console.WindowWidth - FieldWidth) / 2;
    static int OffsetY => (Console.WindowHeight - Height) / 2;

    static void Main()
    {
        Console.CursorVisible = false;
        playerLane = 1;
        while (true)
        {
            Input();
            Update();
            Draw();
            Thread.Sleep(120);
        }
    }
    static void Input()
    {
        if (!Console.KeyAvailable) return;

        var key = Console.ReadKey(true).Key;

        if (key == ConsoleKey.LeftArrow && playerLane > 0)
            playerLane--;
        if (key == ConsoleKey.A && playerLane > 0)
            playerLane--;
        if (key == ConsoleKey.D && playerLane < 2)
            playerLane++;
        if (key == ConsoleKey.RightArrow && playerLane < Lanes - 1)
            playerLane++;
    }
    static void Update()
    { roadOffset++; }
    static void Draw()
    {
        Console.Clear();

        for (int y = 0; y < Height; y++)
        {
            Console.SetCursorPosition(OffsetX, OffsetY + y);

            for (int lane = 0; lane < Lanes; lane++)
            {
                Console.Write("|");

                string cell = new string(' ', LaneWidth);

                if ((y + roadOffset) % 4 == 0)
                {
                    cell = "    ||    ";
                }

                if (lane == playerLane && y >= Height - 3)
                {
                    int part = y - (Height - 3);
                    cell = part switch
                    {
                        0 => " |------| ",
                        1 => " |______| ",
                        2 => " |_|  |_| ",   
                        _ => cell
                    };
                }

                Console.Write(cell);
            }

            Console.WriteLine("|");
        }
    }
}
