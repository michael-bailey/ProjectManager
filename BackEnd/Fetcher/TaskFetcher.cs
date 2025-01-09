using EntityLib;
using EntityLib.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Shared.Task;
using TaskStatus = Shared.Task.TaskStatus;

namespace BackEnd.Fetcher;

/**
 * # TaskFetcher
 * Class for handling the creation and global fetching of tasks
 */
public class TaskFetcher (
	DatabaseContext ctx,
	UserManager<UserEntity> userManager,
	SignInManager<UserEntity> signInManager)
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
	
	public TaskData GetTask(int simpleId)
	{
		return (from task in ctx.Tasks
			where task.SimpleId == simpleId
			select task).First().ToData();
	}

	public async Task<TaskEntity> AddNewTaskAsync(
		Guid userId, NewTaskInput inputs)
	{
		var task = new TaskEntity
		{
			Title = inputs.Title,
			Description = inputs.Description,

			CreateTime = DateTime.Now.ToUniversalTime(),
			UpdateTime = DateTime.Now.ToUniversalTime(),
			DueDate = inputs.DueDate,

			Status = inputs.Status,
			
			OwnerId = userId
		};
		
		ctx.Tasks.Add(task);

		await ctx.SaveChangesAsync();
		
		return task;
	}

	public void DeleteTask(int taskId)
	{
		var entity = (
			from task in ctx.Tasks
			where task.SimpleId == taskId
			select task)
			.First();
		
		ctx.Tasks.Remove(entity);
		
		ctx.SaveChangesAsync();
	}

	public TaskEntity PatchTask(int simpleId, PatchTaskInput patches)
	{
		var task = (from t in ctx.Tasks
			where t.SimpleId == simpleId
			select t).First();

		if (task is null)
			throw new Exception("task id doers not exist");

		task.Title = patches.Title ?? task.Title;
		task.Description = patches.Description ?? task.Description;
		task.Status = patches.Status ?? task.Status;
		task.DueDate = patches.DueDate ?? task.DueDate;
		task.OwnerId = patches.OwnerId ?? task.OwnerId;
		
		ctx.SaveChanges();

		return task;
	}
}