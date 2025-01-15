using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManager.LibEntity;
using ProjectManager.LibEntity.Context;

namespace ProjectManager.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController(DatabaseContext context) : ControllerBase
{
    // GET: api/Task
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskEntity>>> GetTasks()
    {
        return await context.Tasks.ToListAsync();
    }

    // GET: api/Task/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskEntity>> GetTaskEntity(Guid id)
    {
        var taskEntity = await context.Tasks.FindAsync(id);

        if (taskEntity == null)
        {
            return NotFound();
        }

        return taskEntity;
    }

    // PUT: api/Task/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTaskEntity(Guid id, TaskEntity taskEntity)
    {
        if (id != taskEntity.Id)
        {
            return BadRequest();
        }

        context.Entry(taskEntity).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TaskEntityExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Task
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TaskEntity>> PostTaskEntity(TaskEntity taskEntity)
    {
        context.Tasks.Add(taskEntity);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetTaskEntity", new { id = taskEntity.Id }, taskEntity);
    }

    // DELETE: api/Task/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaskEntity(Guid id)
    {
        var taskEntity = await context.Tasks.FindAsync(id);
        if (taskEntity == null)
        {
            return NotFound();
        }

        context.Tasks.Remove(taskEntity);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool TaskEntityExists(Guid id)
    {
        return context.Tasks.Any(e => e.Id == id);
    }
}