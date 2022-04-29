namespace ioList.Services
{
    public interface IListServiceProvider
    {
        public IListService Connect(string listPath);
    }
}