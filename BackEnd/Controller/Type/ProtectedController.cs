using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controller.Type;

[ApiController]
[Authorize]
public class ProtectedController : ControllerBase
{
	
}