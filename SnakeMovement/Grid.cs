using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeMovement
{
    public class Grid
    {
        public const int EMPTY_TILE = 0;
        public const int WALL_TILE = 1;
        public const int POWERUP_TILE = 2;
        
        private readonly int[,] grid;
        private readonly Random random = new Random();

        public Grid(int x, int y) {
            grid = new int[x, y];
        }

        public Grid(int[,] grid) {
            this.grid = grid;
        }

        public bool AddRandomPowerUp(List<Vector2D> occupied) {
            List<Vector2D> availablePositions = GetAvailablePositions(occupied);
            if (availablePositions.Count > 0) {
                AddPowerUp(availablePositions);
                return true;
            }
            return false;
        }

        private void AddPowerUp(List<Vector2D> availablePositions) {
            Vector2D pos = availablePositions[random.Next(availablePositions.Count - 1)];
            grid[pos.X, pos.Y] = POWERUP_TILE;
        }

        private List<Vector2D> GetAvailablePositions(List<Vector2D> occupied) {
            List<Vector2D> positions = new List<Vector2D>();
            for (int i = 0; i <= grid.GetUpperBound(0); i++) {
                for (int j = 0; j <= grid.GetUpperBound(1); j++) {
                    if (grid[i, j] == 0 && !occupied.Any(o => o.X == i && o.Y == j))
                        positions.Add(new Vector2D { X = i, Y = j});
                }
            }
            return positions;
        }

        public void Draw(IRenderEngine renderEngine)
        {
            for (int i = 0; i <= grid.GetUpperBound(0); i++) {
                for (int j = 0; j <= grid.GetUpperBound(1); j++) {
                    int value = grid[i, j];
                    DrawTile(value, i, j, renderEngine);
                }
            }
        }

        private void DrawTile(int value, int x, int y, IRenderEngine renderEngine) {
            Color color = Color.White;
            if (value == EMPTY_TILE)
                color = Color.White;
            else if (value == POWERUP_TILE)
                color = Color.Green;
            else if (value == WALL_TILE)
                color = Color.Blue;

            renderEngine.Draw(x, y, value.ToString(), color);
        }

        public void Clear(int x, int y) {
            grid[x, y] = EMPTY_TILE;
        }

        public int Tile(int x, int y) {
            return grid[x, y];
        }
    }
}
