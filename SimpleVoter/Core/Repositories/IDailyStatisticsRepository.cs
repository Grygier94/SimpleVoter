using SimpleVoter.Core.Models;

namespace SimpleVoter.Core.Repositories
{
    public interface IDailyStatisticsRepository
    {
        DailyStatistics GetTodays();

        int GetTotalUsers();
        int GetTotalPublicPolls();
        int GetTotalPrivatePolls();
        int GetTotalUniqueVisitors();
        int GetTotalPageViews();
        
        void Increase_NewUsers();
        void Increase_DeletedUsers();
        void Increase_NewPublicPolls();
        void Increase_DeletedPublicPolls();
        void Increase_NewPrivatePolls();
        void Increase_DeletedPrivatePolls();
        void Increase_UniqueVisitors();
        void Increase_PageViews();
    }
}