using CodeBase.Infrastructure.AssetManagement;

namespace CodeBase.Infrastructure.Services.States
{
    public interface IGameStateInfo : IService
    {
        bool IsActive { get; set; }
        bool IsMobileDevice { get; }
        bool CheatMode { get; set; }
        int CurrentScore { get; set; }
    }
}