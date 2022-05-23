namespace ioList.Services
{
    public interface IListProvider
    {
        public IListRepository Connect(string listPath);
    }
}