using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Events;
using CodeBase.Infrastructure.UI;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class GameUI : MonoBehaviour, IPanelUI
    {
        [SerializeField] private TMP_Text _scoreCountText;
        public GameObject TakeGameObject => this.gameObject;
        
        private ISwitcherPanelUI _switcher;
        private int _scoreCount;

        private void Awake()
        {
            _switcher = AllServices.Container.Single<ISwitcherPanelUI>();

            _switcher.PanelsUI.Add(PanelsUI.Game, this);

            EventsProvider.OnTakeStone += TakeStone;
            EventsProvider.OnTap += AddScore;
            EventsProvider.OnLose += LosePanelActivation;

            _scoreCount = 0;
        }

        private void OnDestroy()
        {
            EventsProvider.OnTakeStone -= TakeStone;
            EventsProvider.OnTap -= AddScore;
            EventsProvider.OnLose -= LosePanelActivation;
        }

        private void TakeStone()
        {
            _scoreCount += 2;
            UpdateScore(_scoreCount);
        }

        private void AddScore()
        {
            _scoreCount++;
            UpdateScore(_scoreCount);
        }

        private void UpdateScore(int score) => 
            _scoreCountText.text = score.ToString();

        private void LosePanelActivation() => 
            _switcher.Switch(_switcher.PanelsUI[PanelsUI.Lose]);
    }
}

