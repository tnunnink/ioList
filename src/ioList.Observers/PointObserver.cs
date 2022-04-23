using EntityObserver;
using ioList.Domain;

namespace ioList.Observers
{
    public class PointObserver : Observer<Point>
    {
        public PointObserver() : base(new Point())
        {
        }
        
        public PointObserver(Point model) : base(model)
        {
        }
    }
}