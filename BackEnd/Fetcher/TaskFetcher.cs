using EntityLib;
using EntityLib.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Shared.Task;
using TaskStatus = Shared.Task.TaskStatus;

namespace BackEnd.Fetcher;

public class TaskFetcher (DatabaseContext ctx)
{
	public List<TaskEntity> AllNoneStatus()
	{
		return (
			from task in ctx.Tasks
			where task.Status == TaskStatus.None
			select task
			).ToList();
	}

	public List<TaskEntity> AllTasks()
	{
		return (
			from task in ctx.Tasks
			select task
			).ToList();
	}

	public async Task<List<TaskEntity>> AddNewTask(string title, string description)
	{
		var task = TaskEntity.NewEntity(
			title: title,
			description: description
		);
		
		ctx.Tasks.Add(task);

		await ctx.SaveChangesAsync();
		
		return AllNoneStatus();
	}

	/**
	 * # DeleteTask
	 * Removes a task from the database
	 * 
	 */
	public void DeleteTask(Guid taskId)
	{
		TaskEntity? entity = (
			from task in ctx.Tasks
			where task.Id == taskId
			select task)
			.ToList()
			.First();
		
		ctx.Tasks.Remove(entity);
		
		ctx.SaveChangesAsync();
	}

	public TaskData GetTask(int simpleId)
	{
		return (from task in ctx.Tasks
			where task.SimpleId == simpleId
			select task).First().ToData();
	}
}