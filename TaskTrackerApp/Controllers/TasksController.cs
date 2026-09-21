using Microsoft.AspNetCore.Mvc;
using TaskTrackerApp.Models;
using TaskTrackerApp.Services;

namespace TaskTrackerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET /api/tasks
        [HttpGet]
        public ActionResult<List<EmployeeTask>> GetAll()
        {
            return Ok(_taskService.GetAllTasks());
        }

        // GET /api/tasks/{id}
        [HttpGet("{id}")]
        public ActionResult<EmployeeTask> GetById(Guid id)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        // POST /api/tasks
        [HttpPost]
        public ActionResult<EmployeeTask> Create(EmployeeTask task)
        {
            var created = _taskService.CreateTask(task);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT /api/tasks/{id}
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, EmployeeTask task)
        {
            task.Id = id;
            var success = _taskService.UpdateTask(task);
            if (!success) return NotFound();
            return NoContent();
        }

        // PUT /api/tasks/{id}/status
        [HttpPut("{id}/status")]
        public IActionResult ChangeStatus(Guid id, [FromBody] TaskState newStatus)
        {
            var success = _taskService.ChangeStatus(id, newStatus);
            if (!success) return NotFound();
            return NoContent();
        }

        // DELETE /api/tasks/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var success = _taskService.DeleteTask(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}