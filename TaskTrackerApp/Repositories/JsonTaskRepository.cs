using System.Text.Json;
using TaskTrackerApp.Models;

namespace TaskTrackerApp.Repositories
{
    public class JsonTaskRepository : ITaskRepository
    {
        private readonly string _filePath;
        private readonly object _lock = new();

        public JsonTaskRepository(IWebHostEnvironment env)
        {
            var dataFolder = Path.Combine(env.ContentRootPath, "Data");
            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);

            _filePath = Path.Combine(dataFolder, "tasks.json");

            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "[]");
        }

        public List<EmployeeTask> GetAll()
        {
            lock (_lock)
            {
                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<EmployeeTask>>(json) ?? new List<EmployeeTask>();
            }
        }

        public EmployeeTask? GetById(Guid id)
        {
            return GetAll().FirstOrDefault(t => t.Id == id);
        }

        public void Add(EmployeeTask task)
        {
            lock (_lock)
            {
                var tasks = GetAll();
                tasks.Add(task);
                SaveAll(tasks);
            }
        }

        public void Update(EmployeeTask task)
        {
            lock (_lock)
            {
                var tasks = GetAll();
                var index = tasks.FindIndex(t => t.Id == task.Id);
                if (index != -1)
                {
                    tasks[index] = task;
                    SaveAll(tasks);
                }
            }
        }

        public void Delete(Guid id)
        {
            lock (_lock)
            {
                var tasks = GetAll();
                tasks.RemoveAll(t => t.Id == id);
                SaveAll(tasks);
            }
        }

        private void SaveAll(List<EmployeeTask> tasks)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(tasks, options);
            File.WriteAllText(_filePath, json);
        }
    }
}