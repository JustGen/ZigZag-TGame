using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.States;
using CodeBase.Infrastructure.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class MenuUI : MonoBehaviour, IPanelUI
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private TMP_Text _bestScoreText;
        [SerializeField] private TMP_Text _gamesPlayedText;
        public GameObject TakeGameObject => this.gameObject;

        private ISwitcherPanelUI _switcher;
        private IGameStateInfo _gameStateInfo;
        private IPersistentProgressService _progress;
        
        private void Awake()
        {
            _switcher = AllServices.Container.Single<ISwitcherPanelUI>();
            _gameStateInfo = AllServices.Container.Single<IGameStateInfo>();
            _progress = AllServices.Container.Single<IPersistentProgressService>();
            _switcher.PanelsUI.Add(PanelsUI.Menu, this);
            
            _startGameButton.onClick.AddListener(OnStartGame);
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            _bestScoreText.text = "BEST SCORE: " + _progress.BestScore;
            _gamesPlayedText.text = "GAMES PLAYED: " + _progress.GamesPlayed;
        }

        private void OnDestroy() => 
            _startGameButton.onClick.RemoveListener(OnStartGame);

        private void OnStartGame()
        {
            _switcher.Switch(_switcher.PanelsUI[PanelsUI.Game]);
            _gameStateInfo.IsActive = true;
        }

        private void OnEnable() => 
            _switcher.Switch(this);
    }
}