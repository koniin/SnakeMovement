namespace SnakeMovement
{
    public class Score {
        private int score = 0;
        private readonly int x, y;

        public Score(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public void Increase() {
            score += 1;
        }

        public void Draw(IRenderEngine renderEngine) {
            renderEngine.Draw(x, y, "Score: " + score, Color.Green);
        }
    }
}
