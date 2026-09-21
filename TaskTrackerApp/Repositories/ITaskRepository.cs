using TaskTrackerApp.Models;

namespace TaskTrackerApp.Repositories
{
    public interface ITaskRepository
    {
        List<EmployeeTask> GetAll();
        EmployeeTask? GetById(Guid id);
        void Add(EmployeeTask task);
        void Update(EmployeeTask task);
        void Delete(Guid id);
    }
}