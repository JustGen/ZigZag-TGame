using CodeBase.Infrastructure.AssetManagement;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        int BestScore { get; set; }
        int GamesPlayed { get; set; }
    }
}