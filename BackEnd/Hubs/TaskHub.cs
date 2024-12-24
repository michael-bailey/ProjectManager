using EntityLib;
using EntityLib.Context;
using Microsoft.AspNetCore.SignalR;

namespace BackEnd.Hubs;

public class TaskHub(DatabaseContext context) : Hub
{
	public async Task SendTaskUpdates()
	{
		var data = (from task in context.Tasks select task).ToList();
		await Clients.All.SendAsync("TaskUpdate", data);
	}
}