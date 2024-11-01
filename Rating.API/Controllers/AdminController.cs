using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Rating.Application.DTOs.User;
using Rating.Application.Services;
using Rating.Domain.Abstractions;
using Rating.Domain.Enums;

namespace Rating.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly AdminService _adminService;

    public AdminController(AdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPut("replace-role")]
    public async Task<IActionResult> ReplaceRole(Guid userId, Role role)
    {
        await _adminService.ReplaceRoleAsync(userId, role);
        return Ok("Роль успешно изменена");
    }
}