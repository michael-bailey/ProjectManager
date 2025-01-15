using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManager.StaticWebApplication.Data;
using ProjectManager.StaticWebApplication.Models;

namespace ProjectManager.StaticWebApplication.Controllers
{
	public class PrintablesController(
		ApplicationDbContext context,
		SignInManager<UserAccount> signInManager,
		UserManager<UserAccount> userManager)
		: Controller
	{
		
		// GET: Printables
		public async Task<IActionResult> Index()
		{
			var printables = await context.Printables.ToListAsync();

			var printableEnumerator =
				from printable in printables
				select new PrintableModel
				{
					Id = printable.Id,
					Name = printable.Name,
					Description = printable.Description
				};

			return View(printableEnumerator);
		}

		// GET: Printables/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			if (id == null)
				return NotFound();

			var printable = await context.Printables
				.FirstOrDefaultAsync(m => m.Id == id);

			if (printable == null)
				return NotFound();

			return View(new PrintableModel
			{
				Id = printable.Id,
				Name = printable.Name,
				Description = printable.Description
			});
		}

		// GET: Printables/Create
		public IActionResult Create()
		{
			ViewData["BlobId"] = new SelectList(context.Blobs, "Id", "Id");
			return View();
		}

		// POST: Printables/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(
			PrintableModel model)
		{
			if (!signInManager.IsSignedIn(User))
				return Redirect("/");

			var user = await userManager.GetUserAsync(User);

			if (user is null) 
				return Unauthorized();
			
			if (!ModelState.IsValid)
				return View(model);

			Blob blob = new()
			{
				Content = []
			};

			await context.Blobs.AddAsync(blob);

			Printable printable = new()
			{
				
				Name = model.Name,
				Description = model.Description,
				BlobId = blob.Id,

				OwnerId = user.Id,
			};


			context.Add(printable);
			
			await context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		// GET: Printables/Edit/5
		public async Task<IActionResult> Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var printable = await context.Printables.FindAsync(id);
			if (printable == null)
			{
				return NotFound();
			}

			ViewData["BlobId"] =
				new SelectList(context.Blobs, "Id", "Id", printable.BlobId);
			return View(printable);
		}

		// POST: Printables/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id,
			[Bind("Id,Name,Description,BlobId,OwnerId")] PrintableModel printable)
		{
			if (id != printable.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					context.Update(printable);
					await context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PrintableExists(printable.Id))
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


			return View();
		}

		// GET: Printables/Delete/5
		public async Task<IActionResult> Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var printable = await context.Printables
				.Include(p => p.Blob)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (printable == null)
			{
				return NotFound();
			}

			return View(printable);
		}

		// POST: Printables/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var printable = await context.Printables.FindAsync(id);
			if (printable != null)
			{
				context.Printables.Remove(printable);
			}

			await context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool PrintableExists(Guid id)
		{
			return context.Printables.Any(e => e.Id == id);
		}
	}
}