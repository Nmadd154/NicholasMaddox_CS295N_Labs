// Created by Nicholas Maddox
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcEldenRingBossLore.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEldenRingBossLore.Controllers;

[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // GET: User
    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();
        var userVMs = new List<UserVM>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userVMs.Add(new UserVM
            {
                UserId = user.Id,
                UserName = user.UserName ?? "",
                Email = user.Email ?? "",
                Name = user.Name,
                Roles = roles.ToList()
            });
        }

        return View(userVMs);
    }

    // GET: User/Create
    public async Task<IActionResult> Create()
    {
        var allRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
        var registerVM = new RegisterVM
        {
            AllRoles = allRoles!
        };
        return View(registerVM);
    }

    // POST: User/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RegisterVM registerVM, List<string> selectedRoles)
    {
        if (ModelState.IsValid)
        {
            var user = new AppUser
            {
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                Name = registerVM.Name,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerVM.Password);
            if (result.Succeeded)
            {
                // Add selected roles
                if (selectedRoles != null && selectedRoles.Any())
                {
                    await _userManager.AddToRolesAsync(user, selectedRoles);
                }

                TempData["Success"] = "User created successfully.";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        var allRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
        registerVM.AllRoles = allRoles!;
        return View(registerVM);
    }

    // GET: User/Edit
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        var allRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

        var userVM = new UserVM
        {
            UserId = user.Id,
            UserName = user.UserName ?? "",
            Email = user.Email ?? "",
            Name = user.Name,
            Roles = userRoles.ToList(),
            AllRoles = allRoles!
        };

        return View(userVM);
    }

    // POST: User/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, UserVM userVM, List<string> selectedRoles)
    {
        if (id != userVM.UserId)
        {
            return NotFound();
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        // Update user properties
        user.Name = userVM.Name;
        user.Email = userVM.Email;
        user.UserName = userVM.UserName;

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            foreach (var error in updateResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(userVM);
        }

        // Update roles
        var currentRoles = await _userManager.GetRolesAsync(user);
        var rolesToAdd = selectedRoles.Except(currentRoles);
        var rolesToRemove = currentRoles.Except(selectedRoles);

        await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
        await _userManager.AddToRolesAsync(user, rolesToAdd);

        return RedirectToAction(nameof(Index));
    }

    // GET: User/Delete
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        var userVM = new UserVM
        {
            UserId = user.Id,
            UserName = user.UserName ?? "",
            Email = user.Email ?? "",
            Name = user.Name,
            Roles = userRoles.ToList()
        };

        return View(userVM);
    }

    // POST: User/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser?.Id == user.Id)
        {
            TempData["Error"] = "You cannot delete your own account.";
            return RedirectToAction(nameof(Index));
        }

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            TempData["Error"] = "Failed to delete user.";
            return RedirectToAction(nameof(Index));
        }

        TempData["Success"] = "User deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}
