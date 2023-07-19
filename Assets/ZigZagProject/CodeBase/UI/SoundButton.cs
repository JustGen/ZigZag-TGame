using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class SoundButton : MonoBehaviour
    {
        [SerializeField] private Sprite _modeOn;
        [SerializeField] private Sprite _modeOff;
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        
        private bool _isActive;
        private ISettingsService _settings;

        private void Awake()
        {
            _settings = AllServices.Container.Single<ISettingsService>();
            
            _isActive = false;
            SwitchMode();
            
            _button.onClick.AddListener(SwitchMode);
        }

        private void SwitchMode()
        {
            if (_isActive)
            {
                _image.sprite = _modeOff;
                _isActive = false;
                _settings.SoundActive = false;
            }
            else
            {
                _image.sprite = _modeOn;
                _isActive = true;
                _settings.SoundActive = true;
            }
        }
    }
}