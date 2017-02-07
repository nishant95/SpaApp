using SpaData.Models;
using SpaData.Repository;

namespace SpaData
{
    public interface IUnitOfWork
    {
        int Complete();
        void Dispose();
    }
}