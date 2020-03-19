namespace SharedFiles
{
    public class GetResponse
    {
        public Directions Direction;
        public string Message;
        public bool Scent;
        public int X;
        public int Y;

        public enum Directions
        {
            //Clockwise
            North = 0,
            East = 1,
            South = 2,
            West = 3,
        }

        public enum Turns
        {
            L, // left
            R, // right
            F, // forward
        }

        public override string ToString()
        {
            return $"X:{X}, Y:{Y}, Direction:{Direction}, Scent:{Scent}, Message:{Message}";
        }
    }
}