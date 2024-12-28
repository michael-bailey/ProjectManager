using BackEnd.Attribute;
using BackEnd.Fetcher;
using EntityLib;
using EntityLib.Context;
using Microsoft.AspNetCore.Mvc;
using Shared.Task;

namespace BackEnd.Controller;

[Project(Name = "Tasks")]
[Route("/api/[controller]/")]
[ApiController]
public class TaskController( DatabaseContext ctx ) : ControllerBase
{
	private readonly TaskFetcher _taskFetcher = new TaskFetcher(ctx);
	
	[HttpGet("init")]
	public List<TaskEntity> InitTasks()
	{
		var tasks = _taskFetcher.AllNoneStatus();
		return tasks;
	}
	
	[HttpGet]
	public List<TaskEntity> GetAllTasks()
	{
		return _taskFetcher.AllTasks();
	}

	[HttpGet("T{simpleId}")]
	public TaskData GetTaskById(string simpleId)
	{
		var parsedId = int.Parse(simpleId);
		
		return _taskFetcher.GetTask(parsedId);
	}

	[HttpPost]
	public async Task<List<TaskEntity>> AddTask([FromBody] NewTaskInput input)
	{
		string title = input.title;
		string description = input.description;
		
		Console.WriteLine($"title value: {title}");
		Console.WriteLine($"description value: {title}");
		
		return await _taskFetcher.AddNewTask(title, description);
	}

	[HttpDelete]
	public async Task<List<TaskEntity>> DeleteTask(Guid taskId)
	{
		_taskFetcher.DeleteTask(taskId);
		return _taskFetcher.AllTasks();
	}
}