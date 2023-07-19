using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class GodModeButton : MonoBehaviour
    {
        [SerializeField] private Sprite _modeOn;
        [SerializeField] private Sprite _modeOff;
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _textInfo;

        private bool _isActive;
        private IGameStateInfo _gameStateInfo;
        
        private void Awake()
        {
            _gameStateInfo = AllServices.Container.Single<IGameStateInfo>();
                
            _isActive = true;
            SwitchMode();
            _button.onClick.AddListener(SwitchMode);
        }

        private void SwitchMode()
        {
            if (_isActive)
            {
                _image.sprite = _modeOff;
                _isActive = false;
                _gameStateInfo.CheatMode = false;
                _textInfo.text = "GodMode DISABLE";
            }
            else
            {
                _image.sprite = _modeOn;
                _isActive = true;
                _gameStateInfo.CheatMode = true;
                _textInfo.text = "GodMode ENABLE";
            }
        }
    } 
}