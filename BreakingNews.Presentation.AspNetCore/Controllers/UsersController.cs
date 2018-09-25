using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakingNews.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BreakingNews.Presentation.AspNetCore.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var model = new UserIndexViewModel
            {
                Roles = _roleManager.Roles,
                Users = _userManager.Users
            };

            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            var appRoles = _roleManager.Roles;

            var model = new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                RolesListItems = RolesList(appRoles, userRoles)
            };

            return View("FormUsers", model);
        }

        private static IEnumerable<SelectListItem> RolesList(IEnumerable<IdentityRole> appRoles, ICollection<string> userRoles)
        {
            var roles = new List<SelectListItem>();

            foreach (var role in appRoles.ToList())
            {
                roles.Add(new SelectListItem(role.Name, role.Name, userRoles.Contains(role.Name)));
            }

            return roles;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(UserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            var appRoles = _roleManager.Roles;

            if (!ModelState.IsValid)
            {
                model.RolesListItems = RolesList(appRoles, await _userManager.GetRolesAsync(user));
                return View("FormUsers", model);
            }

            await _userManager.RemoveFromRolesAsync(user, appRoles.Select(s => s.Name));

            foreach (var roleName in model.SelectedRoles)
                await _userManager.AddToRoleAsync(user, roleName);

            return RedirectToAction("Index");

        }

        [HttpGet("/Roles/Edit/{id}")]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
                return NotFound();

            var model = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };

            return View("RoleForm", model);
        }

        [HttpGet("/Roles/New")]
        public IActionResult NewRole(string id)
        {
            var model = new RoleViewModel();

            return View("RoleForm", model);
        }

        [HttpPost("/Roles/Save")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveRole(RoleViewModel model)
        {
            if (!ModelState.IsValid)
                return View("RoleForm", model);

            if (model.Id == null)
            {
                var role = new IdentityRole(model.Name);
                await _roleManager.CreateAsync(role);
            }
            else
            {
                var existingRole = await _roleManager.FindByIdAsync(model.Id);

                if (existingRole.Name == "admin" || existingRole.Name == "user")
                {
                    ModelState.AddModelError("Name", "Não é possível alterar as Roles padrões.");
                    return View("RoleForm", model);
                }

                existingRole.Name = model.Name;

                await _roleManager.UpdateAsync(existingRole);
            }

            return RedirectToAction("Index");

        }

        [HttpGet("/Roles/Delete/{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
                return NotFound();

            if (role.Name == "admin" || role.Name == "user")
                return RedirectToAction("Index");

            var userInRole = await _userManager.GetUsersInRoleAsync(role.Name);

            foreach (var user in userInRole)
            {
                await _userManager.RemoveFromRoleAsync(user, role.Name);
            }

            await _roleManager.DeleteAsync(role);

            return RedirectToAction("Index");
        }
    }
}