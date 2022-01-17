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

        public override async Task<bool> Upsert(Student entity)
        {
            try
            {
                var student = await dbSet.Where(s => s.Id == entity.Id)
                                     .FirstOrDefaultAsync();

                if (student == null)
                {
                    return await Add(entity);
                }
                student.Id = entity.Id;
                student.Name = entity.Name;
                student.Age = entity.Age;

                return true;

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Repo} Upsert Method error", typeof(StudentRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(int id)
        {
            try
            {
                var exist = await dbSet.Where(s => s.Id == id)
                                       .FirstOrDefaultAsync();

                if (exist != null)
                {
                    dbSet.Remove(exist);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Repo} Upsert Method error", typeof(StudentRepository));
                return false;
            }

        }



    }
}
