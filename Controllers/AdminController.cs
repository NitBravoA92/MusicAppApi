using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using MusicAppApi.Data;
using MusicAppApi.Models;
using System;

namespace MusicAppApi.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public AdminController(RoleManager<Role> roleManager, ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpPost("roles/create")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                return BadRequest("Error!!");
            }

            var role = new Role { Name = roleName };
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return Ok("Rol '{roleName}' creado exitosamente.");
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return Ok(roles);
        }

        [HttpPost("permissions/create")]
        public async Task<IActionResult> CreatePermission(string permissionName, string description = null)
        {
            if (await _dbContext.Permissions.AnyAsync(p => p.Name == permissionName))
            {
                return BadRequest("The permission already exists!");
            }

            var permission = new Permission { Name = permissionName, Description = description };
            _dbContext.Permissions.Add(permission);
            await _dbContext.SaveChangesAsync();
            
            return Ok("Permiso '{permissionName}' creado exitosamente.");
        }

        [HttpGet("permissions")]
        public async Task<IActionResult> GetPermissions()
        {
            var permissions = await _dbContext.Permissions.ToListAsync();
            return Ok(permissions);
        }
    }
}
