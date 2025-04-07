using SA_Online_Mart.ViewModel;
using System.Security.Claims;

namespace SA_Online_Mart.Services
{
    public interface IAdminService
    {
        Task<AdminDashboardViewModel> GetDataCount(ClaimsPrincipal user);
    }
}
