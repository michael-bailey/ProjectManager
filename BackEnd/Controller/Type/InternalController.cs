using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controller.Type;

[ApiController]
[Authorize(Roles = "Internal")]
public class InternalController : ControllerBase
{ }