using Company.Pro.DAL.Models;
using Company.Pro.PL.Dtos;
using Company.Pro.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Company.Pro.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _rolemangager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _rolemangager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<RoleToReturnDto> roles;
            if (string.IsNullOrEmpty(SearchInput))
            {
                roles = _rolemangager.Roles.Select(U => new RoleToReturnDto()
                {
                    Id = U.Id,
                    Name = U.Name
                });
            }
            else
            {
                roles = _rolemangager.Roles.Select(U => new RoleToReturnDto()
                {
                    Id = U.Id,
                    Name = U.Name
                }).Where(U => U.Name.ToLower().Contains(SearchInput.ToLower()));
            }
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleToReturnDto model)
        {
            if (ModelState.IsValid)  // Server Side Validation
            {
                var role = await _rolemangager.FindByNameAsync(model.Name);
                if (role is null)
                {
                    role = new IdentityRole()
                    {
                        Name = model.Name
                    };
                    var result = await _rolemangager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest("Invalid Id");
            }
            var role = await _rolemangager.FindByIdAsync(id);
            if (role is null)
            {
                return NotFound("Role Not Found");
            }
            var dto = new RoleToReturnDto()
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(ViewName, dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {

            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // To Prevent Any Tools From Sending Request To This Action Method
        public async Task<IActionResult> Edit([FromRoute] string id, RoleToReturnDto model) // Model Binding FromRoute To Prevent Overposting Attack
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id)
                {
                    return BadRequest("Invalid Id");
                }
                var role = await _rolemangager.FindByIdAsync(id);
                if (role is null)
                {
                    return NotFound("Role Not Found");
                }
                role.Name = model.Name;

                var result = await _rolemangager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // To Prevent Any Tools From Sending Request To This Action Method
        public async Task<IActionResult> Delete([FromRoute] string id, RoleToReturnDto model) // Model Binding FromRoute To Prevent Overposting Attack
        {
            // if (ModelState.IsValid)
            // {
            if (id != model.Id)
            {
                return BadRequest("Invalid Id");
            }
            var user = await _rolemangager.FindByIdAsync(id);
            if (user is null)
            {
                return NotFound("Role Not Found");
            }

            var result = await _rolemangager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            // }
            return View(model);
        }

        // AddOrRemoveUser
        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUser(string roleId)
        {
            var role = await _rolemangager.FindByIdAsync(roleId);
            if (role is null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = roleId;
            var userInRoleDto = new List<UserInRoleDto>();
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var userInRole = new UserInRoleDto()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }
                userInRoleDto.Add(userInRole);
            }
            return View(userInRoleDto);
        }

        // AddOrRemoveUser
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(string roleId,List<UserInRoleDto> users)
        {
            var role = await _rolemangager.FindByIdAsync(roleId);
            if (role is null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if (appUser is not null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        }
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                        }
                    }
                }
                return RedirectToAction(nameof(Edit), new {id = roleId});
            }
            return View(users);
        }
    }
}
