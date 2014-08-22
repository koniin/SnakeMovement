namespace SnakeMovement
{
    public enum Color {
        Red, 
        Green,
        Blue,
        White,
        Yellow
    }

    public interface IRenderEngine {
        void Draw(int x, int y, string text, Color color);
    }
}
