using WebApiDemo.Core.IConfiguration;
using WebApiDemo.Core.IRepository;
using WebApiDemo.Core.Repository;

namespace WebApiDemo.Datasource
{
    public class UnitOfWork : IUnitOfWork, IDisposable

    {
        private readonly DataContext _context;
        private readonly ILogger _logger;
        public IStudentRepository Student { get; set; }
        public UnitOfWork(DataContext context, ILoggerFactory logger)
        {
            _context = context;
            _logger = logger.CreateLogger("logs");

            Student = new StudentRepository(_context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.DisposeAsync();
        }
    }
}
