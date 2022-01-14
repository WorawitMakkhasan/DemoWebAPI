using WebApiDemo.Core.IRepository;

namespace WebApiDemo.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IStudentRepository Student { get; }

        Task CompleteAsync();
    }
}
