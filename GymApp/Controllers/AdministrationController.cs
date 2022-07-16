using GymApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymApp.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager,UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleModel createRoleModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = createRoleModel.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }
                else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            return View(createRoleModel);
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role= await roleManager.FindByIdAsync(id);
            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id: {id} cannot be found!";
                return View("Error");
            }
            var model = new EditRoleModel
            {
                Id = id,
                RoleName = role.Name,
            };
            foreach(var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user,role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id: {model.Id} cannot be found!";
                return View("Error");
            }
            else
            {
                role.Name=model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id:{roleId} cannot be found";
                return View("Error");
            }
            var model = new List<UserRoleModel>();
            foreach(var user in userManager.Users)
            {
                var userRoleModel = new UserRoleModel
                {
                    UserID = user.Id,
                    UserName = user.UserName,
                };
                if(await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleModel.IsSelected = true;
                }
                else
                {
                    userRoleModel.IsSelected = false;
                }
                model.Add(userRoleModel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleModel> userRoleModels,string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role with id:{roleId} cannot be found";
                return View("Error");
            }
            for(int i = 0; i < userRoleModels.Count; i++)
            {
                var user = await userManager.FindByIdAsync(userRoleModels[i].UserID);
                IdentityResult result = null;
                if (userRoleModels[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result=await userManager.AddToRoleAsync(user, role.Name);
                }else if (!(userRoleModels[i].IsSelected) && (await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result= await userManager.RemoveFromRoleAsync(user, role.Name);

                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < userRoleModels.Count)
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole",new { Id = role.Id });
                    }
                }
            }

            return RedirectToAction("EditRole",new {Id=role.Id});
        }
    }
}
