using SA_Online_Mart.Models;

namespace SA_Online_Mart.ViewModel
{
    public class AdminDashboardViewModel
    {
        public AppUser User { get; set; }
        public int ProductCount { get; set; }
        public int CustomerCount { get; set; }  
    }
}
