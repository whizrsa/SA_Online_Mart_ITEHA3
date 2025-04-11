using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SA_Online_Mart.Data;
using SA_Online_Mart.Models;
using SA_Online_Mart.Services;
using SA_Online_Mart.ViewModel;
using System.Threading.Tasks;

namespace SA_Online_Mart.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            var dashboardData = await _adminService.GetDataCount(User);

            if (dashboardData != null)
            {
                return View(dashboardData);
            }

            return NotFound("Admin Dashboard not responsive");

        }
    }
}
