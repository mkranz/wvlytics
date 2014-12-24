using Amazon.DynamoDBv2.DataModel;
using Wvlytics.Model;

namespace Wvlytics.Services
{
    public class DynamoDBAppenderService : IAppenderService
    {
        private readonly DynamoDBContext _context;

        public DynamoDBAppenderService(DynamoDBContext context)
        {
            _context = context;
        }

        public void SaveMatch(MatchHistory match)
        {
            _context.Save(match);
        }

        public void SaveMatchSnapshot(MatchHistorySnapshot snapshot)
        {
            _context.Save(snapshot);
        }
    }

    public interface IAppenderService
    {
        void SaveMatch(MatchHistory match);
        void SaveMatchSnapshot(MatchHistorySnapshot snapshot);
    }
}