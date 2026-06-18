public interface IDashboardService
{
    Task<DashboardResponse> GetDashboard(string userId);
}