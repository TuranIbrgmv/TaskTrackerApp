namespace TaskTrackerApp.Models
{
    public class EmployeeTask
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public TaskState Status { get; set; } = TaskState.New;
        public string Priority { get; set; } = "Обычный";
    }
}