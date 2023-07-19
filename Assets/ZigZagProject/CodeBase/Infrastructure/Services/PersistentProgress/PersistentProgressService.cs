using UnityEngine;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        private const string BestScoreKey = "BestScore";
        private const string GamesPlayedKey = "GamesPlayed";
        
        private int _bestScore;
        private int _gamesPlayed;

        public PersistentProgressService()
        {
            RegisterBestScore();
            RegisterGamesPlayed();
        }

        public int BestScore
        {
            get
            {
                _bestScore = PlayerPrefs.GetInt(BestScoreKey);
                return _bestScore;
            }
            set
            {
                _bestScore = value;
                PlayerPrefs.SetInt(BestScoreKey, _bestScore);
            }
        }

        public int GamesPlayed
        {
            get
            {
                _gamesPlayed = PlayerPrefs.GetInt(GamesPlayedKey);
                return _gamesPlayed;
            }
            set
            {
                _gamesPlayed = value;
                PlayerPrefs.SetInt(GamesPlayedKey, _gamesPlayed);
            }
        }

        private void RegisterGamesPlayed()
        {
            if (PlayerPrefs.HasKey(GamesPlayedKey))
                _gamesPlayed = GamesPlayed;
            else
                GamesPlayed = 0;
        }
    
        private void RegisterBestScore()
        {
            if (PlayerPrefs.HasKey(BestScoreKey))
                _bestScore = BestScore;
            else
                BestScore = 0;
        }
    }
}