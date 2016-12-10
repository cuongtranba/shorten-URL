namespace ServiceInterface
{
    public interface ICreateShortURLService
    {
        string MakeShortURL(int lenght = 5);
    }
}
