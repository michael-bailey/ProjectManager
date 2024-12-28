using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Shared;
using Shared.Task;

namespace FrontEnd.Fetcher;

/**
 * # TaskFetcher
 *
 * Set of utilities for interfacing with backend to fetch tasks.
 *
 * Is injected into pages using this line...
 * `@inject TaskFetcher Fetcher`
 *
 * This is used only on frontends
 *
 * > Ensure that any frontend project references the backend.
 */
public class TaskFetcher( HttpClient httpClient )
{
	/** Fetches all tasks */
	public async Task<List<TaskData>> FetchTasks()
	{
		return await httpClient.GetFromJsonAsync<List<TaskData>>("/api/task") ?? [];
	}

	/** Creates a new tasks with title and description */
	public async Task<List<TaskData>> CreateTasks(string title,
		string description)
	{
		var content = JsonContent.Create(new NewTaskInput(title, description));
		var response = await httpClient.PostAsync("api/task", content);
		
		return await response.Content.ReadFromJsonAsync<List<TaskData>>() ?? throw new NotSupportedException();
	}
	
	/** fetches data for a single Task */
	public async Task<TaskData> FetchTask(string simpleId)
	{
		var response = await httpClient.GetAsync($"/api/task/{simpleId}");
		return await response.Content.ReadFromJsonAsync<TaskData>() ?? throw new NotSupportedException();
	}
}