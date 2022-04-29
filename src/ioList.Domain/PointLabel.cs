namespace ioList.Domain
{
    public class PointLabel
    {
        private PointLabel()
        {
        }
        
        public PointLabel(Point point, Label label)
        {
            PointId = point.Id;
            Point = point;
            LabelId = label.Id;
            Label = label;
        }
        public int PointId { get; private set; }
        public Point Point { get; private set; }
        public int LabelId { get; private set; }
        public Label Label { get; private set; }
    }
}