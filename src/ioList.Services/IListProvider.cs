namespace ioList.Services
{
    public interface IListProvider
    {
        public IListService Connect(string listPath);
    }
}