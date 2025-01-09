using BackEnd.Attribute;
using BackEnd.Controller.Type;
using BackEnd.Fetcher;
using EntityLib;
using EntityLib.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Newtonsoft.Json.Linq;
using Shared.Task;

namespace BackEnd.Controller;

[Project(Name = "Tasks")]
[Route("/api/[controller]/")]
[ApiController]
public class TaskController(
	UserManager<UserEntity> userManager,
	TaskFetcher taskFetcher) : ProtectedController
{
	
	[HttpGet("all")]
	public List<TaskEntity> GetAllTasks()
	{
		return taskFetcher.AllTasks();
	}

	[HttpGet]
	public async Task<ActionResult<ICollection<TaskData>>> GetAllUsersTasks()
	{
		var user = await userManager.GetUserAsync(User);

		if (user is null)
			return Unauthorized("No user signed in.");

		return (
			from task in user.Tasks
			select task.ToData())
			.ToList();
	}
	
	[HttpPost]
	public async Task<ActionResult<TaskEntity>> CreateTask(NewTaskInput input)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
		
		var user = await userManager.GetUserAsync(User);
		
		var title = input.Title;
		var description = input.Description;

		if (user is null)
			return Unauthorized("No user signed in.");

		var task = await taskFetcher.AddNewTaskAsync(user.Id, input);
		
		return Ok(task);
	}
	
	[HttpGet("T{simpleId:int}")]
	public TaskData GetTaskById(int simpleId)
	{
		return taskFetcher.GetTask(simpleId);
	}
	
	[HttpPatch("T{simpleId:int}")]
	public ActionResult PatchTask(int simpleId, PatchTaskInput patches)
	{
		if (!ModelState.IsValid)
			BadRequest(ModelState.Values.SelectMany(v => v.Errors));
		
		return Ok(taskFetcher.PatchTask(simpleId, patches));
	}
	
	[HttpDelete("T{simpleId:int}")]
	public ActionResult<List<TaskEntity>> DeleteTask(int simpleId)
	{
		taskFetcher.DeleteTask(simpleId);
		return taskFetcher.AllTasks();
	}
}