namespace SpaData
{
    public interface IUnitOfWork
    {
        int Complete();
        void Dispose();
    }
}