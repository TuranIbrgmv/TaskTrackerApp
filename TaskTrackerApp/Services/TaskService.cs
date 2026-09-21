using TaskTrackerApp.Models;
using TaskTrackerApp.Repositories;

namespace TaskTrackerApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public List<EmployeeTask> GetAllTasks()
        {
            return _repository.GetAll();
        }

        public EmployeeTask? GetTaskById(Guid id)
        {
            return _repository.GetById(id);
        }

        public EmployeeTask CreateTask(EmployeeTask task)
        {
            task.Id = Guid.NewGuid();
            task.CreatedAt = DateTime.Now;
            _repository.Add(task);
            return task;
        }

        public bool UpdateTask(EmployeeTask task)
        {
            var existing = _repository.GetById(task.Id);
            if (existing == null) return false;

            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.EmployeeName = task.EmployeeName;
            existing.Priority = task.Priority;
            _repository.Update(existing);
            return true;
        }

        public bool ChangeStatus(Guid id, TaskState newStatus)
        {
            var existing = _repository.GetById(id);
            if (existing == null) return false;

            existing.Status = newStatus;
            _repository.Update(existing);
            return true;
        }

        public bool DeleteTask(Guid id)
        {
            var existing = _repository.GetById(id);
            if (existing == null) return false;

            _repository.Delete(id);
            return true;
        }
    }
}