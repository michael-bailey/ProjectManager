using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controller.Type;

[ApiController]
[AllowAnonymous]
public class PublicController : ControllerBase
{ }