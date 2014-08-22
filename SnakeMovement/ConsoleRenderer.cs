using System;
using System.Collections.Generic;

namespace SnakeMovement
{
    public class ConsoleRenderer : IRenderEngine {
        private readonly Dictionary<Color, ConsoleColor> colorMapping = new Dictionary<Color, ConsoleColor> {
            {Color.Blue, ConsoleColor.Blue},
            {Color.Green, ConsoleColor.Green},
            {Color.White, ConsoleColor.White},
            {Color.Yellow, ConsoleColor.Yellow},
            {Color.Red, ConsoleColor.Red}
        };

        public void Draw(int x, int y, string text, Color color) {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = colorMapping[color];
            Console.Write(text);
        }
    }
}
