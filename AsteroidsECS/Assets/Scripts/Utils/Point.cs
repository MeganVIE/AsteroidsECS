namespace Utils
{
    public struct Point
    {
        public float X;
        public float Y;

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Point operator +(Point first, Point second) => new(first.X + second.X, first.Y + second.Y);
    }
}