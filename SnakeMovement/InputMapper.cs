using System;
using System.Collections.Generic;

namespace SnakeMovement
{
    public enum Input
    {
        Exit,
        Up,
        Down,
        Left,
        Right,
        None
    }

    public class InputMapper {
        private Dictionary<ConsoleKey, Input> inputMap = new Dictionary<ConsoleKey, Input> {
            {ConsoleKey.A, Input.Left},
            {ConsoleKey.D, Input.Right},
            {ConsoleKey.W, Input.Up},
            {ConsoleKey.S, Input.Down}
        };

        public Input Map(ConsoleKey consoleKey) {
            if (inputMap.ContainsKey(consoleKey))
                return inputMap[consoleKey];
            return Input.None;
        }

        public void SetMapping(Input input, ConsoleKey consoleKey) {
            inputMap[consoleKey] = input;
        }
    }
}
