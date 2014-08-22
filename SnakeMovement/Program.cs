using System;

namespace SnakeMovement
{
    public class Program
    {
        private static void Main(string[] args) {
            Console.CursorVisible = false;
            SnakeGame game = new SnakeGame(new ConsoleRenderer());
            game.Run();
            Console.ReadKey(true);
        }
    }
}
