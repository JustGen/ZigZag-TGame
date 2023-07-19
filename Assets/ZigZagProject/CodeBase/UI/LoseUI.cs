using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.States;
using CodeBase.Infrastructure.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class LoseUI : MonoBehaviour, IPanelUI
    {
        [SerializeField] private Button _retryButton;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _bestScoreText;
        public GameObject TakeGameObject => this.gameObject;
        
        private ISwitcherPanelUI _switcher;
        private IPersistentProgressService _progress;
        private IGameStateInfo _gameStateInfo;
        
        private void Awake()
        {
            _switcher = AllServices.Container.Single<ISwitcherPanelUI>();
            _progress = AllServices.Container.Single<IPersistentProgressService>();
            _gameStateInfo = AllServices.Container.Single<IGameStateInfo>();
            
            _switcher.PanelsUI.Add(PanelsUI.Lose, this);
            
            _retryButton.onClick.AddListener(RetryGame);
        }

        private void OnEnable()
        {
            _scoreText.text = _gameStateInfo.CurrentScore.ToString();
            _bestScoreText.text = _progress.BestScore.ToString();
        }

        private void OnDestroy() => 
            _retryButton.onClick.RemoveListener(RetryGame);

        private void RetryGame()
        {
            SceneManager.LoadScene(0);
            //_switcher.Switch(_switcher.PanelsUI[PanelsUI.Game]);
        }
    }    
}

