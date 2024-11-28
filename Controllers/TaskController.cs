using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList1.Models;
using TodoList1.Data;

namespace TodoList1.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Task
        [HttpPost("AddTasks")]
        public async Task<IActionResult> PostTask([FromBody] TasksModel task)
        {
            if (task == null)
            {
                return BadRequest("Task data is null");
            }

            // Check if the user ID exists in the database
            var userExists = await _context.Users.AnyAsync(u => u.UserId == task.UserId);
            if (!userExists)
            {
                return NotFound("User not found");
            }

            // Model validation check
            if (ModelState.IsValid)
            {
                // Save the task to the database
                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();

                // Return the created task and a 201 Created status
                return Ok("Task added successfully");
            }

            return BadRequest("Invalid task data");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound("task not found");
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();


            return Ok("Task deleted sucessfully");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TasksModel updatedTask)
        {
            // Check if the provided task ID matches the ID of the task being updated
            if (id != updatedTask.Id)
            {
                return BadRequest("Task ID mismatch.");
            }


            // Find the existing task by ID
            var task = await _context.Tasks.FindAsync(id);
            if (task.UserId != updatedTask.UserId)
            {
                return BadRequest("User ID mismatch.");
            }
            if (task == null)
            {
                return NotFound("Task not found.");
            }

            // Update the task's properties
            task.TaskName = updatedTask.TaskName;
            task.Status = updatedTask.Status;
            task.CompletedDate = updatedTask.CompletedDate;

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a success response
            return Ok("Task updated successfully.");

        }


        [HttpPut("mark-done/{id}")]
        public async Task<IActionResult> MarkTaskDone(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) { return NotFound("Task not found"); }

            task.Status = true;
            task.CompletedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok("Task marked as done successfully.");


        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTasksByUserId(int id)
        {
            // Fetch tasks associated with the specified userId
            var tasks = await _context.Tasks
                                      .Where(t => t.UserId == id)
                                      .ToListAsync();

            // Check if any tasks were found
            if (tasks == null || tasks.Count == 0)
            {
                return NotFound("No tasks found for the specified user.");
            }

            // Return the list of tasks
            return Ok(tasks);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Viewtask()
        {
            // Get the logged-in user's ID from session
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "User"); // Redirect to login if user is not authenticated
            }

            // Pass the userId to the view using ViewData
            ViewData["UserId"] = userId;

            return View();
        }

       /* [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Viewtask()
        {
            return View();
        }*/

    }
}
