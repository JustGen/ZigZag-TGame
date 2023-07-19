using CodeBase.Infrastructure.AssetManagement;

namespace CodeBase.Infrastructure.Services.Settings
{
    public interface ISettingsService : IService
    {
        bool SoundActive { get; set; }
    }
}