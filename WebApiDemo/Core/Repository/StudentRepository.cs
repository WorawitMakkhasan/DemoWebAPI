using WebApiDemo.Core.IRepository;
using WebApiDemo.Model;

namespace WebApiDemo.Core.Repository
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(DataContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<Student>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Repo} All Method error", typeof(StudentRepository));
                return new List<Student>();
            }
        }

    }
}
