using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManager.LibEntity;
using ProjectManager.LibEntity.Context;

namespace ProjectManager.Internal.Areas.Project.Controllers;

[Area("Project")]
public class TaskController : Controller
{
    private readonly DatabaseContext _context;

    public TaskController(DatabaseContext context)
    {
        _context = context;
    }

    // GET: Project/Task
    public async Task<IActionResult> Index()
    {
        var databaseContext = _context.Tasks.Include(t => t.Owner);
        return View(await databaseContext.ToListAsync());
    }

    // GET: Project/Task/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var taskEntity = await _context.Tasks
            .Include(t => t.Owner)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (taskEntity == null)
        {
            return NotFound();
        }

        return View(taskEntity);
    }

    // GET: Project/Task/Create
    public IActionResult Create()
    {
        ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "FirstName");
        return View();
    }

    // POST: Project/Task/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,SimpleId,Title,Description,CreateTime,UpdateTime,DueDate,Status,OwnerId")] TaskEntity taskEntity)
    {
        if (ModelState.IsValid)
        {
            _context.Add(taskEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "FirstName", taskEntity.OwnerId);
        return View(taskEntity);
    }

    // GET: Project/Task/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var taskEntity = await _context.Tasks.FindAsync(id);
        if (taskEntity == null)
        {
            return NotFound();
        }
        ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "FirstName", taskEntity.OwnerId);
        return View(taskEntity);
    }

    // POST: Project/Task/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,SimpleId,Title,Description,CreateTime,UpdateTime,DueDate,Status,OwnerId")] TaskEntity taskEntity)
    {
        if (id != taskEntity.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(taskEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskEntityExists(taskEntity.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "FirstName", taskEntity.OwnerId);
        return View(taskEntity);
    }

    // GET: Project/Task/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var taskEntity = await _context.Tasks
            .Include(t => t.Owner)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (taskEntity == null)
        {
            return NotFound();
        }

        return View(taskEntity);
    }

    // POST: Project/Task/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var taskEntity = await _context.Tasks.FindAsync(id);
        if (taskEntity != null)
        {
            _context.Tasks.Remove(taskEntity);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TaskEntityExists(Guid id)
    {
        return _context.Tasks.Any(e => e.Id == id);
    }
}