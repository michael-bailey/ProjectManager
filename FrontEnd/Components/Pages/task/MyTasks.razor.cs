using FrontEnd.Fetcher;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shared.Task;

namespace FrontEnd.Components.Pages.task;

public partial class MyTasks
{
	
	[Inject] public required NavigationManager Navigation { get; set; }
	[Inject] public required TaskFetcher Fetcher { get; set; }
	
	
	private List<TaskData> _tasks = [];

	private HashSet<TaskData> _selectedTasks = [];
    
    
	private bool _isCellEditMode;
	private List<string> _events = new();
	private bool _editTriggerRowClick;
    
	protected override async Task OnInitializedAsync()
	{
		_tasks = await Fetcher.FetchTasks();
	}

	private void GotoEditTask(DataGridRowClickEventArgs<TaskData> e)
	{
		Navigation.NavigateTo($"/task/T{e.Item.SimpleId}");
	}
	
	private void GotoEditTask(EventArgs e)
	{
		Navigation.NavigateTo($"/task/T{e}");
	}

	private void ShowMenu()
	{
		Navigation.NavigateTo("/my_tasks");
	}

	private void ShowContextMenu(DataGridRowClickEventArgs<TaskData> obj)
	{
		throw new NotImplementedException();
	}
}