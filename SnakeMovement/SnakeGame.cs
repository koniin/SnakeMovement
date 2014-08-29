using System;
using System.Threading;

namespace SnakeMovement
{
    public class SnakeGame
    {
        private readonly Grid grid;
        private readonly Snake snake;
        private readonly Score score;
        private readonly CollisionManager collisionManager;
        private int gameState = 1;
        private readonly InputMapper inputMapper;
        private readonly IRenderEngine renderEngine;

        public SnakeGame(IRenderEngine renderEngine)
        {
            this.renderEngine = renderEngine;
            collisionManager = new CollisionManager();
            inputMapper = new InputMapper();
            score = new Score(0, 12);
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

        public void Run() {
            Draw(); // because the way input is handled it needs to be drawn first
            GameLoop();
            DrawGameOver();
        }
		
		private void GameLoop() {
			while (gameState == 1) {
                HandleInput();
                Update();
                HandleCollisions();
                Draw();
            }
		}

        private void Update() {
            snake.Upate();
        }

        private void Draw() {
            grid.Draw(renderEngine);
            snake.Draw(renderEngine);
            score.Draw(renderEngine);
        }

        private void HandleCollisions() {
            CollisionType collision = collisionManager.Collision(grid, snake);
            if (collision == CollisionType.PowerUp) {
                snake.AddTail();
                grid.Clear(snake.X, snake.Y);
                grid.AddRandomPowerUp(snake.GetPositions());
                score.Increase();
            }

            if (collision == CollisionType.Fatal)
                gameState = 0;
        }

        private void HandleInput()
        {
            Input input = GetInput();
            
            if (input == Input.Exit)
                gameState = 0;
            if (input == Input.Right)
                snake.ChangeDirection(1, 0);
            if (input == Input.Left)
                snake.ChangeDirection(-1, 0);
            if (input == Input.Up)
                snake.ChangeDirection(0, -1);
            if (input == Input.Down)
                snake.ChangeDirection(0, 1);
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
