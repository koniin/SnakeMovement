using System;
using System.Threading;

namespace SnakeMovement
{
    public class SnakeGame
    {
        private readonly Grid grid;
        private readonly Snake snake;
        private readonly CollisionManager collisionManager;
        private int gameState = 1;
        private readonly InputMapper inputMapper;
        private readonly IRenderEngine renderEngine;

        public SnakeGame(IRenderEngine renderEngine)
        {
            this.renderEngine = renderEngine;
            collisionManager = new CollisionManager();
            inputMapper = new InputMapper();
            gameState = 1;
            grid = new Grid(new int[,] {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, 
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } 
            });
            snake = new Snake(4, 4, 4, 1, 0);

            grid.AddRandomPowerUp(snake.GetPositions());
        }

        public void Run()
        {
            grid.Draw(renderEngine);
            snake.Draw(renderEngine);

            while (gameState == 1) {
                Input input = GetInput();
                if (input == Input.Exit)
                    gameState = 0;

                HandleInput(snake, input);
                snake.Upate();

                HandleCollisions();

                grid.Draw(renderEngine);
                snake.Draw(renderEngine);
            }

            DrawGameOver();
        }

        private void HandleCollisions() {
            CollisionType collision = collisionManager.Collision(grid, snake);
            if (collision == CollisionType.PowerUp) {
                snake.AddTail();
                grid.Clear(snake.X, snake.Y);
                grid.AddRandomPowerUp(snake.GetPositions());
            }

            if (collision == CollisionType.Fatal)
                gameState = 0;
        }

        private void HandleInput(Snake s, Input input)
        {
            if (input == Input.Right)
                s.ChangeDirection(1, 0);
            if (input == Input.Left)
                s.ChangeDirection(-1, 0);
            if (input == Input.Up)
                s.ChangeDirection(0, -1);
            if (input == Input.Down)
                s.ChangeDirection(0, 1);
        }

        private Input GetInput()
        {
            while (Console.KeyAvailable == false)
                Thread.Sleep(250);
            ConsoleKeyInfo cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.Q)
                return Input.Exit;

            return inputMapper.Map(cki.Key);
        }

        private void DrawGameOver() {
            renderEngine.Draw(3, 4, "Game Over.", Color.Yellow);
            renderEngine.Draw(3, 5, "Press Enter to exit", Color.Yellow);
        }
    }
}
