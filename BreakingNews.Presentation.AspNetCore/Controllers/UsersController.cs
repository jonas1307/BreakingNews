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
            var users = _userManager.Users;

            return View(users);
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
                Roles = RolesList(appRoles, userRoles)
            };

            return View("FormUsers", model);
        }

        private IEnumerable<SelectListItem> RolesList(IEnumerable<IdentityRole> appRoles, ICollection<string> userRoles)
        {
            var retorno = new List<SelectListItem>();

            foreach (var role in appRoles.ToList())
            {
                retorno.Add(new SelectListItem(role.Name, role.Name, userRoles.Contains(role.Name)));
            }

            return retorno;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(UserViewModel model)
        {
            if (!ModelState.IsValid)
                return View("FormUsers", model);

            if (string.IsNullOrEmpty(model.Id))
            {
                var result = await _userManager.CreateAsync(new IdentityUser(model.UserName), model.Password);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.UserName);

                    foreach (var roleName in model.SelectedRoles)
                    {
                        if (!await _userManager.IsInRoleAsync(user, roleName))
                        {
                            await _userManager.AddToRoleAsync(user, roleName);
                        }
                    }
                }

                RedirectToAction("Index");
            }
            else
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                var appRoles = _roleManager.Roles;

                await _userManager.RemoveFromRolesAsync(user, appRoles.Select(s => s.Name));

                foreach (var roleName in model.SelectedRoles)
                    await _userManager.AddToRoleAsync(user, roleName);
            }

            return Ok();
        }
    }
}