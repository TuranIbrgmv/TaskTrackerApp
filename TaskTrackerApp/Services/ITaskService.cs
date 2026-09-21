using TaskTrackerApp.Models;

namespace TaskTrackerApp.Services
{
    public interface ITaskService
    {
        List<EmployeeTask> GetAllTasks();
        EmployeeTask? GetTaskById(Guid id);
        EmployeeTask CreateTask(EmployeeTask task);
        bool UpdateTask(EmployeeTask task);
        bool ChangeStatus(Guid id, TaskState newStatus);
        bool DeleteTask(Guid id);
    }
}