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

        public IActionResult New()
        {
            ViewData["userNameAttributes"] = new { @class = "form-control", autocomplete = "off" };

            var appRoles = _roleManager.Roles;

            var model = new FormUserViewModel
            {
                RolesListItems = RolesList(appRoles, null)
            };

            return View("FormUsers", model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            var appRoles = _roleManager.Roles;

            var model = new FormUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                RolesListItems = RolesList(appRoles, userRoles)
            };

            return View("FormUsers", model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            await _userManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(FormUserViewModel model)
        {
            var appRoles = _roleManager.Roles;

            if (string.IsNullOrEmpty(model.Id))
            {
                ViewData["userNameAttributes"] = new { @class = "form-control", autocomplete = "off" };

                if (string.IsNullOrEmpty(model.Password))
                {
                    ModelState.AddModelError("Password", "A senha é obrigatória.");
                }

                if (string.IsNullOrEmpty(model.PasswordConfirmation))
                {
                    ModelState.AddModelError("PasswordConfirmation", "A confirmação é obrigatória.");
                }

                if (!string.IsNullOrEmpty(model.Password) && !string.IsNullOrEmpty(model.PasswordConfirmation) &&
                                          model.Password != model.PasswordConfirmation)
                {
                    ModelState.AddModelError("PasswordConfirmation", "A confirmação de senha não confere.");
                }

                var existingUser = await _userManager.FindByNameAsync(model.UserName);

                if (existingUser != null)
                {
                    ModelState.AddModelError("UserName", "O usuário informado já possui cadastro.");
                }

                if (!ModelState.IsValid)
                {
                    model.RolesListItems = RolesList(appRoles, null);
                    return View("FormUsers", model);
                }

                var user = new IdentityUser(model.UserName);

                var status = await _userManager.CreateAsync(user, model.Password);

                if (status.Succeeded)
                {
                    foreach (var role in model.SelectedRoles)
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }
                }
                else
                {
                    foreach (var error in status.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        model.RolesListItems = RolesList(appRoles, await _userManager.GetRolesAsync(user));
                        return View("FormUsers", model);
                    }
                }
            }
            else
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (!ModelState.IsValid)
                {
                    model.RolesListItems = RolesList(appRoles, await _userManager.GetRolesAsync(user));
                    return View("FormUsers", model);
                }

                await _userManager.RemoveFromRolesAsync(user, appRoles.Select(s => s.Name));

                foreach (var roleName in model.SelectedRoles)
                    await _userManager.AddToRoleAsync(user, roleName);
            }

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

            return View("FormRole", model);
        }

        [HttpGet("/Roles/New")]
        public IActionResult NewRole(string id)
        {
            var model = new RoleViewModel();

            return View("FormRole", model);
        }

        [HttpPost("/Roles/Save")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveRole(RoleViewModel model)
        {
            if (!ModelState.IsValid)
                return View("FormRole", model);

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
                    return View("FormRole", model);
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

        private static IEnumerable<SelectListItem> RolesList(IEnumerable<IdentityRole> appRoles, ICollection<string> userRoles)
        {
            var roles = new List<SelectListItem>();

            foreach (var role in appRoles.ToList())
            {
                roles.Add(new SelectListItem(role.Name, role.Name, userRoles?.Contains(role.Name) ?? false));
            }

            return roles;
        }
    }
}