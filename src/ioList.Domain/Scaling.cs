namespace ioList.Domain
{
    public class Scaling
    {
        public int PointId { get; private set; }
        public Point Point { get; private set; }
        public float EngineeringMax { get; private set; }
        public float EngineeringMin { get; private set; }
        public float RawMin { get; private set; }
        public float RawMax { get; private set; }

        public override string ToString()
        {
            return $"{EngineeringMin} - {EngineeringMax}";
        }
    }
}