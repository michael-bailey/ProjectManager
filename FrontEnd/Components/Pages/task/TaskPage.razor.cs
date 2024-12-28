using Microsoft.AspNetCore.Components;
using Shared.Task;

namespace FrontEnd.Components.Pages.task;

public partial class TaskPage
{
	[Parameter] public string TaskId { get; set; }

	private TaskData? _taskData { get; set; } = null;
	public bool IsLoading
	{
		get => _taskData is null;
	}

	protected override async Task OnInitializedAsync()
	{
		_taskData = await Fetcher.FetchTask(TaskId);
	}
}