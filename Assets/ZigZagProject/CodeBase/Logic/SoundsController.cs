using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Events;
using CodeBase.Infrastructure.Services.Settings;
using UnityEngine;

namespace CodeBase.Logic
{
    public class SoundsController : MonoBehaviour
    {
        [SerializeField] private AudioSource _soundTapSource;
        [SerializeField] private AudioSource _soundTakeStoneSource;
        [SerializeField] private AudioSource _soundLoseSource;

        private ISettingsService _settings;

        private void Awake()
        {
            _settings = AllServices.Container.Single<ISettingsService>();

            EventsProvider.OnTap += OnTapSound;
            EventsProvider.OnTakeStone += OnTakeStoneSound;
            EventsProvider.OnLose += OnLoseSound;
        }

        private void OnDestroy()
        {
            EventsProvider.OnTap -= OnTapSound;
            EventsProvider.OnTakeStone -= OnTakeStoneSound;
            EventsProvider.OnLose -= OnLoseSound;
        }

        private void OnTapSound()
        {
            if (_settings.SoundActive)
                _soundTapSource.Play();
        }

        private void OnTakeStoneSound()
        {
            if (_settings.SoundActive)
                _soundTakeStoneSource.Play();
        }

        private void OnLoseSound()
        {
            if (_settings.SoundActive)
                _soundLoseSource.Play();
        }
    }
}