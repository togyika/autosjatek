using System;
using System.Threading;

class Program
{
    const int LaneWidth = 10;
    const int Lanes = 3;
    const int Height = 20;

    static int playerLane;
    static int roadOffset;
    static int score = 0;
    static int scoreCounter = 0;
    static int carOffset = 0;

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

        if ((key == ConsoleKey.LeftArrow || key == ConsoleKey.A) && playerLane > 0)
        {
            playerLane--;
            carOffset = -2;
        }

        if ((key == ConsoleKey.RightArrow || key == ConsoleKey.D) && playerLane < Lanes - 1)
        {
            playerLane++;
            carOffset = 2;
        }
    }

    static void Update()
    {
        roadOffset++;

        scoreCounter++;
        if (scoreCounter >= 5)
        {
            score++;
            scoreCounter = 0;
        }

        if (carOffset > 0) carOffset--;
        if (carOffset < 0) carOffset++;
    }

    static void Draw()
    {
        Console.Clear();

        Console.SetCursorPosition(0, 0);
        Console.Write($"Score: {score}");

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
                        0 => new string(' ', Math.Abs(carOffset)) + "|------|",
                        1 => new string(' ', Math.Abs(carOffset)) + "|______|",
                        2 => new string(' ', Math.Abs(carOffset)) + "|_|  |_|",
                        _ => cell
                    };
                }

                Console.Write(cell.PadRight(LaneWidth));
            }

            Console.WriteLine("|");
        }
    }
}
