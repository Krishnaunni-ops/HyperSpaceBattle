using System;

namespace HyperspaceCheeseBattleLab7
{
    class Program
    {
        static int[,] board = new int[,]
        {
            { 1, 1, 1, 1, 1, 1, 1, 1 }, // row 0
            { 3, 3, 1, 2, 1, 1, 4, 4 }, // row 1
            { 3, 3, 1, 3, 4, 3, 4, 4 }, // row 2
            { 3, 3, 1, 3, 1, 1, 4, 4 }, // row 3
            { 3, 3, 1, 3, 1, 1, 4, 4 }, // row 4
            { 3, 3, 3, 3, 1, 1, 4, 4 }, // row 5
            { 3, 3, 1, 2, 1, 0, 4, 4 }, // row 6
            { 2, 3, 3, 3, 3, 3, 2, 0 } // row 7
        };

        static Player[] players = new Player[4];

        static int numPlayers;

        static Random random = new Random();

        static int DiceThrow() => 1;

        static void PlayerTurn(int playerNo)
        {
            int diceRoll = DiceThrow();
            int x = players[playerNo].X;
            int y = players[playerNo].Y;

            switch (board[y, x])
            {
                case 1: // Up arrow
                    y += diceRoll;
                    break;
                case 2: // Down arrow
                    y -= diceRoll;
                    break;
                case 3: // Right arrow
                    x += diceRoll;
                    break;
                case 4: // Left arrow
                    x -= diceRoll;
                    break;
            }

            if (x < 0 || x > 7 || y < 0 || y > 7)
            {
                Console.WriteLine($"{players[playerNo].Name} has moved out of bounds and remains in the same position.");
                return;
            }

            while (RocketInSquare(playerNo, x, y))
            {
                switch (board[y, x])
                {
                    case 1: // Up arrow
                        y += 1;
                        break;
                    case 2: // Down arrow
                        y -= 1;
                        break;
                    case 3: // Right arrow
                        x += 1;
                        break;
                    case 4: // Left arrow
                        x -= 1;
                        break;
                }

                if (x < 0 || x > 7 || y < 0 || y > 7)
                {
                    Console.WriteLine($"{players[playerNo].Name} has moved out of bounds and remains in the same position.");
                    return;
                }
            }

            players[playerNo].X = x;
            players[playerNo].Y = y;

            Console.WriteLine($"{players[playerNo].Name} has moved to ({x}, {y}).");
        }

        static bool RocketInSquare(int playerNo, int x, int y)
        {
            for (int i = 0; i < numPlayers; i++)
            {
                if (i == playerNo) continue;
                if (players[i].X == x && players[i].Y == y)
                {
                    return true;
                }
            }
            return false;
        }

        static void ResetGame()
        {
            Console.Write("Enter the number of players (2-4): ");
            numPlayers = int.Parse(Console.ReadLine());

            if (numPlayers < 2 || numPlayers > 4)
            {
                Console.WriteLine("Invalid number of players. Please enter a number between 2 and 4.");
                return;
            }

            for (int i = 0; i < numPlayers; i++)
            {
                players[i] = new Player { X = 0, Y = 0 };
            }

            for (int i = 0; i < numPlayers; i++)
            {
                Console.Write($"Enter player {i + 1} name: ");
                players[i].Name = Console.ReadLine();
            }
        }

        static void Main(string[] args)
        {
            ResetGame();

            for (int i = 0; i < numPlayers; i++)
            {
                PlayerTurn(i);
            }

            Console.ReadKey();
        }
    }

    struct Player
    {
        public string Name;
        public int X;
        public int Y;
    }
}