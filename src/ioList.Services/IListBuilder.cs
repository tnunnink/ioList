using ioList.Domain;

namespace ioList.Services
{
    public interface IListBuilder
    {
        public bool Build(ListInfo info);
    }
}