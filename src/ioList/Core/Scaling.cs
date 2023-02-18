namespace ioList.Core
{
    public class Scaling
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public float EngineeringMax { get; set; }
        public float EngineeringMin { get; set; }
        public float RawMin { get; set; }
        public float RawMax { get; set; }

        public override string ToString()
        {
            return $"{EngineeringMin} - {EngineeringMax}";
        }
    }
}