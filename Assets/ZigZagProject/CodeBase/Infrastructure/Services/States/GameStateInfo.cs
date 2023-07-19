namespace CodeBase.Infrastructure.Services.States
{
    public class GameStateInfo : IGameStateInfo
    {
        public bool IsActive { get; set; }
        public bool IsMobileDevice { get; private set; }
        public bool CheatMode { get; set; }
        public int CurrentScore { get; set; }

        public GameStateInfo()
        {
            IsActive = false;
            CheatMode = false;
            CurrentScore = 0;
            TakePlatformService();
        }

        private void TakePlatformService()
        {
#if UNITY_EDITOR
            IsMobileDevice = false;
#elif UNITY_IOS || UNITY_ANDROID
            IsMobileDevice = true;
#endif
        }
    }
}

